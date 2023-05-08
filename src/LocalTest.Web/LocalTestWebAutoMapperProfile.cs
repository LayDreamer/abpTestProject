using AutoMapper;
using LocalTest.Books;
using LocalTest.Families;
using LocalTest.FamilyLibs;
using LocalTest.MaterialSpecificationList;
using LocalTest.FactoryList;

namespace LocalTest.Web;

public class LocalTestWebAutoMapperProfile : Profile
{
    public LocalTestWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<FamilyDto, CreateUpdateFamilyDto>();
        CreateMap<FamilyLibDto, DownlodFamilyLibDto>();

        CreateMap<ProjectDto, CreateUpdateProjectDto>();

        CreateMap<MaterialSpecificationDto, CreateUpdateListDto>();
        CreateMap<MaterialSpecificationDetailDto, CreateUpdateDetailDto>();

        CreateMap<RequisitionListDto, CreateUpdateMaterialRequisitionDto>();
    }
}
