using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

public class ItemWorldPickup : BaseWorldPickup, ICollidable {
  private IItem item;

  public ItemWorldPickup(IItem item) : base(item.Position) {
    this.item = item;
  }

  public override void Draw(SpriteBatch spriteBatch) {
    item.Draw(spriteBatch);
  }

  public override void Update(GameTime gameTime) {
    item.Position = Position;
  }
}
