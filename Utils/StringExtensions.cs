namespace NeuroPlanets.Utils;

public static class StringExtensions {
    public static string Colorize(this string text, ConsoleColor color) {
        Console.ForegroundColor = color;
        return text;
    }
}