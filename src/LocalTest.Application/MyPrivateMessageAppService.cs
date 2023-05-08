using EasyAbp.PrivateMessaging.PrivateMessages;
using EasyAbp.PrivateMessaging.PrivateMessages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace LocalTest
{
    [Dependency(ReplaceServices = true)]
    public class MyPrivateMessageAppService : PrivateMessageAppService
    {
        public MyPrivateMessageAppService(IDataFilter dataFilter, IExternalUserLookupServiceProvider externalUserLookupServiceProvider, IPrivateMessageRepository privateMessageRepository, IPrivateMessageSenderSideManager privateMessageSenderSideManager, IPrivateMessageReceiverSideManager privateMessageReceiverSideManager) : base(dataFilter, externalUserLookupServiceProvider, privateMessageRepository, privateMessageSenderSideManager, privateMessageReceiverSideManager)
        {
        }

        public override Task<PrivateMessageDto> CreateAsync(CreateUpdatePrivateMessageDto input)
        {
            if (input.ToUserName == CurrentUser.UserName)
            {
                throw new UserFriendlyException("请勿给自己发消息");
            }
            return base.CreateAsync(input);
        }


    }
}
