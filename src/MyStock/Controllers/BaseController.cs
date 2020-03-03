using Microsoft.AspNetCore.Mvc;
using MyStock.Business.Interfaces;

namespace MyStock.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotifier _notifier;

        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}