using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LocalTest.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task SaveFileAsync(SaveFileInputDto input);

        Task<FileDto> GetFileAsync(GetFileRequestDto input);
    }

}
