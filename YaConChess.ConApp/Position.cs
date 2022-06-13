namespace YaConChess.ConApp;

public class Position {
  public char X { get; } = '-';
  public int Y { get; } = -1;

  public Position(char x, int y) {
    if (x is >= 'a' and <= 'h' && y is >= 1 and <= 8) {
      X = x;
      Y = y;
    }
  }

  public static int GetRowFromPosY(int y) => Board.BoardSide - y;
  public static int GetColFromPosX(char x) => x - 'a';

  public static bool IsYValid(int y) => y is >= 1 and <= 8;
  public static bool IsXValid(char x) => x is >= 'a' and <= 'z';

  public override string ToString() => $"{X}{Y}";

  public override bool Equals(object? obj) {
    bool equals = false;
  
    if (!ReferenceEquals(null, obj)) {
      bool equalReference = ReferenceEquals(this, obj);
      bool equalType = obj.GetType() == GetType();
      bool equalPosition = this == (Position) obj;
      equals = equalReference || equalType && equalPosition;
    }
  
    return equals;
  }

  public override int GetHashCode() => HashCode.Combine(X, Y);

  public static bool operator ==(Position a, Position b) => a.X == b.X && a.Y == b.Y;
  public static bool operator !=(Position a, Position b) => a.X != b.X || a.Y != b.Y;
}
