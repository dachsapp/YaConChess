namespace YaConChess.ConApp; 

public class NonJumpingPiece : Piece {
  protected bool NothingInBetween(bool canMove, Position newPos, Cell[,] cells) {
    int deltaX = newPos.X - Position.X;
    int deltaY = newPos.Y - Position.Y;

    Vector dirVector = new(deltaX, deltaY);
    double longerSide = dirVector.LongerSide;

    for (int i = 1; canMove && i < longerSide; i++) {
      Vector currentVector = dirVector.ApproxUnitVector * i;
      Position currentPosition = Position + currentVector;
      
      int row = Position.GetRowFromPosY(currentPosition.Y);
      int col = Position.GetColFromPosX(currentPosition.X);

      if (currentPosition != newPos && cells[row, col].Piece != null)
        canMove = false;
    }
    
    return canMove;
  }
  
}

