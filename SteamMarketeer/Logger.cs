using System;

public static class Logger
{
    public static void Skip()
    {
        Console.WriteLine();
    }

    public static void Log(string log, ConsoleColor? color = null)
    {
        if (color.HasValue)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color.Value;
            Console.WriteLine(log);
            Console.ForegroundColor = prevColor;
        }
        else
        {
            Console.WriteLine(log);
        }
    }

    public static void Warn(string log)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARN] {log}");
        Console.ForegroundColor = prevColor;
    }

    public static void Error(string log)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {log}");
        Console.ForegroundColor = prevColor;
    }
}
