using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace LocalTest.FactoryList
{
    public class RequisitionList : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        /// <summary>
        /// 文件存储地址
        /// </summary>
        public string FilePath { get; set; }
    }
}
