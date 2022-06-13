namespace YaConChess.ConApp;

public class Vector {
  private double X { get; }
  private double Y { get; }
  public double LongerSide => Math.Max(X, Y);
  
  public Vector ApproxUnitVector => this / LongerSide;
  
  public Vector(double x, double y) {
    X = x;
    Y = y;
  }


  public override string ToString() => $"v( {X} , {Y} )";

  public static Vector operator *(Vector v, double n) =>
    new(v.X * n, v.Y * n);
  
  public static Vector operator *(double n,Vector v) =>
    new(v.X * n, v.Y * n);

  public static Vector operator /(Vector v, double n) =>
    new(v.X / n, v.Y / n);

  public static Vector operator +(Vector v1, Vector v2) => 
    new(v1.X + v2.X, v1.Y + v2.Y);

  public static Vector operator -(Vector v1, Vector v2) => 
    new(v1.X - v2.X, v1.Y - v2.Y);

  public static double operator *(Vector v1, Vector v2) =>
    v1.X * v2.X + v1.Y * v2.Y;

  public static Position operator +(Vector v, Position p) =>
    new((char) (p.X + v.X), (int) (p.Y + v.Y));

  public static Position operator +(Position p, Vector v) =>
    new((char) (p.X + v.X), (int) (p.Y + v.Y));
}