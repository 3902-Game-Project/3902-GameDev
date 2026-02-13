using GameProject.Interfaces;

namespace GameProject.Globals;

public class GlobalVarStore(Game1 game) : IGlobalData {
  public AssetStore Assets { get; private set; } = new AssetStore(game);

  public void Initialize() {
    Assets.Initialize();
  }

  public void LoadContent() {
    Assets.LoadContent();
  }
}
