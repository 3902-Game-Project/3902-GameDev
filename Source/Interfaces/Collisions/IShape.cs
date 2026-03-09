namespace GameProject.Interfaces;

public enum ShapeType { Circle, Box }

public interface IShape {
  ShapeType Type { get; }
}
