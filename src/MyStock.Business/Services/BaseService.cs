using FluentValidation;
using FluentValidation.Results;
using MyStock.Business.Interfaces;
using MyStock.Business.Models;
using MyStock.Business.Notifications;

namespace MyStock.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string msg)
        {
            _notifier.Handle(new Notification(msg));
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : BaseEntity
        {
            var validator = validation.Validate(entity);
            if (validator.IsValid) return true;

            Notify(validator);
            return false;
        }
    }
}