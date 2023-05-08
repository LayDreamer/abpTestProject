using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LocalTest.FactoryList
{
    public interface IMaterialRequisitionService :
        ICrudAppService< //Defines CRUD methods
            RequisitionListDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateMaterialRequisitionDto> //Used to create/update a book
    {

    }
}
