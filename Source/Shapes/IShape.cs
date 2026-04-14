namespace GameProject.Collisions.Shapes;

internal enum ShapeType { Circle, Box }

internal interface IShape {
  ShapeType Type { get; }
}
