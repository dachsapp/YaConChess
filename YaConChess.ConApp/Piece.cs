namespace YaConChess.ConApp; 

public abstract class Piece {
  public Position Position { get; protected set; } = null!;

  public void MoveTo(Position newPosition) => Position = newPosition;
}
