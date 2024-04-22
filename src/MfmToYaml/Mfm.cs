using MfmToYaml.Models;
using MfmToYaml.Readers;
using System.Buffers;
using VYaml.Emitter;

namespace MfmToYaml;

public class Mfm
{
    public Dictionary<string, FunctionEnum> Enums { get; } = [];
    public Dictionary<(int, int), Function> Functions { get; } = [];

    public Mfm(Span<byte> data)
    {
        MfmReader reader = new(data, this);
        reader.Read();

        foreach (var (_, function) in Functions) {
            FormatName(function.Name);
            foreach (var (type, name) in function.Parameters) {
                FormatName(name);

                if (Enums.ContainsKey(type)) {
                    FormatName(type);
                }
            }
        }

        foreach (var (name, functionEnum)  in Enums) {
            FormatName(name);
            foreach (var (_, enumValueName) in functionEnum) {
                FormatName(enumValueName);
            }
        }
    }

    public void Write(IBufferWriter<byte> writer)
    {
        Utf8YamlEmitter emitter = new(writer);
        emitter.BeginMapping();
        {
            foreach (var (key, functionEnum) in Enums) {
                emitter.WriteString(key);
                emitter.BeginMapping();
                {
                    foreach (var (enumKey, enumName) in functionEnum) {
                        emitter.WriteInt64(enumKey);
                        emitter.WriteString(enumName);
                    }
                }
                emitter.EndMapping();
            }
        }
        emitter.EndMapping();

        emitter.WriteRaw("---"u8, indent: false, lineBreak: true);

        emitter.BeginMapping();
        {
            foreach (var ((group, type), function) in Functions) {
                emitter.BeginSequence(SequenceStyle.Flow);
                {
                    emitter.WriteInt32(group);
                    emitter.WriteInt32(type);
                }
                emitter.EndSequence();

                emitter.Tag($"!{function.Name}");
                emitter.BeginSequence();
                {
                    foreach (var (paramType, paramName) in function.Parameters) {
                        emitter.Tag($"!{paramType}");
                        emitter.WriteString(paramName);
                    }
                }
                emitter.EndSequence();
            }
        }
        emitter.EndMapping();
    }

    private unsafe void FormatName(string name)
    {
        fixed (char* ptr = name) {
            Span<char> chars = new(ptr, name.Length);
            chars[0] = char.ToUpper(chars[0]);
        }
    }
}
