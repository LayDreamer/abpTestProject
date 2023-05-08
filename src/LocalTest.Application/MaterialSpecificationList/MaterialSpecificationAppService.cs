using LocalTest.Permissions;
using LocalTest.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LocalTest.MaterialSpecificationList
{
    public class MaterialSpecificationAppService :
        CrudAppService<
            MaterialSpecification,
            MaterialSpecificationDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateListDto>,
        IMaterialSpecificationAppService
    {
        protected readonly IRepository<MaterialSpecification, Guid> _repository;
        public MaterialSpecificationAppService(IRepository<MaterialSpecification, Guid> repository)
             : base(repository)
        {
            _repository = repository;

            GetPolicyName = LocalTestPermissions.MaterialSpecificationList.Default;
            GetListPolicyName = LocalTestPermissions.MaterialSpecificationList.Default;
            CreatePolicyName = LocalTestPermissions.MaterialSpecificationList.Create;
            DeletePolicyName = LocalTestPermissions.MaterialSpecificationList.Delete;
        }
    }
}
