namespace YaConChess.ConApp; 

public interface IPiece {
  string Symbol { get; }
  Position Position { get; }
  ConsoleColor Color { get; }
  
  bool CanMoveTo(Position newPos, Cell[,] cells);
  void MoveTo(Position position);
}
