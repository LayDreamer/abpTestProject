using LocalTest.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LocalTest.FactoryList
{
    public class MaterialRequisitionService :
        CrudAppService<
            RequisitionList, //The Book entity
            RequisitionListDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateMaterialRequisitionDto>, //Used to create/update a book
        IMaterialRequisitionService //implement the IBookAppService
    {
        protected readonly IRepository<RequisitionList, Guid> _repository;
        public MaterialRequisitionService(IRepository<RequisitionList, Guid> repository)
            : base(repository)
        {
            _repository = repository;
            GetPolicyName = LocalTestPermissions.FactoryMaterialRequisition.Default;
            GetListPolicyName = LocalTestPermissions.FactoryMaterialRequisition.Default;
            CreatePolicyName = LocalTestPermissions.FactoryMaterialRequisition.Create;
            DeletePolicyName = LocalTestPermissions.FactoryMaterialRequisition.Delete;
        }

        public async Task<PagedResultDto<RequisitionListDto>> GetListByInputAsync(OrderNotificationSearchDto input)
        {
            IQueryable<RequisitionList> query = await _repository.GetQueryableAsync();
            
            ///只允许看自己的数据
            query = query.Where(e => e.CreatorId == CurrentUser.Id);

            if (!string.IsNullOrEmpty(input.SearchValue))
            {
                //名称查询过滤
                query = query.Where(e => e.Name.Contains(input.SearchValue));
                //return new PagedResultDto<ProjectDto> { Items = null, TotalCount = 0 };
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<RequisitionListDto>(
                totalCount,
                entityDtos
            );
        }
    }
}
