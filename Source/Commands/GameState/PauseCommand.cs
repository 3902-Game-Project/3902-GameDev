using GameProject.GameStates;

namespace GameProject.Commands;

internal class PauseCommand(Game1 game) : IGPCommand {
  internal void Execute() {
    game.StateMachine.ChangeState(GameState.StatePause);
  }
}
