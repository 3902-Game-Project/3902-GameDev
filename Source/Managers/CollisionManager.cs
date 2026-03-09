using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Managers;

public class CollisionManager {
  private List<ICollidable> colliders;

  public CollisionManager() {
    colliders = new List<ICollidable>();
  }

  public void Update(GameTime gameTime) {
    for (int i = 0; i < colliders.Count - 1; i++) {
      ICollidable c1 = colliders[i];
      for (int j = i + 1; j < colliders.Count; j++) {
        ICollidable c2 = colliders[j];

      }
    }
  }

  private bool CheckCollison(ICollidable c1, ICollidable c2) {
    
  }

  private bool BoxBoxCollision(BoxCollider b1, BoxCollider b2) {
    
  }

  private bool BoxCircleCollision(BoxCollider b, CircleCollider c) {
    
  }

  private bool CircleCircleCollision(CircleCollider c1, CircleCollider c2) {
    
  }
}
