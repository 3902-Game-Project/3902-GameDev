using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Misc;

internal class GPTimer : IInstantaneousUpdatable {
  public double Time { get; private set; }

  public void Update(GameTime gameTime) {
    Time += gameTime.ElapsedGameTime.TotalSeconds;
  }
}
