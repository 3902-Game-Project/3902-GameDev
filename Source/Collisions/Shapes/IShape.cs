namespace GameProject.Collisions.Shapes;

internal enum ShapeType { Circle, Box }

internal interface IShape {
  internal ShapeType Type { get; }
}
