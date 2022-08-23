using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto;
using FluentValidation.Results;

namespace OnboardingSIGDB1.Domain.Interfaces
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        
        void AddNotification(string key, string message);

        void AddNotification(Notification notification);

        void AddNotifications(IReadOnlyCollection<Notification> notifications);

        void AddNotifications(IList<Notification> notifications);

        void AddNotifications(ICollection<Notification> notifications);

        void AddNotifications(ValidationResult validationResult);
        
        void AddNotifications(IEnumerable<ValidationResult> validationResult);
    }
}