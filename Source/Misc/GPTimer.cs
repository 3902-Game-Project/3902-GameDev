using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Misc;

internal class GPTimer : IGPUpdatable {
  public double Time { get; private set; }

  public void Update(GameTime gameTime) {
    Time += gameTime.ElapsedGameTime.TotalSeconds;
  }
}
