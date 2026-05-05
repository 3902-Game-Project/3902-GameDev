using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Factories.Controller;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.HelperFuncs;
using GameProject.Items;
using GameProject.Misc;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateItemScreenType(Game1 game) : IGameState {
  private IController<Keys> keyboardController;
  private IController<GPGamePadButtons> gamePadController;
  private Texture2D blankTexture;
  public bool InWeaponMenu { get; private set; } = true;
  public int SelectedWeaponIndex { get; private set; } = 0;
  public int SelectedBackpackIndex { get; private set; } = 0;

  public void Initialize() {
    keyboardController = ItemScreenControllerFactory.CreateKeyboardController(game, this, game.StateGame.Player);
    gamePadController = ItemScreenControllerFactory.CreateGamePadController(game, this, game.StateGame.Player);
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
      if (SelectedBackpackIndex >= Constants.BACKPACK_COLUMNS) {
        SelectedBackpackIndex -= Constants.BACKPACK_COLUMNS;
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
      if (player != null && SelectedBackpackIndex + Constants.BACKPACK_COLUMNS < player.Inventory.GeneralItems.Count) {
        SelectedBackpackIndex += Constants.BACKPACK_COLUMNS;
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

  public void LowLevelDraw(LowLevelDrawParams drawData) {
    drawData.ClearWindowCallback(new(25, 28, 33));

    drawData.SpriteBatch.Begin(
      SpriteSortMode.Deferred,
      BlendState.AlphaBlend,
      SamplerState.PointClamp,
      DepthStencilState.None,
      RasterizerState.CullNone
    );

    SpriteFont font = MiscAssetStore.Instance.MainFont;
    int centerX = Constants.WINDOW_WIDTH / 2;

    TextFuncs.DrawCenteredString(
      spriteBatch: drawData.SpriteBatch,
      position: new Vector2(centerX, 49.0f),
      text: "- INVENTORY -",
      color: Color.Gold
    );

    Player player = game.StateGame.Player;

    if (player != null) {
      // --- DRAW WEAPONS ---
      TextFuncs.DrawCenteredString(
        spriteBatch: drawData.SpriteBatch,
        position: new Vector2(centerX, 129.0f),
        text: "WEAPONS",
        color: Color.LightGray
      );

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
        drawData.SpriteBatch.Draw(blankTexture, new Rectangle((int) uiX, (int) uiY, weaponSlotSize, weaponSlotSize), slotColor);

        float scale = (InWeaponMenu && i == SelectedWeaponIndex) ? 2.5f : 2.0f;
        Color tint = (InWeaponMenu && i == SelectedWeaponIndex) ? Color.White : Color.Gray;

        weapon.DrawUI(drawData.SpriteBatch, uiPosition + new Vector2(20, 20), scale, tint);

        if (i == player.Inventory.ActiveWeaponIndex) {
          drawData.SpriteBatch.DrawString(font, "Equipped", new Vector2(uiX + 15, uiY + weaponSlotSize + 10), Color.MediumSpringGreen);
        }
      }

      // --- DRAW BACKPACK ---
      TextFuncs.DrawCenteredString(
        spriteBatch: drawData.SpriteBatch,
        position: new Vector2(centerX, 359.0f),
        text: "BACKPACK",
        color: Color.LightGray
      );

      int itemSlotSize = 80;
      int itemSpacing = 100;
      int gridStartX = centerX - (Constants.BACKPACK_COLUMNS * itemSpacing / 2) + (itemSpacing / 2) - (itemSlotSize / 2);
      int gridStartY = 400;

      for (int i = 0; i < 10; i++) {
        int column = i % Constants.BACKPACK_COLUMNS;
        int row = i / Constants.BACKPACK_COLUMNS;
        float uiX = gridStartX + (column * itemSpacing);
        float uiY = gridStartY + (row * itemSpacing);

        bool isHovered = !InWeaponMenu && i == SelectedBackpackIndex && i < player.Inventory.GeneralItems.Count;
        Color slotColor = isHovered ? new Color(70, 70, 70, 200) : new Color(40, 40, 40, 200);

        drawData.SpriteBatch.Draw(blankTexture, new Rectangle((int) uiX, (int) uiY, itemSlotSize, itemSlotSize), slotColor);

        if (i < player.Inventory.GeneralItems.Count) {
          IItem item = player.Inventory.GeneralItems[i];
          Color tint = isHovered ? Color.White : Color.Gray;
          float scale = isHovered ? .21f : .19f;

          if (item is KeyItem) {
            scale = isHovered ? 3.0f : 2.8f;
          }

          item.DrawUI(drawData.SpriteBatch, new Vector2(uiX, uiY) + new Vector2(10, 10), scale, tint);
        }
      }
    }

    TextFuncs.DrawCenteredString(
      spriteBatch: drawData.SpriteBatch,
      position: new Vector2(centerX, Constants.WINDOW_HEIGHT - 40.0f),
      text:
        "Press I / GamePadB to return to game\n" +
        "Press Q / GamePadY to quit",
      color: Color.DarkGray
    );

    drawData.SpriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }
  public void OnStateLeave(bool nextStateIsCurrentState) { }
  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }
  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
