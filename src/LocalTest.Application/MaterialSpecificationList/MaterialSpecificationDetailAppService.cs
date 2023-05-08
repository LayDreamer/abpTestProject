using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LocalTest.MaterialSpecificationList
{
    public class MaterialSpecificationDetailAppService :
        CrudAppService<
            MaterialSpecificationDetail,
            MaterialSpecificationDetailDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateDetailDto>,
        IMaterialSpecificationDetailAppService
    {
        protected readonly IRepository<MaterialSpecificationDetail, Guid> _repository;
        public MaterialSpecificationDetailAppService(IRepository<MaterialSpecificationDetail, Guid> repository)
             : base(repository)
        {
            _repository = repository;
        }
        public async Task<PagedResultDto<MaterialSpecificationDetailDto>> GetListByIdAsync(OrderNotificationSearchDto input)
        {
            if (input.SearchValue == null || input.SearchValue.Trim() == "null")
            {
                return new PagedResultDto<MaterialSpecificationDetailDto> { Items = null, TotalCount = 0 };
                //throw new Volo.Abp.UserFriendlyException("请重新选择节点！");
            }
            
            IQueryable<MaterialSpecificationDetail> query = await _repository.GetQueryableAsync();

            if (input.SearchValue != null)
            {
                query = query.Where(e => e.MaterialNumber == input.SearchValue);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<MaterialSpecificationDetailDto>(
                totalCount,
                entityDtos
            );
        }
    }
}
