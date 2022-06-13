namespace YaConChess.ConApp; 

public static class Logger {
  private const string FileName = "game.log";

  public static void Clear() {
    File.WriteAllText(FileName, string.Empty);
  }
  
  public static void WriteMove(string move) {
    List<string> linesList = new();
    if (File.Exists(FileName)) {
      linesList = File.ReadAllLines(FileName).ToList();
    }

    linesList.Add(move);
    
    File.WriteAllLines(FileName, linesList);
  }

  public static void UndoMove() {
    if (File.Exists(FileName)) {
      List<string> lines = File.ReadAllLines(FileName).ToList();
      if (lines.Count > 0) lines.RemoveAt(lines.Count - 1);
      File.WriteAllLines(FileName, lines);
    }
  }

  public static string[] ReadAllMoves() {
    List<string> moves = new();
    for (int i = 0; i < GetLength(); i++)
      moves.Add(ReadMove(i));
    return moves.ToArray();
  }

  private static string ReadMove(int i) {
    string move = string.Empty;
    if (File.Exists(FileName)) {
      string[] lines = File.ReadAllLines(FileName);
      if (lines.Length > 0) move = lines[i][..4];
    }

    return move;
  }

  private static int GetLength() {
    int length = 0;
    if (File.Exists(FileName)) {
      string[] lines = File.ReadAllLines(FileName);
      length = lines.Length;
    }

    return length;
  }
}