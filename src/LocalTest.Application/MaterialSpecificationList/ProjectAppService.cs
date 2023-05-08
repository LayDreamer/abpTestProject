using LocalTest.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LocalTest.MaterialSpecificationList
{
    public class ProjectAppService :
        CrudAppService<
            Project,
            ProjectDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProjectDto>,
        IProjectAppService
    {
        protected readonly IRepository<Project, Guid> _repository;
        public ProjectAppService(IRepository<Project, Guid> repository)
             : base(repository)
        {
            _repository = repository;

            GetPolicyName = LocalTestPermissions.Projects.Default;
            GetListPolicyName = LocalTestPermissions.Projects.Default;
            CreatePolicyName = LocalTestPermissions.Projects.Create;
            UpdatePolicyName = LocalTestPermissions.Projects.Edit;
            DeletePolicyName = LocalTestPermissions.Projects.Delete;
        }


        public async Task<PagedResultDto<ProjectDto>> GetListByInputAsync(OrderNotificationSearchDto input)
        {
            IQueryable<Project> query = await _repository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.SearchValue))
            {
                //名称查询过滤
                query = query.Where(e => e.Name.Contains(input.SearchValue) || e.Code.Contains(input.SearchValue));
                //return new PagedResultDto<ProjectDto> { Items = null, TotalCount = 0 };
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<ProjectDto>(
                totalCount,
                entityDtos
            );
        }


        //public async Task<ProjectDto> GetTargetProject(string code)
        //{
        //    var resultDto = await GetListByInputAsync(new OrderNotificationSearchDto
        //    {
        //        SearchValue = code,
        //    });
        //    ProjectDto projectDto = resultDto.Items.FirstOrDefault();
        //    if (projectDto == null)
        //    {
        //        throw new Volo.Abp.UserFriendlyException("没有匹配的项目！");
        //    }
        //    return projectDto;
        //}
    }
}
