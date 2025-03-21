using System.Text.Json;
using TePay.Exceptions;
using System.Text.Json.Serialization;

namespace TePay.Helpers;

/// <summary>
/// A helper class for handling JSON serialization and deserialization using <see cref="JsonSerializer"/>.
/// This class provides methods to serialize objects into JSON strings and deserialize JSON data into objects.
/// It uses a default set of serializer options that include camelCase property naming and ignoring null properties during serialization.
/// If any serialization or deserialization fails, a <see cref="TePaySerializationException"/> is thrown with relevant error details.
/// </summary>
internal static class JsonHelper
{
    private static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Asynchronously deserializes a JSON stream into an object of type <typeparamref name="T"/>.
    /// Throws a <see cref="TbcPaySerializationException"/> if deserialization fails or returns null.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize the JSON into.</typeparam>
    /// <param name="stream">The stream containing JSON data.</param>
    /// <returns>The deserialized object of type <typeparamref name="T"/>.</returns>
    /// <exception cref="TePaySerializationException">Thrown when deserialization fails or returns null.</exception>
    public static async Task<T> DeserializeAsync<T>(Stream stream)
    {
        try
        {
            var result = await JsonSerializer.DeserializeAsync<T>(stream, DefaultSerializerOptions);

            if (result == null)
            {
                throw new TePaySerializationException($"Deserialization returned null for type {typeof(T).Name}.", null!);
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new TePaySerializationException($"Failed to deserialize stream into type {typeof(T).Name}.", ex);
        }
    }

    /// <summary>
    /// Serializes an object into a JSON string.
    /// Throws a <see cref="TePaySerializationException"/> if serialization fails.
    /// </summary>
    /// <param name="content">The object to serialize.</param>
    /// <returns>A JSON string representing the object.</returns>
    /// <exception cref="TePaySerializationException">Thrown when serialization fails.</exception>
    public static string Serialize(object content)
    {
        try
        {
            return JsonSerializer.Serialize(content, DefaultSerializerOptions);
        }
        catch (Exception ex)
        {
            throw new TePaySerializationException("Failed to serialize object.", ex);
        }
    }
}
