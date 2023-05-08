using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LocalTest.Families
{
    public interface IFamilyTreeAppService :
        ICrudAppService<
            FamilyTreeDto,
            Guid,
            PagedAndSortedResultRequestDto>
    {
        
    }
}
