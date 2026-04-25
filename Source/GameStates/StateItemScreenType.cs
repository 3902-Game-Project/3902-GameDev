using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Items;
using GameProject.Misc;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateItemScreenType(Game1 game) : IGameState {
  private static readonly string RETURN_TEXT = "Press I / GamePad B to return to game";
  private IController keyboardController;
  private IController gamePadController;
  private Texture2D blankTexture;
  public bool InWeaponMenu { get; private set; } = true;
  public int SelectedWeaponIndex { get; private set; } = 0;
  public int SelectedBackpackIndex { get; private set; } = 0;
  private const int BACKPACK_COLUMNS = 5;

  public void Initialize() {
    keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.I, new ReturnToGameNoFadeCommand(game) },
                
        // Navigation bindings
        { Keys.W, new MenuMoveUpCommand(this) },
        { Keys.Up, new MenuMoveUpCommand(this) },
        { Keys.S, new MenuMoveDownCommand(this) },
        { Keys.Down, new MenuMoveDownCommand(this) },
        { Keys.A, new MenuMoveLeftCommand(this) },
        { Keys.Left, new MenuMoveLeftCommand(this) },
        { Keys.D, new MenuMoveRightCommand(this) },
        { Keys.Right, new MenuMoveRightCommand(this) },
                
        // Action bindings
        { Keys.Enter, new MenuEquipCommand(this) },
        { Keys.Space, new MenuEquipCommand(this) },
      }
    );

    gamePadController = new GamePadController(
      pressedMappings: new Dictionary<Buttons, IGPCommand> {
        { Buttons.X, new QuitCommand(game) },
        { Buttons.B, new ReturnToGameNoFadeCommand(game) },
        { Buttons.DPadUp, new MenuMoveUpCommand(this) },
        { Buttons.DPadDown, new MenuMoveDownCommand(this) },
        { Buttons.DPadLeft, new MenuMoveLeftCommand(this) },
        { Buttons.DPadRight, new MenuMoveRightCommand(this) },
        { Buttons.A, new MenuEquipCommand(this) },
      }
    );
  }

  public void MoveCursorLeft() {
    Player player = game.StateGame.Player;
    if (player == null) return;

    if (InWeaponMenu) {
      if (player.Inventory.Weapons.Count > 0) {
        SelectedWeaponIndex--;
        if (SelectedWeaponIndex < 0) SelectedWeaponIndex = player.Inventory.Weapons.Count - 1;
      }
    } else {
      if (player.Inventory.GeneralItems.Count > 0) {
        SelectedBackpackIndex--;
        if (SelectedBackpackIndex < 0) SelectedBackpackIndex = player.Inventory.GeneralItems.Count - 1;
      }
    }
  }

  public void MoveCursorRight() {
    Player player = game.StateGame.Player;
    if (player == null) return;

    if (InWeaponMenu) {
      if (player.Inventory.Weapons.Count > 0) {
        SelectedWeaponIndex++;
        if (SelectedWeaponIndex >= player.Inventory.Weapons.Count) SelectedWeaponIndex = 0;
      }
    } else {
      if (player.Inventory.GeneralItems.Count > 0) {
        SelectedBackpackIndex++;
        if (SelectedBackpackIndex >= player.Inventory.GeneralItems.Count) SelectedBackpackIndex = 0;
      }
    }
  }

  public void MoveCursorUp() {
    if (!InWeaponMenu) {
      if (SelectedBackpackIndex >= BACKPACK_COLUMNS) {
        SelectedBackpackIndex -= BACKPACK_COLUMNS;
      } else {
        InWeaponMenu = true;
      }
    }
  }

  public void MoveCursorDown() {
    if (InWeaponMenu) {
      InWeaponMenu = false;
    } else {
      Player player = game.StateGame.Player;
      if (player != null && SelectedBackpackIndex + BACKPACK_COLUMNS < player.Inventory.GeneralItems.Count) {
        SelectedBackpackIndex += BACKPACK_COLUMNS;
      }
    }
  }

  public void EquipSelectedWeapon() {
    Player player = game.StateGame.Player;
    if (player == null) return;

    if (InWeaponMenu) {
      if (player.Inventory.Weapons.Count > 0) {
        player.Inventory.EquipWeapon(SelectedWeaponIndex);
      }
    } else {
      if (player.Inventory.GeneralItems.Count > 0 && SelectedBackpackIndex < player.Inventory.GeneralItems.Count) {
        IItem selectedItem = player.Inventory.GeneralItems[SelectedBackpackIndex];

        if (selectedItem is KeyItem) {
          new PlayerUseKeyCommand(player, default).Execute();
        } else {
          selectedItem.Use(default);
          player.Inventory.RemoveGeneralItem(selectedItem);
        }
        if (SelectedBackpackIndex >= player.Inventory.GeneralItems.Count && SelectedBackpackIndex > 0) {
          SelectedBackpackIndex--;
        }
        new ReturnToGameNoFadeCommand(game).Execute();
      }
    }
  }

  public void LoadContent(ContentManager content) {
    blankTexture = new Texture2D(game.GraphicsDevice, 1, 1);
    blankTexture.SetData([Color.White]);
  }

  public void Update(double deltaTime) {
    keyboardController.Update();
    gamePadController.Update();
  }

  public void LowLevelDraw(GraphicsDevice graphicsDevice, RenderTargetTracker renderTargetTracker, SpriteBatch spriteBatch) {
    graphicsDevice.Viewport = game.DefaultViewport;
    graphicsDevice.Clear(new Color(25, 28, 33));

    spriteBatch.Begin(
        SpriteSortMode.Deferred,
        BlendState.AlphaBlend,
        SamplerState.PointClamp,
        DepthStencilState.None,
        RasterizerState.CullNone
    );

    SpriteFont font = MiscAssetStore.Instance.MainFont;
    //int screenWidth = game.Window.ClientBounds.Width;
    //int screenHeight = game.Window.ClientBounds.Height;
    int screenWidth = game.DefaultViewport.Width;
    int screenHeight = game.DefaultViewport.Height;
    int centerX = screenWidth / 2;

    spriteBatch.DrawString(font, "- INVENTORY -", new Vector2(centerX - (font.MeasureString("- INVENTORY -").X / 2), 40), Color.Gold);

    Player player = game.StateGame.Player;

    if (player != null) {
      // --- DRAW WEAPONS ---
      spriteBatch.DrawString(font, "WEAPONS", new Vector2(centerX - (font.MeasureString("WEAPONS").X / 2), 120), Color.LightGray);

      int weaponSlotSize = 120;
      int weaponSpacing = 160;
      int weaponStartX = centerX - (player.Inventory.Weapons.Count * weaponSpacing / 2) + (weaponSpacing / 2) - (weaponSlotSize / 2);
      int weaponStartY = 160;

      for (int i = 0; i < player.Inventory.Weapons.Count; i++) {
        IItem weapon = player.Inventory.Weapons[i];
        float uiX = weaponStartX + (i * weaponSpacing);
        float uiY = weaponStartY;
        Vector2 uiPosition = new(uiX, uiY);

        Color slotColor = (InWeaponMenu && i == SelectedWeaponIndex) ? new Color(70, 70, 70, 200) : new Color(40, 40, 40, 200);
        spriteBatch.Draw(blankTexture, new Rectangle((int) uiX, (int) uiY, weaponSlotSize, weaponSlotSize), slotColor);

        float scale = (InWeaponMenu && i == SelectedWeaponIndex) ? 2.5f : 2.0f;
        Color tint = (InWeaponMenu && i == SelectedWeaponIndex) ? Color.White : Color.Gray;

        weapon.DrawUI(spriteBatch, uiPosition + new Vector2(20, 20), scale, tint);

        if (i == player.Inventory.ActiveWeaponIndex) {
          spriteBatch.DrawString(font, "Equipped", new Vector2(uiX + 15, uiY + weaponSlotSize + 10), Color.MediumSpringGreen);
        }
      }

      // --- DRAW BACKPACK ---
      spriteBatch.DrawString(font, "BACKPACK", new Vector2(centerX - (font.MeasureString("BACKPACK").X / 2), 350), Color.LightGray);

      int itemSlotSize = 80;
      int itemSpacing = 100;
      int gridStartX = centerX - (BACKPACK_COLUMNS * itemSpacing / 2) + (itemSpacing / 2) - (itemSlotSize / 2);
      int gridStartY = 400;

      for (int i = 0; i < 10; i++) {
        int column = i % BACKPACK_COLUMNS;
        int row = i / BACKPACK_COLUMNS;
        float uiX = gridStartX + (column * itemSpacing);
        float uiY = gridStartY + (row * itemSpacing);

        bool isHovered = !InWeaponMenu && i == SelectedBackpackIndex && i < player.Inventory.GeneralItems.Count;
        Color slotColor = isHovered ? new Color(70, 70, 70, 200) : new Color(40, 40, 40, 200);

        spriteBatch.Draw(blankTexture, new Rectangle((int) uiX, (int) uiY, itemSlotSize, itemSlotSize), slotColor);

        if (i < player.Inventory.GeneralItems.Count) {
          IItem item = player.Inventory.GeneralItems[i];
          Color tint = isHovered ? Color.White : Color.Gray;
          float scale = isHovered ? .21f : .19f;

          if (item is KeyItem) {
            scale = isHovered ? 3.0f : 2.8f;
          }

          item.DrawUI(spriteBatch, new Vector2(uiX, uiY) + new Vector2(10, 10), scale, tint);
        }
      }
    }

    Vector2 returnSize = font.MeasureString(RETURN_TEXT);
    spriteBatch.DrawString(font, RETURN_TEXT, new Vector2(centerX - (returnSize.X / 2), screenHeight - 60), Color.DarkGray);

    spriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }
  public void OnStateLeave(bool nextStateIsCurrentState) { }
  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }
  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
