using GameProject.GameStates;
using GameProject.Interfaces;
using GameProject.Sprites;

namespace GameProject.Factories;

internal class GameStateFactory {
  public static GameStateFactory Instance { get; private set; } = new GameStateFactory();

  private GameStateFactory() { }

  public IGameState CreateMenuState(Game1 gameObject) {
    return new StateMenu(gameObject);
  }

  public IGameState CreateGameState(Game1 gameObject) {
    return new StateGame(gameObject);
  }
}
