namespace YaConChess.ConApp;

public class Output {
  private readonly ConsoleColor FG = Console.ForegroundColor;
  private readonly ConsoleColor BG = Console.BackgroundColor;

  public void DisplayWithColor(string text, ConsoleColor bgColor, ConsoleColor? fgColor = null) {
    if (fgColor != null) Console.ForegroundColor = (ConsoleColor) fgColor;
    Console.BackgroundColor = bgColor;
    Console.Write(text);
    if (fgColor != null) Console.ForegroundColor = FG;
    Console.BackgroundColor = BG;
  }

  public static string Message = Messages.RequireMove;
}