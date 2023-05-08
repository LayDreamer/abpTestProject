using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LocalTest.MaterialSpecificationList
{
    public interface IMaterialSpecificationDetailAppService :
        ICrudAppService<
            MaterialSpecificationDetailDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateDetailDto>
    {

    }
}
