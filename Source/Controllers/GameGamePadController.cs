using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class GameGamePadController(Game1 game) : IController {
  private GamePadState prevGamePadState = new();
  private GamePadState gamePadState = new();

  private ICommand quitCommand = new QuitCommand(game);
  private ICommand returnToMainMenuCommand = new ReturnToMenuAndResetCommand(game);
  private ICommand useItemPressedCommand = new PlayerUseItemCommand(game.StateGame.Player, UseType.Pressed);
  private ICommand useItemHeldCommand = new PlayerUseItemCommand(game.StateGame.Player, UseType.Held);
  private ICommand useItemReleasedCommand = new PlayerUseItemCommand(game.StateGame.Player, UseType.Released);
  private ICommand dieCommand = new PlayerDieCommand(game.StateGame.Player);
  private ICommand playerMoveUpCommand = new PlayerMoveUpCommand(game.StateGame.Player);
  private ICommand playerMoveDownCommand = new PlayerMoveDownCommand(game.StateGame.Player);
  private ICommand playerMoveLeftCommand = new PlayerMoveLeftCommand(game.StateGame.Player);
  private ICommand playerMoveRightCommand = new PlayerMoveRightCommand(game.StateGame.Player);
  private ICommand prevLevelCommand = new PreviousLevelCommand(game);
  private ICommand nextLevelCommand = new NextLevelCommand(game);

  public void Update(GameTime gameTime) {
    prevGamePadState = gamePadState;
    gamePadState = GamePad.GetState(PlayerIndex.One);

    // The bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.

    if (gamePadState.Buttons.X == ButtonState.Pressed && prevGamePadState.Buttons.X == ButtonState.Released) {
      quitCommand.Execute();
    }

    if (gamePadState.Buttons.B == ButtonState.Pressed && prevGamePadState.Buttons.B == ButtonState.Released) {
      returnToMainMenuCommand.Execute();
    }

    if (gamePadState.Buttons.A == ButtonState.Pressed && prevGamePadState.Buttons.A == ButtonState.Released) {
      useItemPressedCommand.Execute();
    }

    if (gamePadState.Buttons.Y == ButtonState.Pressed && prevGamePadState.Buttons.Y == ButtonState.Released) {
      dieCommand.Execute();
    }

    if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed && prevGamePadState.Buttons.RightShoulder == ButtonState.Released) {
      nextLevelCommand.Execute();
    }

    if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed && prevGamePadState.Buttons.LeftShoulder == ButtonState.Released) {
      prevLevelCommand.Execute();
    }

    if (gamePadState.DPad.Up == ButtonState.Pressed) {
      playerMoveUpCommand.Execute();
    }

    if (gamePadState.DPad.Down == ButtonState.Pressed) {
      playerMoveDownCommand.Execute();
    }

    if (gamePadState.DPad.Left == ButtonState.Pressed) {
      playerMoveLeftCommand.Execute();
    }

    if (gamePadState.DPad.Right == ButtonState.Pressed) {
      playerMoveRightCommand.Execute();
    }

    if (gamePadState.Buttons.B == ButtonState.Pressed) {
      useItemHeldCommand.Execute();
    }

    if (gamePadState.Buttons.B == ButtonState.Released && prevGamePadState.Buttons.B == ButtonState.Pressed) {
      useItemReleasedCommand.Execute();
    }
  }
}
