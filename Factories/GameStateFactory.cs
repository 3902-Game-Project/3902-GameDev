using GameProject.GameStates;
using GameProject.Interfaces;

namespace GameProject.Factories;

internal class GameStateFactory {
  public static GameStateFactory Instance { get; private set; } = new GameStateFactory();

  private GameStateFactory() { }

  public IGameState CreateMenuState(Game1 game) {
    return new StateMenu(game);
  }

  public IGameState CreateGameState(Game1 game) {
    return new StateGame(game);
  }
}
