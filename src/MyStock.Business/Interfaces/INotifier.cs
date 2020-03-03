using System.Collections.Generic;
using MyStock.Business.Notifications;

namespace MyStock.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}