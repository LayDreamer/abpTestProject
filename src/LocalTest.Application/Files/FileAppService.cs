using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using LocalTest.Utils;

namespace LocalTest.Files
{
    public class FileAppService : ApplicationService, Files.IFileAppService
    {
        private readonly IBlobContainer<MyFileContainer> _fileContainer;

        public FileAppService(IBlobContainer<MyFileContainer> fileContainer)
        {
            _fileContainer = fileContainer;
        }

        public async Task SaveFileAsync(SaveFileInputDto input)
        {
            await _fileContainer.SaveAsync(input.Name, input.Content, true);
        }

        public async Task<FileDto> GetFileAsync(GetFileRequestDto input)
        {
            var blob = await _fileContainer.GetAllBytesOrNullAsync(input.Name);
            return new FileDto
            {
                Name = input.Name,
                Content = blob
            };
        }

        //public async Task PostUploadFileAsync(string filePath)
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        return;
        //    }
        //    string extension = Path.GetExtension(filePath);
        //    FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //    using (stream)
        //    {
        //        var bytes = await stream.GetAllBytesAsync(default);
        //        string fileName = Path.Combine(Common.LocalMaterialListPath, $"测试图片.{extension}");
        //        Common.ByteToFile(bytes, fileName);
        //    }
        //}
    }
}
