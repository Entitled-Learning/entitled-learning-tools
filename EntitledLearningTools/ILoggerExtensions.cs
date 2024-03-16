// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearningTools;

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
    public static partial void CreateCommunityPartnerError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 5,
    Level = LogLevel.Error)]
    public static partial void UpdateCommunityPartnerError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 6,
    Level = LogLevel.Error)]
    public static partial void DeleteCommunityPartnerError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 7,
    Level = LogLevel.Error)]
    public static partial void CreateCommunityPartnerContactError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 8,
    Level = LogLevel.Error)]
    public static partial void UpdateCommunityPartnerContactError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 9,
    Level = LogLevel.Error)]
    public static partial void DeleteCommunityPartnerContactError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 10,
    Level = LogLevel.Error)]
    public static partial void CreateInventoryError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 11,
    Level = LogLevel.Error)]
    public static partial void UpdateInventoryError(
        this ILogger logger,
        Exception ex);

    [LoggerMessage(
    EventId = 12,
    Level = LogLevel.Error)]
    public static partial void DeleteInventoryError(
        this ILogger logger,
        Exception ex);
}
