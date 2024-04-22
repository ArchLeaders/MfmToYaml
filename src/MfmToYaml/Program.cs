using CommunityToolkit.HighPerformance.Buffers;
using MfmToYaml;
using MfmToYaml.Extensions;
using System.Buffers;
using System.Reflection;

Console.WriteLine($"""
    MSBT Function Map (MFM) to YAML [Version {Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "???"}]
    (c) 2024 ArchLeaders. MIT.

    """);

if (args.Length == 0 || args[0].ToLower() is "h" or "help" or "-h" or "--help") {
    Console.WriteLine("""
          Usage:
        <input> [-o|--output OUTPUT] ...

          Documentation:
        github.com/ArchLeaders/MfmToYaml
        """);
}

for (int i = 0; i < args.Length; i++) {
    string input = args[i];

    using FileStream fs = File.OpenRead(input);
    int size = Convert.ToInt32(fs.Length);
    using SpanOwner<byte> buffer = SpanOwner<byte>.Allocate(size);
    fs.Read(buffer.Span);
    fs.Dispose();

    if (!args.TryGetOutput(ref i, out string? output)) {
        output = Path.ChangeExtension(input, "yaml");
    }

    Mfm mfm = new(buffer.Span);
    ArrayBufferWriter<byte> writer = new();
    mfm.Write(writer);

    using FileStream outputFs = File.Create(output);
    outputFs.Write(writer.WrittenSpan);
}