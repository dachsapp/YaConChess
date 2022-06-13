namespace YaConChess.ConApp.Pieces;

public class Pawn : Piece, IPiece {
  public Pawn(Position position, ConsoleColor color) {
    Position = position;
    Color = color;
  }

  public string Symbol =>
    Chess.Pawn;
  public ConsoleColor Color { get; }

  private ConsoleColor OppositeColor => Color switch {
    Chess.FWhite => Chess.FBlack,
    Chess.FBlack => Chess.FWhite,
    _ => Chess.NonColor
  };
  
  private int AdvanceFactor => Color switch {
    Chess.FWhite => 1,
    Chess.FBlack => -1,
    _ => 999
  };

  public bool CanMoveTo(Position newPos, Cell[,] cells) {
    int row = Position.GetRowFromPosY(newPos.Y);
    int col = Position.GetColFromPosX(newPos.X);

    bool isPosNull = cells[row, col].Piece == null;
    bool isPieceInTheWay = isPosNull && cells[row + AdvanceFactor, col].Piece != null;
    bool isPosOfOpponentPiece = !isPosNull && cells[row, col].Piece!.Color == OppositeColor;
    
    bool isAdvanceMove = newPos.Y == Position.Y + AdvanceFactor;
    bool isTwoStepMove = newPos.Y == Position.Y + AdvanceFactor * 2 && !isPieceInTheWay;
    bool isXOneStepMove = newPos.X == Position.X + 1 || newPos.X == Position.X - 1;
    bool isXEqual = newPos.X == Position.X;

    bool isSimpleMove = isXEqual && (isAdvanceMove || Position.Y is 2 or 7 && isTwoStepMove);
    bool isEatingMove = isAdvanceMove && isXOneStepMove;
    
    return isSimpleMove && !isPosOfOpponentPiece || isPosOfOpponentPiece && isEatingMove;
  }
}

