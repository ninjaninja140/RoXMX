using System;

namespace RoXMX.Utilities
{
    internal static class Logger
    {
        private static void Write(string type, string message)
        {
            Console.WriteLine(FormatMessage(type, message));
        }

        private static void WriteBack(string type, string message)
        {
            Console.Write($"\r{FormatMessage(type, message)}");
        }

        private static string FormatMessage(string type, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string formattedType = FormatType(type);
            return $"{Colours.GREY}[{timestamp} {formattedType} {Colours.NORMAL}{Colours.BOLD}RoXMX{Colours.NOBOLD}{Colours.GREY}] {Colours.NORMAL}{message}";
        }

        private static string FormatType(string type)
        {
            return type.ToLower() switch
            {
                "info" => $"{Colours.BLUE}INFO{Colours.NORMAL}",
                "warn" => $"{Colours.YELLOW}WARN{Colours.NORMAL}",
                "trace" => $"{Colours.CYAN}TRACE{Colours.NORMAL}",
                "error" => $"{Colours.RED}ERROR{Colours.NORMAL}",
                _ => $"{Colours.NORMAL}UNKNOWN{Colours.NORMAL}"
            };
        }

        public static void Info(params object[] args) => Write("INFO", string.Join(" ", args));
        public static void Warn(params object[] args) => Write("WARN", string.Join(" ", args));
        public static void Trace(params object[] args) => Write("TRACE", string.Join(" ", args));
        public static void Error(params object[] args) => Write("ERROR", string.Join(" ", args));

        public static void BackInfo(params object[] args) => WriteBack("INFO", string.Join(" ", args));
        public static void BackWarn(params object[] args) => WriteBack("WARN", string.Join(" ", args));
        public static void BackTrace(params object[] args) => WriteBack("TRACE", string.Join(" ", args));
        public static void BackError(params object[] args) => WriteBack("ERROR", string.Join(" ", args));
    }
}
