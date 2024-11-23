using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShareFile.Lambda.Template.Models;

public readonly struct PrettyTimeSpan(TimeSpan t) : IUtf8SpanFormattable
{
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = default)
    {
        bytesWritten = 0;
        return (t.Hours == 0 || TryWriteUnit(ref utf8Destination, t.Hours, "h", ref bytesWritten)) &&
            (t.Minutes == 0 || TryWriteUnit(ref utf8Destination, t.Minutes, "m", ref bytesWritten)) &&
            (t.Seconds == 0 || TryWriteUnit(ref utf8Destination, t.Seconds, "s", ref bytesWritten)) &&
            TryWriteUnit(ref utf8Destination, t.Milliseconds, "ms", ref bytesWritten);
    }

    private static bool TryWriteUnit(ref Span<byte> buffer, int value, ReadOnlySpan<char> unit, ref int bytesWritten)
    {
        if (!value.TryFormat(buffer, out var written))
        {
            return false;
        }
        buffer = buffer[written..];
        bytesWritten += written;
        if (buffer.Length < unit.Length)
        {
            return false;
        }
        for (var i = 0; i < unit.Length; i++)
        {
            buffer[i] = (byte)unit[i];
        }
        buffer = buffer[unit.Length..];
        bytesWritten += unit.Length;
        return true;
    }
}

public class PrettyTimeSpanConverter : JsonConverter<PrettyTimeSpan>
{
    public override PrettyTimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, PrettyTimeSpan value, JsonSerializerOptions options)
    {
        Span<byte> utf8 = stackalloc byte[64];
        if (!value.TryFormat(utf8, out var bytesWritten))
        {
            throw new FormatException("unable to format timespan");
        }
        writer.WriteStringValue(utf8[..bytesWritten]);
    }
}