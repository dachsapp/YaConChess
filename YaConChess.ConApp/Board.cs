using YaConChess.ConApp.Pieces;

namespace YaConChess.ConApp;

public class Board {
  public const int BoardSide = 8;

  private readonly Output _outputModule = new();
  private readonly Cell[,] _cells = new Cell[BoardSide, BoardSide];

  public Board() {
    InitBoard();
    Draw();
  }

  private void InitBoard() {
    InitEmptyBoard();
    AddWhitePieces();
    AddBlackPieces();
  }

  private void InitEmptyBoard() {
    for (int row = 0; row < _cells.GetLength(0); row++)
    for (int col = 0; col < _cells.GetLength(1); col++)
      _cells[row, col] = new Cell();
  }

  private void AddWhitePieces() {
    const int row = 1;
    const ConsoleColor color = Chess.FWhite;
    GeneratePieceLine(row, color);
    GeneratePawnLine(row + 1, color);
  }

  private void AddBlackPieces() {
    const int row = 8;
    const ConsoleColor color = Chess.FBlack;
    GeneratePieceLine(row, color);
    GeneratePawnLine(row - 1, color);
  }

  private void GeneratePieceLine(int row, ConsoleColor color) {
    AddPiece(new Rook(new Position('a', row), color));
    AddPiece(new Knight(new Position('b', row), color));
    AddPiece(new Bishop(new Position('c', row), color));
    AddPiece(new Queen(new Position('d', row), color));
    AddPiece(new King(new Position('e', row), color));
    AddPiece(new Bishop(new Position('f', row), color));
    AddPiece(new Knight(new Position('g', row), color));
    AddPiece(new Rook(new Position('h', row), color));
  }

  private void GeneratePawnLine(int row, ConsoleColor color) {
    for (char i = 'a'; i < 'a' + BoardSide; i++)
      AddPiece(new Pawn(new Position(i, row), color));
  }

  private void AddPiece(IPiece piece) {
    int row = Position.GetRowFromPosY(piece.Position.Y);
    int col = Position.GetColFromPosX(piece.Position.X);

    _cells[row, col].Piece = piece;
  }

  public void Draw() {
    RePlayMoves(Logger.ReadAllMoves());

    for (int row = 0; row < BoardSide; row++) {
      for (int col = 0; col < BoardSide; col++) {
        ConsoleColor bgColor = row % 2 == col % 2 ? Chess.White : Chess.Black;
        Cell cell = _cells[row, col];
        string cellString = cell.ToString();
        _outputModule.DisplayWithColor(cellString, bgColor, cell.Piece?.Color);
      }

      Console.Write($" {8 - row}");
      Console.WriteLine();
    }

    for (char i = 'a'; i - 'a' < BoardSide; i++) Console.Write($" {i} ");
    Console.WriteLine();
  }

  public bool MoveIfPossible(string cell1, string cell2) {
    var pos1 = new Position(cell1[0], cell1[1] - '0');
    var pos2 = new Position(cell2[0], cell2[1] - '0');

    int row1 = Position.GetRowFromPosY(pos1.Y);
    int col1 = Position.GetColFromPosX(pos1.X);
    int row2 = Position.GetRowFromPosY(pos2.Y);
    int col2 = Position.GetColFromPosX(pos2.X);

    bool movePossible = false;
    bool piece1NotNull = _cells[row1, col1].Piece != null;
    
    if (piece1NotNull) {
      IPiece piece1 = _cells[row1, col1].Piece!;
      IPiece? piece2 = _cells[row2, col2].Piece;
      
      bool canMove = piece1.CanMoveTo(pos2, _cells);
      bool piece2Null = piece2 == null;

      if (canMove && (piece2Null || piece1.Color != piece2!.Color)) {
        Logger.WriteMove($"{cell1}{cell2}");
        Move(cell1, cell2);

        movePossible = !IsOwnColorChecked(_cells[row2, col2].Piece!);

        IMovementTrackable? movementTrackable = null;
        bool movementTrackableWasFalse = false;
        Output.Message = Messages.RequireMove;
        if (piece1 is IMovementTrackable trackable) {
          movementTrackable = trackable;
          movementTrackableWasFalse = !movementTrackable.HasMoved == false;
          movementTrackable.HasMoved = true;
        }

        if (!movePossible) {
          Logger.UndoMove();
          Output.Message = Messages.Checked;
          if (movementTrackable != null && movementTrackableWasFalse)
            movementTrackable.HasMoved = false;
        }
      }
    }

    return movePossible;
  }

  private bool IsOwnColorChecked(IPiece currentPiece) {
    bool isChecked = false;

    for (int row = 0; !isChecked && row < _cells.GetLength(0); row++)
    for (int col = 0; !isChecked && col < _cells.GetLength(1); col++) {
      bool pieceNotNull = _cells[row, col].Piece != null;

      if (pieceNotNull) {
        IPiece piece = _cells[row, col].Piece!;
        bool sameColor = piece.Color == currentPiece.Color;
        bool isKing = piece.Symbol == Chess.King;

        if (sameColor && isKing && (piece as King)!.IsCheck(_cells))
          isChecked = true;
      }
    }

    return isChecked;
  }

  private void Move(string cell1, string cell2) {
    var pos1 = new Position(cell1[0], cell1[1] - '0');
    var pos2 = new Position(cell2[0], cell2[1] - '0');

    int row1 = Position.GetRowFromPosY(pos1.Y);
    int col1 = Position.GetColFromPosX(pos1.X);
    int row2 = Position.GetRowFromPosY(pos2.Y);
    int col2 = Position.GetColFromPosX(pos2.X);

    _cells[row1, col1].Piece?.MoveTo(pos2);
    _cells[row2, col2].Piece = _cells[row1, col1].Piece;
    _cells[row1, col1].Piece = null;
  }

  private void RePlayMoves(string[] moves) {
    InitBoard();
    foreach (string move in moves) {
      string cell1 = move[..2];
      string cell2 = move[^2..];
      Move(cell1, cell2);
    }
  }
}