using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStock.Business.Interfaces;

namespace MyStock.Extensions
{
    public class SummaryErrorsViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryErrorsViewComponent(INotifier notifier)            
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notifier.GetNotifications());       
            notifications.ForEach(x => ViewData.ModelState.AddModelError(string.Empty, x.Message));     
            return View();
        }
    }
}