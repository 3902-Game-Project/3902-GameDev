namespace GameProject.Interfaces;

internal enum ShapeType { Circle, Box }

internal interface IShape {
  ShapeType Type { get; }
}
