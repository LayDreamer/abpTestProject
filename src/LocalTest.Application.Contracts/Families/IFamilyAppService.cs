using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LocalTest.Families
{
    public interface IFamilyAppService :
        ICrudAppService< //Defines CRUD methods
            FamilyDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateFamilyDto> //Used to create/update a book
    {

    }
}
