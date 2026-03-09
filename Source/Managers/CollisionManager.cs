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
    BoxCollider b1 = c1 as BoxCollider;
    BoxCollider b2 = c2 as BoxCollider;
    CircleCollider s1 = c1 as CircleCollider;
    CircleCollider s2 = c2 as CircleCollider;
    if (b1 != null && b2 != null) return BoxBoxCollision(b1, b2);
    if (b1 != null && s2 != null) return BoxCircleCollision(b1, s2);
    if (b1 == null && b2 != null) return BoxCircleCollision(b2, s1);
    if (s1 != null && s2 != null) return CircleCircleCollision(s1, s2);
    return false;
  }

  private bool BoxBoxCollision(BoxCollider b1, BoxCollider b2) {
    
  }

  private bool BoxCircleCollision(BoxCollider b, CircleCollider c) {
    
  }

  private bool CircleCircleCollision(CircleCollider c1, CircleCollider c2) {
    
  }
}
