﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace LocalTest.Families
{
    public class Family : AuditedAggregateRoot<Guid>
    {
        public string ProcductName { get; set; }

        public string FileName { get; set; }

    }
}
