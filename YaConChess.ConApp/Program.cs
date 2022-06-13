namespace YaConChess.ConApp;

public static class Program {
  public static void Main() {
    Console.Clear();
    Logger.Clear();

    ConsoleColor winner = RunGameAndGetWinner();
    DisplayWinner(winner);
  }

  private static ConsoleColor RunGameAndGetWinner() {
    var board = new Board();
    return RunBoardLoopAndGetWinner(board);
  }

  private static ConsoleColor RunBoardLoopAndGetWinner(Board board) {
    bool gameOn = true;
    while (gameOn) {
      string move = GetMove();
      gameOn = IsGameOn(move);
      bool movePossible = MakeMoveIfPossible(move, board);
      if (Output.Message != Messages.Checked) {
        Output.Message = movePossible
          ? Messages.RequireMove
          : Messages.IllegalMove;
      }

      if (move == Action.Undo) {
        Logger.UndoMove();
        Output.Message = Messages.RequireMove;
      }

      DisplayGame(board);
    }

    return Chess.NonColor;
  }

  private static void DisplayGame(Board board) {
    Console.Clear();
    board.Draw();
  }

  private static bool MakeMoveIfPossible(string move, Board board) {
    bool movePossible = !Action.Actions.Contains(move) && move.Length == 4;
    if (movePossible) {
      string[] splitMove = SplitMove(move);
      movePossible = board.MoveIfPossible(splitMove[0], splitMove[1]);
    }

    return movePossible;
  }

  private static bool IsGameOn(string move) =>
    move != Action.Draw && move != Action.Resign;

  private static void DisplayWinner(ConsoleColor winner) {
    string displayedText = winner switch {
      Chess.FWhite => "White Won!",
      Chess.FBlack => "Black Won!",
      _ => "Draw!"
    };
    Console.WriteLine(displayedText);
  }

  private static string[] SplitMove(string move) =>
    new[] { move[..2], move[^2..] };

  private static string GetMove() {
    Console.Write(Output.Message);
    string move = Console.ReadLine()!.ToLower();
    bool isMoveCorrect = Action.Actions.Contains(move);

    if (!isMoveCorrect && move.Length == 4) {
      bool charsValid = AreCharsValid(move);
      bool numbsValid = AreNumbsValid(move);

      isMoveCorrect = charsValid && numbsValid;
    }

    if (!isMoveCorrect) Output.Message = Messages.IncorrectMoveInput;
    return isMoveCorrect ? move : string.Empty;
  }

  private static bool AreCharsValid(string move) =>
    Position.IsXValid(move[0]) && Position.IsXValid(move[2]);

  private static bool AreNumbsValid(string move) =>
    Position.IsYValid(move[1] - '0') && Position.IsYValid(move[3] - '0');
}