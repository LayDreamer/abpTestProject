using LocalTest.MaterialSpecificationList;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LocalTest.Utils
{
    public class ExcelHelper
    {
        public ExcelHelper()
        {
            //指定EPPlus使用非商业化许可证
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }
        /// <summary>
        /// 解析上传Excel文件
        /// </summary>
        /// <typeparam name="T">需要转换的类型</typeparam>
        /// <param name="file">上传的文件</param>
        /// <param name="TargetDir">目标存储目录</param>
        /// <param name="sheetName">表单名</param>
        /// <param name="rowCount">指定读取的起始行</param>
        /// <param name="colCount">-1时默认读取到总表单列数，否则读取到指定列数</param>
        /// <returns>目标实体的集合</returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<List<T>> ParseExcelAsync<T>(IFormFile file, string TargetDir, string sheetName, int rowCount, int colCount) where T : new()
        {
            List<T> materials = new();
            string filePath = Path.Combine(TargetDir, file.FileName);

            bool isLegal = Common.JudgeFileNameExtension(file.FileName, new List<string> { ".xlsx", ".xls" });
            if (!isLegal)
                throw new UserFriendlyException("请选择Excel文件进行上传！");

            try
            {
                if (!Directory.Exists(TargetDir))
                {
                    Directory.CreateDirectory(TargetDir);
                }

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"【{sheetName}】文件保存错误：{ex.Message}");
            }


            materials = ReadInfoFromExcel<T>(filePath, sheetName, rowCount, colCount, out string errorMessage);
            if (materials.Count == 0 || !string.IsNullOrEmpty(errorMessage))
                throw new UserFriendlyException($"Excel【{sheetName}】数据解析错误：{errorMessage}");
            return materials;
        }


        /// <summary>
        /// 读取excel文件中的信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>

        public static List<T> ReadInfoFromExcel<T>(string fileName, string sheetName, int rowCount, int colCount, out string errorMessage) where T : new()
        {
            errorMessage = string.Empty;

            List<T> datas = new();
            try
            {
                using (ExcelPackage Excel = new ExcelPackage(new FileInfo(fileName)))//打开表
                {
                    var worksheets = Excel.Workbook.Worksheets;
                    ExcelWorksheet sheet = Excel.Workbook.Worksheets[0];
                    if (!string.IsNullOrEmpty(sheetName))
                    {
                        sheet = worksheets.FirstOrDefault(e => e.Name.Contains(sheetName));
                        if (sheet == null)
                        {
                            errorMessage = $"未找到指定sheet表：{sheetName}！";
                        }
                    }

                    ///-1时默认读取表单的
                    if (colCount < 0)
                    {
                        colCount = sheet.Dimension.End.Column;
                    }

                    ///从指定行开始读取数据，行数据为空结束
                    DataTable dataTable = Common.WorksheetToTable(sheet, rowCount, colCount);
                    datas = dataTable.ToModelList<T>();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return datas;
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">文件存储地址</param>
        /// <param name="tList">数据源</param>
        /// <param name="titleRow">列名起始行</param>
        /// <param name="dataRow">数据起始行</param>
        /// <param name="titleCol">列名起始列</param>
        /// <returns></returns>
        public async Task OutPutExcel<T>(string filePath, string targetFilePath, List<T> tList, int titleRow = 1, int dataRow = 2, int titleCol = 1) where T : new()
        {
            if (!File.Exists(filePath))
            {
                throw new UserFriendlyException($"模板地址不存在：{filePath}");
            }

            string resDir = Path.GetDirectoryName(targetFilePath);
            if (!Directory.Exists(resDir))
            {
                Directory.CreateDirectory(resDir);
            }

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                //工作簿
                //ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                //实体属性
                PropertyInfo[] properties = GetProperties(new T());
                //填充表头
                //for (int i = 1; i < properties.Length + 1; i++)
                for (int i = titleCol; i < properties.Length + titleCol; i++)
                {
                    //worksheet.Cells[1, i].Value = properties[i - 1].Name;
                    string des = properties[i - titleCol] == null ? null : ((DescriptionAttribute)Attribute.GetCustomAttribute(properties[i - titleCol], typeof(DescriptionAttribute)))?.Description;
                    worksheet.Cells[titleRow, i].Value = des;
                    //worksheet.Cells[titleRow, i].Style.Font.Size = 18;
                }

                //填充行(从第二行开始)
                //for (int i = 2; i < tList.Count + 2; i++)
                for (int i = dataRow; i < tList.Count + dataRow; i++)
                {
                    //填充行内列
                    //for (int j = 1; j < properties.Length + 1; j++)
                    for (int j = titleCol; j < properties.Length + titleCol; j++)
                    {
                        var property = properties[j - titleCol].Name;
                        var des = properties[j - titleCol] == null ? null : ((DescriptionAttribute)Attribute.GetCustomAttribute(properties[j - titleCol], typeof(DescriptionAttribute)))?.Description;
                        if (des == null)
                            continue;
                        worksheet.Cells[i, j].Value = GetPropertyValue(tList[i - dataRow], property);
                    }
                }

                foreach (var item in worksheet.Cells)
                {
                    item.Style.Font.Size = 18;
                }

                //列宽自适应
                worksheet.Cells.AutoFitColumns();

                using (FileStream fileStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write))
                {
                    //保存
                    await package.SaveAsAsync(fileStream);
                }

            }
        }

        /// <summary>
        /// 获取类的全部属性
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetProperties<T>(T t) where T : new()
        {
            PropertyInfo[] properties = t.GetType().GetProperties();
            return properties;
        }

        /// <summary>
        /// 获取类的属性值
        /// </summary>
        /// <param name="obj">类</param>
        /// <param name="property">属性</param>
        /// <returns></returns>
        private static object GetPropertyValue(object obj, string property)
        {
            return obj.GetType().GetProperty(property).GetValue(obj);
        }
    }
}
