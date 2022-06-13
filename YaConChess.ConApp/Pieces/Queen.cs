namespace YaConChess.ConApp.Pieces;

public class Queen : NonJumpingPiece, IPiece {
  public Queen(Position position, ConsoleColor color) {
    Position = position;
    Color = color;
  }

  public string Symbol => Chess.Queen;
  public ConsoleColor Color { get; }

  public bool CanMoveTo(Position newPos, Cell[,] cells) {
    int deltaX = newPos.X - Position.X;
    int deltaY = newPos.Y - Position.Y;
    
    bool isSameY = newPos.Y == Position.Y;
    bool isSameX = newPos.X == Position.X;

    bool isHorizontalMove = IsHorizontalMove(isSameX, isSameY);
    bool isVerticalMove = IsVerticalMove(isSameX, isSameY);
    bool isDiagonalMove = IsDiagonalMove(deltaX, deltaY);

    bool canMove = isHorizontalMove != isVerticalMove || isDiagonalMove;
    return NothingInBetween(canMove, newPos, cells);
  }

  private static bool IsHorizontalMove(bool isSameX, bool isSameY)
    => isSameY && !isSameX;

  private static bool IsVerticalMove(bool isSameX, bool isSameY)
    => !isSameY && isSameX;

  private static bool IsDiagonalMove(int deltaX, int deltaY) 
    => Math.Abs(deltaX) == Math.Abs(deltaY);
}