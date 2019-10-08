using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFull
{
    // @Lyrcaxis on the C# discord asked for help improving the string interpolation code below, and someone dared suggest that string.Concat wasn't good
    // and I couldn't let that rest!

    [MemoryDiagnoser]
    public class TimeFormatting
    {
        public DayOfWeek Day = DateTime.Now.DayOfWeek;
        public int Week = 5; // actually week number
        public int Hour = DateTime.Now.Hour;
        public int Minute = DateTime.Now.Minute;
        public int Second = DateTime.Now.Second;

        public StringBuilder builder = new StringBuilder();

        [Benchmark]
        public string Lyrcaxis_OriginalCode()
        {
            return $"({(int)Day + (Week * 7)}) {Day}  {Hour:00}:{Minute:00}:{Second:00}";
        }

        [Benchmark]
        public string Lyrcaxis_NewCode()
        {
            builder.Clear();
            builder.Append("(");
            builder.Append((int)Day + (Week * 7));
            builder.Append(") ");
            builder.Append(Day);
            builder.Append("  ");
            builder.Append(Hour.ToString().PadLeft(2, '0'));
            builder.Append(":");
            builder.Append(Minute.ToString().PadLeft(2, '0'));
            builder.Append(":");
            builder.Append(Second.ToString().PadLeft(2, '0'));

            return builder.ToString();
        }

        [Benchmark]
        public string StringConcat()
        {
            return "(" +
                ((int)Day + (Week * 7)) +
                ") " +
                Day +
                "  " +
                Hour.ToString().PadLeft(2, '0') +
                ":" +
                Minute.ToString().PadLeft(2, '0') +
                ":" +
                Second.ToString().PadLeft(2, '0');
        }

        [Benchmark]
        public string NoPadLeft()
        {
            return "(" +
                ((int)Day + (Week * 7)) +
                ") " +
                Day +
                "  " +
                (Hour < 10 ? "0" : "") + Hour +
                ":" +
                (Minute < 10 ? "0" : "") + Minute +
                ":" +
                (Second < 10 ? "0" : "") + Second;
        }

        [Benchmark]
        public string Math()
        {
            return "(" +
                ((int)Day + (Week * 7)) +
                ") " +
                Day +
                "  " +
                (char)((Hour / 10) + 48) +
                (char)((Hour % 10) + 48) +
                ":" +
                (char)((Minute / 10) + 48) +
                (char)((Minute % 10) + 48) +
                ":" +
                (char)((Second / 10) + 48) +
                (char)((Second % 10) + 48);
        }

        [Benchmark]
        public string StringBuilder_With_Math()
        {
            builder.Clear();
            builder.Append("(");
            builder.Append((int)Day + (Week * 7));
            builder.Append(") ");
            builder.Append(Day);
            builder.Append("  ");
            builder.Append((char)((Hour / 10) + 48));
            builder.Append((char)((Hour % 10) + 48));
            builder.Append(":");
            builder.Append((char)((Minute / 10) + 48));
            builder.Append((char)((Minute % 10) + 48));
            builder.Append(":");
            builder.Append((char)((Second / 10) + 48));
            builder.Append((char)((Second % 10) + 48));

            return builder.ToString();
        }
    }
}
