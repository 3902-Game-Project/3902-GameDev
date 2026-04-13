using GameProject.Controllers;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateItemScreenType(Game1 game) : IGameState {
  private static readonly string RETURN_TEXT = "Press I/GamePadB to return to game, Q/GamePadY to quit.";
  private IController keyboardController;
  private IController gamePadController;

  // NEW: A local variable to hold our font
  private SpriteFont mainFont;

  public int SelectedWeaponIndex { get; private set; } = 0;

  public void Initialize() {
    keyboardController = new ItemScreenKeyboardController(game, this);
    gamePadController = new ItemScreenGamePadController(game);
  }

  public void MoveCursorLeft() {
    Player player = game.StateGame.Player;
    if (player != null && player.Inventory.Weapons.Count > 0) {
      SelectedWeaponIndex--;
      if (SelectedWeaponIndex < 0) {
        SelectedWeaponIndex = player.Inventory.Weapons.Count - 1;
      }
    }
  }

  public void MoveCursorRight() {
    Player player = game.StateGame.Player;
    if (player != null && player.Inventory.Weapons.Count > 0) {
      SelectedWeaponIndex++;
      if (SelectedWeaponIndex >= player.Inventory.Weapons.Count) {
        SelectedWeaponIndex = 0;
      }
    }
  }

  public void EquipSelectedWeapon() {
    Player player = game.StateGame.Player;
    if (player != null) {
      player.Inventory.EquipWeapon(SelectedWeaponIndex);
    }
  }

  public void LoadContent(ContentManager content) {
    // NEW: Load the font directly here!
    mainFont = content.Load<SpriteFont>("MainFont");
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    gamePadController.Update(gameTime);
  }

  public void LowLevelDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
    graphicsDevice.Viewport = game.DefaultViewport;
    graphicsDevice.Clear(Color.CornflowerBlue);

    spriteBatch.Begin(
      SpriteSortMode.Deferred,
      BlendState.AlphaBlend,
      SamplerState.PointClamp,
      DepthStencilState.None,
      RasterizerState.CullNone
    );

    // NEW: Replaced game.Assets.MainFont with our local mainFont
    spriteBatch.DrawString(
      mainFont,
      RETURN_TEXT,
      new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height) * 0.5f + new Vector2(0.0f, 50.0f),
      Color.White,
      0.0f,
      mainFont.MeasureString(RETURN_TEXT) * 0.5f,
      1.0f,
      SpriteEffects.None,
      0.0f
    );

    Player player = game.StateGame.Player;

    if (player != null) {

      // --- 1. DRAW WEAPONS (Top Row) ---
      if (player.Inventory.Weapons.Count > 0) {
        for (int i = 0; i < player.Inventory.Weapons.Count; i++) {
          IItem weapon = player.Inventory.Weapons[i];

          float uiX = (game.Window.ClientBounds.Width / 2f) - 50 + (i * 100);
          float uiY = (game.Window.ClientBounds.Height / 2f) - 100;
          Vector2 uiPosition = new Vector2(uiX, uiY);

          float scale = (i == SelectedWeaponIndex) ? 1.2f : 1.0f;
          Color tint = (i == SelectedWeaponIndex) ? Color.White : Color.Gray;

          weapon.DrawUI(spriteBatch, uiPosition, scale, tint);

          // NEW: Replaced game.Assets.MainFont with our local mainFont
          if (i == player.Inventory.ActiveWeaponIndex) {
            spriteBatch.DrawString(mainFont, "Equipped", new Vector2(uiX - 30, uiY + 50), Color.Yellow);
          }
        }
      }

      // --- 2. DRAW GENERAL ITEMS (Grid underneath) ---
      if (player.Inventory.GeneralItems.Count > 0) {
        int startX = (game.Window.ClientBounds.Width / 2) - 200;
        int startY = (game.Window.ClientBounds.Height / 2) + 50;

        for (int i = 0; i < player.Inventory.GeneralItems.Count; i++) {
          IItem item = player.Inventory.GeneralItems[i];
          int column = i % 5;
          int row = i / 5;

          float uiX = startX + (column * 80);
          float uiY = startY + (row * 80);
          Vector2 uiPosition = new Vector2(uiX, uiY);

          item.DrawUI(spriteBatch, uiPosition, 1f, Color.White);
        }
      }
    }

    // --- 3. DRAW KEYS ---
    if (player.Inventory.Keys.Count > 0) {
      int keyStartX = game.Window.ClientBounds.Width - 150;
      int keyStartY = game.Window.ClientBounds.Height - 100;

      for (int i = 0; i < player.Inventory.Keys.Count; i++) {
        IItem key = player.Inventory.Keys[i];

        // Draw them in a little horizontal row
        Vector2 keyUiPosition = new Vector2(keyStartX + (i * 30), keyStartY);
        key.DrawUI(spriteBatch, keyUiPosition, 1f, Color.White);
      }
    }

    spriteBatch.End();
  }

  public void OnStateEnter() { }

  public void OnStateLeave() { }

  public void OnStateStartFadeIn() { }

  public void OnStateEndFadeOut() { }
}
