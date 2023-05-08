using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LocalTest
{
    public class OrderNotificationSearchDto : PagedAndSortedResultRequestDto
    {
        //public Guid Id { get; set; }
        public string Key { get; set; }

        public string SearchValue { get; set; }

        public string SearchCode { get; set; }
    }
}
