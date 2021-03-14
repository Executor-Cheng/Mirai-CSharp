using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mirai_CSharp.Utility
{
    //see https://github.com/mamoe/mirai/blob/dev/docs/Messages.md#mirai-%E7%A0%81
    public ref struct MiraiCodeReader
    {
        public delegate void NameHandler(ReadOnlySpan<char> name);

        public delegate void ArgumentHandler(ReadOnlySpan<char> argument);

        private readonly ReadOnlySpan<char> _Code;

        private int _Index;

        private ReadOnlySpan<char> Current => GetFromIndex(_Code, _Index);

        public MiraiCodeReader(ReadOnlySpan<char> code)
        {
            _Code = code;
            _Index = 0;
        }

        public void Parse(NameHandler nameHandler, ArgumentHandler argumentHandler)
        {
            //[mirai:
            ReadOnlySpan<char> code = Current;
            if (code.Length < 9)
            {
                throw new FormatException();
            }
            if (!GetFromLength(code, 7).SequenceEqual("[mirai:".AsSpan()))
            {
                throw new FormatException();
            }
            if (!FindBlock(GetFromIndex(code, 7), ']', out code))
            {
                throw new FormatException("Missing end of char ]");
            }
            //[mirai:......]
            _Index += code.Length + 7;
            if (!FindBlock(code, ':', out ReadOnlySpan<char> result))
            {
                if (code.Length - 1 == 0)
                {
                    throw new FormatException("Missing name");
                }
                nameHandler(GetFromLength(code, code.Length - 1));
                return;
            }
            nameHandler(GetFromLength(result, result.Length - 1));
            while (FindBlock(code = GetFromIndex(code, result.Length), ':', out result))
            {
                argumentHandler(GetFromLength(result, result.Length - 1));
            }
            argumentHandler(GetFromLength(code, code.Length - 1)); // do not check empty argument
        }

        private static unsafe bool FindBlock(ReadOnlySpan<char> input, char seperator, out ReadOnlySpan<char> span)
        {
            int end = 0;
            ReadOnlySpan<char> code;
            do
            {
                code = CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(input), end), input.Length - end);
                int result = code.IndexOf(seperator);
                if (result == -1)
                {
                    span = default;
                    return false;
                }
                end += result;
            }
            while (Unsafe.Add(ref MemoryMarshal.GetReference(code), end++ - 1) == '\\');
            span = CreateReadOnlySpan(ref MemoryMarshal.GetReference(input), end);
            return true;
        }

        private static unsafe ReadOnlySpan<char> GetFromIndex(ReadOnlySpan<char> span, int index)
        {
            return CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), index), span.Length - index);
        }

        private static unsafe ReadOnlySpan<char> GetFromLength(ReadOnlySpan<char> span, int length)
        {
            return CreateReadOnlySpan(ref MemoryMarshal.GetReference(span), length);
        }
#if !NETSTANDARD2_0
        private static unsafe ReadOnlySpan<char> CreateReadOnlySpan(ref char c, int length)
        {
            return MemoryMarshal.CreateReadOnlySpan(ref c, length);
        }
#else
        private static unsafe ReadOnlySpan<char> CreateReadOnlySpan(ref char c, int length)
        {
            return new ReadOnlySpan<char>(Unsafe.AsPointer(ref c), length);
        }
#endif
    }
}
