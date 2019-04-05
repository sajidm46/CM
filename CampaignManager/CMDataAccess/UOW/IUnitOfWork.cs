using CMDataAccess.Repositories.Abstract;
using System;

namespace CMDataAccess.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        //INotificationRepository NotificationRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        ICustomerDetailsRepository CustomerDetailsRepository { get; }
    }
}
