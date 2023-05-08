using AutoMapper;
using LocalTest.Books;
using LocalTest.Families;
using LocalTest.FamilyLibs;
using LocalTest.FamilyTrees;
using LocalTest.MaterialSpecificationList;
using LocalTest.FactoryList;

namespace LocalTest;

public class LocalTestApplicationAutoMapperProfile : Profile
{
    public LocalTestApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();

        CreateMap<Family, FamilyDto>();
        CreateMap<CreateUpdateFamilyDto, Family>();

        CreateMap<FamilyTree, FamilyTreeDto>();

        CreateMap<FamilyLib, FamilyLibDto>();
        CreateMap<DownlodFamilyLibDto, FamilyLib>();

        CreateMap<MaterialSpecification, MaterialSpecificationDto>();
        CreateMap<CreateUpdateListDto, MaterialSpecification>();

        CreateMap<MaterialSpecificationDetail, MaterialSpecificationDetailDto>();
        CreateMap<CreateUpdateDetailDto, MaterialSpecificationDetail>();

        CreateMap<Project, ProjectDto>();
        CreateMap<CreateUpdateProjectDto, Project>();

        CreateMap<RequisitionList, RequisitionListDto>();
        CreateMap<CreateUpdateMaterialRequisitionDto, RequisitionList>();
        
    }
}
