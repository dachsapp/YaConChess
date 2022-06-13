namespace YaConChess.ConApp.Pieces;

public class Rook : NonJumpingPiece, IPiece, IMovementTrackable {
  public bool HasMoved { get; set; } = false;
  
  public Rook(Position position, ConsoleColor color) {
    Position = position;
    Color = color;
  }

  public string Symbol => Chess.Rook;
  public ConsoleColor Color { get; }

  public bool CanMoveTo(Position newPos, Cell[,] cells) {
    bool isSameY = newPos.Y == Position.Y;
    bool isSameX = newPos.X == Position.X;

    bool isHorizontalMove = IsHorizontalMove(isSameX, isSameY);
    bool isVerticalMove = IsVerticalMove(isSameX, isSameY);

    bool canMove = isHorizontalMove != isVerticalMove;
    return NothingInBetween(canMove, newPos, cells);
  }

  private static bool IsHorizontalMove(bool isSameX, bool isSameY)
    => isSameY && !isSameX;

  private static bool IsVerticalMove(bool isSameX, bool isSameY)
    => !isSameY && isSameX;
}

