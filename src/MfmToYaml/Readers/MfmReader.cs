using MfmToYaml.Models;
using System.Globalization;
using System.Text;

namespace MfmToYaml.Readers;

public ref struct MfmReader(Span<byte> data, Mfm mfm)
{
    private enum State
    {
        None,
        FunctionEnum,
        FunctionEnumValue,
        Function,
        FunctionParameter,
        Comment,
    }

    private const byte SPACE = (byte)' ';
    private const byte TAB = (byte)'\t';
    private const byte CR = (byte)'\r';
    private const byte LF = (byte)'\n';
    private const byte HASHTAG = (byte)'#';
    private const byte COMMA = (byte)',';
    private const byte OPEN_BRACKET = (byte)'[';
    private const byte CLOSE_BRACKET = (byte)']';
    private const byte OPEN_MAP = (byte)'m';

    private readonly Mfm _mfm = mfm;
    private readonly Span<byte> _data = data;
    private int _position;

    public void Read()
    {
        FunctionEnum? currentEnum = null;
        Function? currentFunction = null;

        State previousState = State.None;
        State state = State.None;

        while (_position < _data.Length) {
            switch (state) {
                case State.Comment: {
                    SkipComment();
                    state = previousState;
                    previousState = State.None;
                    break;
                }
                case State.FunctionEnum: {
                    _position += 3; // skip map keyword
                    SkipWhitespace();
                    string enumName = ReadString();
                    SkipWhitespace();
                    string enumType = ReadString();
                    _mfm.Enums.Add(enumName, currentEnum = new(enumType));
                    state = State.FunctionEnumValue;
                    break;
                }
                case State.FunctionEnumValue: {
                    ArgumentNullException.ThrowIfNull(currentEnum, nameof(currentEnum));

                    SkipWhitespace();
                    if (IsSpecialChar()) {
                        previousState = state;
                        goto default;
                    }

                    long enumValue = Read<long>();
                    SkipWhitespace();
                    string enumValueName = ReadString();
                    currentEnum.Add(enumValue, enumValueName);
                    break;
                }
                case State.Function: {
                    _position++;

                    int group = Read<int>();
                    _position++;
                    SkipWhitespace();

                    int type = Read<int>();
                    _position++;
                    SkipWhitespace();

                    string name = ReadString();
                    _mfm.Functions.Add((group, type), currentFunction = new(name, []));
                    state = State.FunctionParameter;
                    break;
                }
                case State.FunctionParameter: {
                    ArgumentNullException.ThrowIfNull(currentFunction, nameof(currentFunction));

                    SkipWhitespace();
                    if (IsSpecialChar()) {
                        previousState = state;
                        goto default;
                    }

                    string functionParamType = ReadString().Trim('{', '}');
                    SkipWhitespace();
                    string functionParamName = ReadString();

                    currentFunction.Parameters.Add(new(functionParamType, functionParamName));
                    break;
                }
                default:
                    SkipWhitespace();
                    state = _data[_position] switch {
                        HASHTAG => State.Comment,
                        OPEN_MAP => State.FunctionEnum,
                        OPEN_BRACKET => State.Function,
                        _ => throw new InvalidOperationException($"""
                            Unexpected character: '{(char)_data[_position]}'
                            """)
                    };
                    break;
            }
        }
    }

    private void SkipComment()
    {
        while (_position < _data.Length && _data[_position] is not (byte)'\n') {
            _position++;
        }
    }

    private void SkipWhitespace()
    {
        while (_position < _data.Length && _data[_position] is SPACE or TAB or CR or LF) {
            _position++;
        }
    }

    private readonly bool IsSpecialChar()
    {
        return _data[_position] is OPEN_MAP or OPEN_BRACKET or HASHTAG;
    }

    private T Read<T>() where T : IUtf8SpanParsable<T>
    {
        ReadOnlySpan<byte> raw = ReadSpan();
        return T.Parse(raw, CultureInfo.InvariantCulture.NumberFormat);
    }

    private string ReadString()
    {
        Span<byte> raw = ReadSpan();
        return Encoding.UTF8.GetString(raw);
    }

    private Span<byte> ReadSpan()
    {
        int start = _position;
        while (_position < _data.Length && _data[_position] is not (SPACE or TAB or CR or LF or HASHTAG or COMMA or CLOSE_BRACKET)) {
            _position++;
        }

        return _data[start.._position];
    }
}
