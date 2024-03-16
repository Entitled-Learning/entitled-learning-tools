// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
namespace ELDataAccessLibrary.Repository;

public static partial class ILoggerExtensions
{
    public static IDisposable BeginScopeWith(this ILogger logger, params (string key, object value)[] keyValuePairs)
    {
        var dictionary = keyValuePairs.ToDictionary(x => x.key, x => x.value);
        var scope = logger.BeginScope(dictionary);
        return scope ?? NullScope.Instance; // Return a fallback disposable object if scope is null
    }

    private class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        public void Dispose() { } // No-op disposal
    }

    [LoggerMessage(
    EventId = 1,
    Level = LogLevel.Error)]
    public static partial void CreateStudentError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 2,
    Level = LogLevel.Error)]
    public static partial void UpdateStudentError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 3,
    Level = LogLevel.Error)]
    public static partial void DeleteStudentError(
        this ILogger logger,
        Exception ex);
    
    [LoggerMessage(
    EventId = 4,
    Level = LogLevel.Error)]
    public static partial void GetStudentError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 5,
    Level = LogLevel.Error)]
    public static partial void CreateCommunityPartnerError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 6,
    Level = LogLevel.Error)]
    public static partial void UpdateCommunityPartnerError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 7,
    Level = LogLevel.Error)]
    public static partial void DeleteCommunityPartnerError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 8,
    Level = LogLevel.Error)]
    public static partial void GetCommunityPartnerError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 9,
    Level = LogLevel.Error)]
    public static partial void CreateCommunityPartnerContactError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 10,
    Level = LogLevel.Error)]
    public static partial void UpdateCommunityPartnerContactError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 11,
    Level = LogLevel.Error)]
    public static partial void DeleteCommunityPartnerContactError(
        this ILogger logger,
        Exception ex);
    
    [LoggerMessage(
    EventId = 12,
    Level = LogLevel.Error)]
    public static partial void GetCommunityPartnerContactError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 13,
    Level = LogLevel.Error)]
    public static partial void CreateInventoryError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 14,
    Level = LogLevel.Error)]
    public static partial void UpdateInventoryError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 15,
    Level = LogLevel.Error)]
    public static partial void DeleteInventoryError(
        this ILogger logger,
        Exception ex);
    
    [LoggerMessage(
    EventId = 16,
    Level = LogLevel.Error)]
    public static partial void GetInventoryError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 17,
    Level = LogLevel.Error)]
    public static partial void CreateGuardianError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 18,
    Level = LogLevel.Error)]
    public static partial void UpdateGuardianError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 19,
    Level = LogLevel.Error)]
    public static partial void DeleteGuardianError(
        this ILogger logger,
        Exception ex);
    
    [LoggerMessage(
    EventId = 20,
    Level = LogLevel.Error)]
    public static partial void GetGuardianError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 21,
    Level = LogLevel.Error)]
    public static partial void CreateGuardianStudentRelationshipError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 22,
    Level = LogLevel.Error)]
    public static partial void UpdateGuardianStudentRelationshipError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 23,
    Level = LogLevel.Error)]
    public static partial void DeleteGuardianStudentRelationshipError(
        this ILogger logger,
        Exception ex);
    
    [LoggerMessage(
    EventId = 24,
    Level = LogLevel.Error)]
    public static partial void GetGuardianStudentRelationshipError(
        this ILogger logger,
        Exception ex);
}
