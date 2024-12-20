using System;

namespace RoXMX.Utilities
{
    internal class Logger
    {
        private static void Write(string Type, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Type = Type.ToLower();

            if (Type == "info") {
                Type = $"{Colours.BLUE}INFO{Colours.NORMAL}";
            } else if (Type == "warn") {
                Type = $"{Colours.YELLOW}WARN{Colours.NORMAL}";
            } else if (Type == "trace") {
                Type = $"{Colours.CYAN}TRACE{Colours.NORMAL}";
            } else if (Type == "error") {
                Type = $"{Colours.RED}ERROR{Colours.NORMAL}";
            }

            Console.WriteLine($"{Colours.GREY}[{timestamp} - {Colours.NORMAL}{Colours.BOLD}RoXMX{Colours.NOBOLD}{Colours.GREY}] {Colours.NORMAL}- {Type} - {message}");
        }

        public static void Info(params object[] args)
        {
            Write("INFO", string.Join(" ", args));
        }

        public static void Warn(params object[] args)
        {
            Write("WARN", string.Join(" ", args));
        }

        public static void Trace(params object[] args)
        {
            Write("TRACE", string.Join(" ", args));
        }

        public static void Error(params object[] args)
        {
            Write("ERROR", string.Join(" ", args));
        }
    }
}
