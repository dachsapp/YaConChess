namespace YaConChess.ConApp;

public static class Messages {
  public const string RequireMove =
    "Input Your Move: ";

  public const string IncorrectMoveInput =
    "Incorrect Input: ";

  public const string IllegalMove =
    "Move is Illegal: ";

  public const string Checked =
    "Let's just, NOT: ";
}

public static class Action {
  public static readonly string[] Actions = { Undo, Resign, Draw };
  public const string Undo = "undo";
  public const string Resign = "resign";
  public const string Draw = "draw";
}