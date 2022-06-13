namespace YaConChess.ConApp.Pieces;

public class Bishop : NonJumpingPiece, IPiece {
  public Bishop(Position position, ConsoleColor color) {
    Position = position;
    Color = color;
  }

  public string Symbol => Chess.Bishop;
  public ConsoleColor Color { get; }

  public bool CanMoveTo(Position newPos, Cell[,] cells) {
    int deltaX = newPos.X - Position.X;
    int deltaY = newPos.Y - Position.Y;
    bool canMove = Math.Abs(deltaX) == Math.Abs(deltaY);

    return NothingInBetween(canMove, newPos, cells);
  }
}