// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Radzen;
using Radzen.Blazor;

namespace EntitledLearning.Tools.UI;

public class PageNotificationService
{
    private readonly NotificationService _notificationService;

    public PageNotificationService(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void NotifyErrorOccured(string? message = null)
    {
        _notificationService.Notify(new NotificationMessage
        {
            Severity = NotificationSeverity.Error,
            Duration = 4000,
            Summary = "An error has occurred.", 
            Detail = message ?? "Our team has been notified. Please try again later.",
        });
    }
}



