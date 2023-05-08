using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LocalTest.FamilyLibs
{
    public interface IFamilyLibAppService :
        ICrudAppService<
            FamilyLibDto,
            Guid,
            PagedAndSortedResultRequestDto>
    {
        
    }
}
