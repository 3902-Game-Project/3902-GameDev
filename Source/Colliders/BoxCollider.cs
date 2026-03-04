using System;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class BoxCollider : ICollider {
  private Vector2 _dimensions;
  private Vector2 _position;
  private float _rotation;

  public Vector2 dimensions {
    get => _dimensions;
    set { _dimensions = value; SetCorners(); }
  }

  public Vector2 position {
    get => _position;
    set { _position = value; SetCorners(); }
  }

  public float rotation {
    get => MathHelper.ToDegrees(rotation);
    set { _rotation = MathHelper.ToRadians(value); SetCorners(); }
  }

  public Vector2[] corners { get; private set; }

  public BoxCollider(Vector2 dimensions, Vector2 position) {
    _dimensions = dimensions;
    _position = position;
    corners = new Vector2[4];
    SetCorners();
  }

  public bool CheckCollision(ICollider other) {
    return false;
  }

  private void SetCorners() {
    Vector2[] localCorners = {
      new Vector2(_dimensions.X * 0.5f, -1 *_dimensions.Y * 0.5f),
      new Vector2(-1 * _dimensions.X * 0.5f, -1 * _dimensions.Y * 0.5f),
      new Vector2(_dimensions.X * 0.5f, _dimensions.Y * 0.5f),
      new Vector2(-1 * _dimensions.X * 0.5f, _dimensions.Y * 0.5f),
    };

    float cos = MathF.Cos(_rotation);
    float sin = MathF.Sin(_rotation);

    for (int i = 0; i < 4; i++) {
      corners[i] = new Vector2(
        _position.X + localCorners[i].X * cos - localCorners[i].Y * sin,
        _position.X + localCorners[i].X * sin + localCorners[i].Y * cos
      );
    }
  }
}
