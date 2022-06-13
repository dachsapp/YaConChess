namespace YaConChess.ConApp.Pieces; 

public class Knight : Piece, IPiece {
  public Knight(Position position, ConsoleColor color) {
    Position = position;
    Color = color;
  }

  public string Symbol => Chess.Knight;
  public ConsoleColor Color { get; }

  public bool CanMoveTo(Position newPos, Cell[,] cells) =>
    (newPos.X == Position.X + 1 && newPos.Y == Position.Y + 2) ||
    (newPos.X == Position.X + 1 && newPos.Y == Position.Y - 2) ||
    (newPos.X == Position.X - 1 && newPos.Y == Position.Y + 2) ||
    (newPos.X == Position.X - 1 && newPos.Y == Position.Y - 2) ||
    (newPos.X == Position.X + 2 && newPos.Y == Position.Y + 1) ||
    (newPos.X == Position.X + 2 && newPos.Y == Position.Y - 1) ||
    (newPos.X == Position.X - 2 && newPos.Y == Position.Y + 1) ||
    (newPos.X == Position.X + 2 && newPos.Y == Position.Y - 1);
}