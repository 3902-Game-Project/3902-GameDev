using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

public class ItemWorldPickup : IWorldPickup {
  private IItem item;

  public ItemWorldPickup(IItem item) {
    this.item = item;
  }

  public void Draw(SpriteBatch spriteBatch) {
    item.Draw(spriteBatch);
  }

  public void Update(GameTime gameTime) {

  }

  public void OnPickup(Player player) {

  }
}
