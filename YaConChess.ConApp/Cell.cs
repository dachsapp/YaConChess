namespace YaConChess.ConApp;

public class Cell {
  public IPiece? Piece;
  public Cell(IPiece? piece = null) => Piece = piece;

  public override string ToString()
    => Piece == null ? Chess.Field : Piece.Symbol;
}