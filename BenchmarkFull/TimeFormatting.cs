using System;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace BenchmarkFull
{
    // @Lyrcaxis on the C# discord asked for help improving the string interpolation code below, and someone dared suggest that string.Concat wasn't good
    // and I couldn't let that rest!

    [MemoryDiagnoser]
    public class TimeFormatting
    {
        // credit to @sc_holden for thinking of doing this
        private readonly string[] PrebuiltDaysString = new string[] {
            ") " + DayOfWeek.Sunday+ "  ",
            ") " + DayOfWeek.Monday + "  ",
            ") " + DayOfWeek.Tuesday + "  ",
            ") " + DayOfWeek.Wednesday+ "  ",
            ") " + DayOfWeek.Thursday+ "  ",
            ") " + DayOfWeek.Friday+ "  ",
            ") " + DayOfWeek.Saturday+ "  "
        };

        // Disabling readonly in case it affects outputted code
#pragma warning disable IDE0044 // Add readonly modifier

        private DayOfWeek Day = DateTime.Now.DayOfWeek;
        private int Week = 12; // actually week number
        private int Hour = DateTime.Now.Hour;
        private int Minute = DateTime.Now.Minute;
        private int Second = DateTime.Now.Second;

        private StringBuilder builder = new StringBuilder();

#pragma warning restore IDE0044 // Add readonly modifier

        [Benchmark]
        public string StringInterpolation()
        {
            return $"({(int)Day + (Week * 7)}) {Day}  {Hour:00}:{Minute:00}:{Second:00}";
        }

        [Benchmark]
        public string StringBuilder_AppendFormat()
        {
            builder.Clear();
            builder.Append("(");
            builder.Append((int)Day + (Week * 7));
            builder.Append(") ");
            builder.Append(Day);
            builder.Append("  ");
            builder.AppendFormat("{0:00}", Hour);
            builder.Append(":");
            builder.AppendFormat("{0:00}", Minute);
            builder.Append(":");
            builder.AppendFormat("{0:00}", Second);

            return builder.ToString();
        }

        [Benchmark]
        public string StringBuilder_NotCached()
        {
            var localBuilder = new StringBuilder();
            localBuilder.Clear();
            localBuilder.Append("(");
            localBuilder.Append((int)Day + (Week * 7));
            localBuilder.Append(") ");
            localBuilder.Append(Day);
            localBuilder.Append("  ");
            localBuilder.Append(Hour.ToString().PadLeft(2, '0'));
            localBuilder.Append(":");
            localBuilder.Append(Minute.ToString().PadLeft(2, '0'));
            localBuilder.Append(":");
            localBuilder.Append(Second.ToString().PadLeft(2, '0'));

            return localBuilder.ToString();
        }

        [Benchmark]
        public string StringBuilder()
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
        public string StringBuilder_NoPadLeft()
        {
            builder.Clear();
            builder.Append("(");
            builder.Append((int)Day + (Week * 7));
            builder.Append(") ");
            builder.Append(Day);
            builder.Append("  ");
            if (Hour < 10)
            {
                builder.Append("0");
            }
            builder.Append(Hour);
            builder.Append(":");
            if (Minute < 10)
            {
                builder.Append("0");
            }
            builder.Append(Minute);
            builder.Append(":");
            if (Second < 10)
            {
                builder.Append("0");
            }
            builder.Append(Second);

            return builder.ToString();
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
        public string StringBuilder_Math()
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

        [Benchmark]
        public string Math_And_Prebuilt_Days()
        {
            var dayNumber = (int)Day;

            return "(" +
                (dayNumber + (Week * 7)) +
                ") " +
                PrebuiltDaysString[dayNumber] +
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
        public string StringBuilder_Math_And_Prebuilt_Days()
        {
            var dayNumber = (int)Day;

            builder.Clear();
            builder.Append("(");
            builder.Append(dayNumber + (Week * 7));
            builder.Append(PrebuiltDaysString[dayNumber]);
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