
namespace DomainDrivenDesign.Helpers.Exceptions;

using System;

/// <summary>
/// Basisklasse für alle Domänenbezogenen Ausnahmen.
/// Diese Ausnahme wird verwendet, um Fehler innerhalb der Domänenlogik darzustellen.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Ein optionaler Fehlercode, der die Art des Fehlers beschreibt.
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Logger-Schnittstelle für das Protokollieren von Ausnahmen.
    /// </summary>
    private readonly ILogger? _logger;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DomainException"/>-Klasse mit einer Fehlermeldung.
    /// </summary>
    /// <param name="message">Die Fehlermeldung, die den Fehler beschreibt.</param>
    /// <param name="logger">Ein optionaler Logger, um die Ausnahme zu protokollieren.</param>
    protected DomainException(string message, ILogger? logger = null) : base(message)
    {
        _logger = logger;
        LogError(message);
    }

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DomainException"/>-Klasse mit einer Fehlermeldung und einer inneren Ausnahme.
    /// </summary>
    /// <param name="message">Die Fehlermeldung, die den Fehler beschreibt.</param>
    /// <param name="innerException">Die innere Ausnahme, die die Ursache des Fehlers beschreibt.</param>
    /// <param name="logger">Ein optionaler Logger, um die Ausnahme zu protokollieren.</param>
    protected DomainException(string message, Exception innerException, ILogger? logger = null)
        : base(message, innerException)
    {
        _logger = logger;
        LogError(message);
    }

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DomainException"/>-Klasse mit einer Fehlermeldung und einem Fehlercode.
    /// </summary>
    /// <param name="message">Die Fehlermeldung, die den Fehler beschreibt.</param>
    /// <param name="errorCode">Der Fehlercode, der die Art des Fehlers beschreibt.</param>
    /// <param name="logger">Ein optionaler Logger, um die Ausnahme zu protokollieren.</param>
    protected DomainException(string message, string errorCode, ILogger? logger = null) : base(message)
    {
        ErrorCode = errorCode;
        _logger = logger;
        LogError(message);
    }

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DomainException"/>-Klasse mit einer Fehlermeldung,
    /// einem Fehlercode und einer inneren Ausnahme.
    /// </summary>
    /// <param name="message">Die Fehlermeldung, die den Fehler beschreibt.</param>
    /// <param name="errorCode">Der Fehlercode, der die Art des Fehlers beschreibt.</param>
    /// <param name="innerException">Die innere Ausnahme, die die Ursache des Fehlers beschreibt.</param>
    /// <param name="logger">Ein optionaler Logger, um die Ausnahme zu protokollieren.</param>
    protected DomainException(string message, string errorCode, Exception innerException, ILogger? logger = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        _logger = logger;
        LogError(message);
    }

    /// <summary>
    /// Protokolliert die Fehlermeldung über den optionalen Logger.
    /// </summary>
    /// <param name="message">Die Fehlermeldung, die protokolliert werden soll.</param>
    private void LogError(string message)
    {
        if (_logger != null)
        {
            _logger.LogError(message);
        }
    }
}

/// <summary>
/// Schnittstelle für Logger.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Protokolliert eine Fehlermeldung.
    /// </summary>
    /// <param name="message">Die Fehlermeldung, die protokolliert werden soll.</param>
    void LogError(string message);
}

