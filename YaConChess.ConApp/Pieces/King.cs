namespace YaConChess.ConApp.Pieces;

public class King : Piece, IPiece, IMovementTrackable {
  public bool HasMoved { get; set; } = false;
  
  public King(Position position, ConsoleColor color) {
    Position = position;
    Color = color;
  }

  public string Symbol => Chess.King;
  public ConsoleColor Color { get; }

  private ConsoleColor OppositeColor => Color == Chess.FWhite
    ? Chess.FBlack
    : Chess.FWhite;

  public bool CanMoveTo(Position newPos, Cell[,] cells) {
    bool samePos = Position == newPos;
    bool correctX = Position.X - 1 <= newPos.X && newPos.X <= Position.X + 1;
    bool correctY = Position.Y - 1 <= newPos.Y && newPos.Y <= Position.Y + 1;

    bool canMove = correctX || correctY && !samePos;

    return canMove;
  }

  public bool IsCheck(Cell[,] cells) {
    bool isCheck = false;

    for (int row = 0; !isCheck && row < cells.GetLength(0); row++)
    for (int col = 0; !isCheck && col < cells.GetLength(1); col++) {
      IPiece? piece = cells[row, col].Piece;
      
      if (piece != null && Symbol != piece.Symbol) {
        bool colorOpposite = piece.Color == OppositeColor;
        bool kingReachable = piece.CanMoveTo(Position, cells);
        if (colorOpposite && kingReachable) isCheck = true;
      }
    }

    return isCheck;
  }
}