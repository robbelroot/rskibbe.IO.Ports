using System.IO.Ports;
using System.Text;

namespace rskibbe.IO.Ports;

public static class SerialPortExtensions
{
    
    public static async Task<string> ReadLineAsync(this SerialPort serialPort)
    {
        var sb = new StringBuilder();
        var buffer = new byte[1];
        var response = "";
        while (true)
        {
            // TO-DO: should use buffer instead of 1 by 1
            await serialPort.BaseStream.ReadAsync(buffer, 0, 1)
                .ConfigureAwait(false);
            var character = serialPort.Encoding.GetString(buffer);
            sb.Append(character);
            var completed = StringBuilderEndsWith(sb, serialPort.NewLine);
            if (completed)
            {
                response = sb.ToString();
                response = response.Substring(0, response.Length - serialPort.NewLine.Length);
                break;
            }
        }
        return response;
    }

    public static async Task<string> ReadLineAsync(this SerialPort serialPort, CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();
        var buffer = new byte[1];
        var response = "";
        while (true)
        {
            // TO-DO: should handle cancellation better
            await serialPort.BaseStream.ReadAsync(buffer, 0, 1, cancellationToken)
                .ConfigureAwait(false);
            var character = serialPort.Encoding.GetString(buffer);
            sb.Append(character);
            var completed = StringBuilderEndsWith(sb, serialPort.NewLine);
            if (completed)
            {
                response = sb.ToString();
                response = response.Substring(0, response.Length - serialPort.NewLine.Length);
                break;
            }
        }
        return response;
    }

    public static async Task WriteLineAsync(this SerialPort serialPort, string str)
    {
        var data = serialPort.Encoding.GetBytes(str + serialPort.NewLine);
        await serialPort.BaseStream.WriteAsync(data, 0, data.Length)
                .ConfigureAwait(false);
        await serialPort.BaseStream.FlushAsync()
                .ConfigureAwait(false);
    }

    public static async Task<string> RequestResponseAsync(this SerialPort serialPort, string str)
    {
        await WriteLineAsync(serialPort, str)
                .ConfigureAwait(false);
        var response = await ReadLineAsync(serialPort)
                .ConfigureAwait(false);
        return response;
    }

    public static async Task<string> RequestResponseAsync(this SerialPort serialPort, string str, CancellationToken cancellationToken)
    {
        await WriteLineAsync(serialPort, str)
                .ConfigureAwait(false);
        var response = await ReadLineAsync(serialPort, cancellationToken)
                .ConfigureAwait(false);
        return response;
    }

    private static bool StringBuilderEndsWith(StringBuilder sb, string str)
    {
        if (sb.Length < str.Length)
            return false;
        var end = sb.ToString(sb.Length - str.Length, str.Length);
        return end.Equals(str);
    }

}