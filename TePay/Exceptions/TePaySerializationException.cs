namespace TePay.Exceptions;

/// <summary>
/// Represents an exception that occurs during JSON serialization or deserialization.
/// </summary>
internal class TePaySerializationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TePaySerializationException"/> class.
    /// </summary>
    /// <param name="message">The error message that describes the issue.</param>
    /// <param name="innerException">The inner exception that caused this serialization error.</param>
    public TePaySerializationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
