using Microsoft.Extensions.Hosting;
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

namespace LocalTest.Utils
{
    public static class Common
    {
        /// <summary>
        /// 开发环境测试地址
        /// </summary>
        public static string currentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YS");
        public static string LocalMaterialListPath = Path.Combine(currentDirectory, "FileManagement", "MaterialList");
        //public static string MaterialListPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FileManagement" + "\\MaterialList";
        //public static string TestFactoryListPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FileManagement" + "\\FactoryList";
        public static string LocalFactoryListPath = Path.Combine(currentDirectory, "FileManagement", "FactoryList");

        /// <summary>
        /// 服务器本地存储清单地址
        /// </summary>
        public const string ServerLocalMaterialListPath = @"E:\ServerData\FileManagement\MaterialList";
        public const string ServerLocalMaterialRequisitionPath = @"E:\ServerData\FileManagement\FactoryList";

        /// <summary>
        /// 外部访问服务器清单地址（虚拟目录地址）
        /// </summary>
        public const string ServerWebMaterialListPathHttps = "https://10.10.12.33:4431/MaterialList/";
        //public const string ServerWebMaterialListPathHttp = "http://10.10.12.33:9091/MaterialList/";

        public const string ServerWebFactoryListPathHttps = "https://10.10.12.33:4431/FactoryList/";
        //public const string ServerWebFactoryListPathHttp = "http://10.10.12.33:9091/FactoryList/";

        #region Excel数据转换

        /// <summary>
        /// Datable To List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToModelList<T>(this DataTable dt) where T : new()
        {
            // 定义集合    
            List<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = null, tempDescription = null;

            foreach (DataRow dr in dt.Rows)
            {
                bool isAdd = true;
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    // 检查DataTable是否包含此列    
                    tempName = pi.Name;
                    tempDescription = pi == null ? null : ((DescriptionAttribute)Attribute.GetCustomAttribute(pi, typeof(DescriptionAttribute)))?.Description;
                    string column = tempDescription ?? tempName;
                    if (dt.Columns.Contains(column))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite)
                            continue;
                        object value = dr[column];
                        try
                        {
                            if (value != DBNull.Value)
                            {
                                if (pi.PropertyType.ToString().Contains("System.Nullable"))
                                    value = Convert.ChangeType(value, Nullable.GetUnderlyingType(pi.PropertyType));
                                else
                                    value = Convert.ChangeType(value, pi.PropertyType);
                                pi.SetValue(t, value, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                if (isAdd)
                {
                    ts.Add(t);
                }
            }
            return ts;
        }

        /// <summary>
        /// Worksheet To DataTable 
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="rowStart"></param>
        /// <param name="ColEnd"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DataTable WorksheetToTable(ExcelWorksheet worksheet, int rowStart, int ColEnd)
        {
            int rows = worksheet.Dimension.End.Row;
            int cols = worksheet.Dimension.End.Column;
            DataTable dt = new DataTable(worksheet.Name);
            DataRow dr = null;
            bool isStop = false;
            ///行
            for (int i = 1; i <= rows; i++)
            {
                if (i >= rowStart && !isStop)
                {
                    dr = dt.Rows.Add();
                }
                ///列
                int colCount = 1;
                for (int j = 1; j <= cols; j++)
                {
                    if (i == rowStart - 1)
                    {
                        //string value = GetString(worksheet.Cells[i, j].Value);
                        string value = GetMegerValue(worksheet, i, j);
                        if (!dt.Columns.Contains(value))
                        {
                            dt.Columns.Add(value);
                        }
                    }
                    else
                    {
                        if (i < rowStart || j > ColEnd)
                        {
                            continue;
                        }
                        //string value = GetString(worksheet.Cells[i, j].Value);
                        string value = GetMegerValue(worksheet, i, j);
                        if (!string.IsNullOrEmpty(value))
                        {

                        }
                        if (j == 1 && string.IsNullOrEmpty(value))
                        {
                            isStop = true;
                            dr.Delete();
                        }

                        ///处理列名重复情况
                        var currentColumn = GetMegerValue(worksheet, rowStart - 1, j);
                        if (j > 1)
                        {
                            var previousColumn = GetMegerValue(worksheet, rowStart - 1, j - 1);
                            if (currentColumn == previousColumn)
                            {
                                continue;
                            }
                            colCount++;
                        }
                        dr[colCount - 1] = value;
                    }
                }
            }
            return dt;
        }

        static string GetString(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 读取合并单元格内的数据
        /// </summary>
        /// <param name="wSheet"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetMegerValue(ExcelWorksheet wSheet, int row, int column)
        {
            string range = wSheet.MergedCells[row, column];
            if (range == null)
                if (wSheet.Cells[row, column].Value != null)
                    return wSheet.Cells[row, column].Value.ToString();
                else
                    return "";
            object value =
                wSheet.Cells[(new ExcelAddress(range)).Start.Row, (new ExcelAddress(range)).Start.Column].Value;
            if (value != null)
                return value.ToString();
            else
                return "";
        }

        #endregion


        /// <summary>
        /// 判断文件名后缀
        /// </summary>
        public static bool JudgeFileNameExtension(string fileName, List<string> permittedExtensions)
        {
            bool isLegal = true;
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                isLegal = false;
            }
            return isLegal;
        }


        public static bool ByteToFile(byte[] byteArray, string fileName)
        {
            bool result = false;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
