using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;
  private IController mouseController;

  public Player Player { get; private set; } = new Player(game);
  public List<IBlock> Blocks; // temporary for sprint2
  public int BlockNumber { get; set; } // temporary for sprint2
  public List<IItem> Items; // temporary for sprint2
  public int ItemNumber { get; set; } // temporary for sprint2

  public int EnemyNumber { get; set; } = 0;  // temporary for sprint2

  public List<IEnemy> Enemies { get; set; } = new List<IEnemy>();

  public IBlock BlockSprite { get; set; }
  public ISprite CurrentSprite { get; set; }
  public IItem ItemSprite { get; set; }

  public ILevelManager LevelManager { get; private set; }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    mouseController = new GameMouseController(game);
    LevelManager = new LevelManager(game);
    Blocks = new List<IBlock>(); // temporary for sprint2
    BlockNumber = Blocks.Count; // temporary for sprint2
    Items = new List<IItem>(); // temporary for sprint2
    ItemNumber = Items.Count; // temporary for sprint2
    LevelManager.Initialize();
  }

  public void LoadContent() {
    Player.LoadContent();
    game.BlockFactory.LoadAllTextures(game);

    var snake = EnemySpriteFactory.Instance.CreateSnakeSprite();
    Enemies.Add(snake);

    var shotgunner = EnemySpriteFactory.Instance.CreateShotgunnerSprite(game.ProjectileManager);
    Enemies.Add(shotgunner);

    var bat = EnemySpriteFactory.Instance.CreateBatSprite();
    Enemies.Add(bat);

    var rifleman = EnemySpriteFactory.Instance.CreateRifleSprite(game.ProjectileManager);
    Enemies.Add(rifleman);

    var tumbleweed = EnemySpriteFactory.Instance.CreateTumbleweedSprite();
    Enemies.Add(tumbleweed);

    var cactus = EnemySpriteFactory.Instance.CreateCactusSprite();
    Enemies.Add(cactus);

    var revolver = game.ItemSpriteFactory.CreateRevolver();
    Player.Inventory.PickupItem(revolver);

    var rifle = game.ItemSpriteFactory.CreateRifle();
    Player.Inventory.PickupItem(rifle);

    var shotgun = game.ItemSpriteFactory.CreateShotgun();
    Items.Add(shotgun);

    LevelManager.LoadContent(game.Content);
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    mouseController.Update(gameTime);
    LevelManager.Update(gameTime);
    Player.Update(gameTime);

    if (Blocks != null) {
      foreach (IBlock block in Blocks) {

        if (block.Collider is BoxCollider blockBox && Player.Collider is BoxCollider playerBox) {

          if (playerBox.CheckCollision(blockBox)) {
            float distanceX = playerBox.position.X - blockBox.position.X;
            float distanceY = playerBox.position.Y - blockBox.position.Y;
            float minDistanceX = (playerBox.dimensions.X / 2f) + (blockBox.dimensions.X / 2f);
            float minDistanceY = (playerBox.dimensions.Y / 2f) + (blockBox.dimensions.Y / 2f);

            float depthX = minDistanceX - System.Math.Abs(distanceX);
            float depthY = minDistanceY - System.Math.Abs(distanceY);

            if (depthX < depthY) {
              if (distanceX > 0) {
                Player.Position = new Vector2(Player.Position.X + depthX, Player.Position.Y);
              } else {
                Player.Position = new Vector2(Player.Position.X - depthX, Player.Position.Y);
              }
            } else {
              if (distanceY > 0) {
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y + depthY);
              } else {
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y - depthY);
              }
            }

            Player.Collider.position = Player.Position;
          }
        }
      }
    }

    if (Enemies != null && Enemies.Count > 0) {
      Enemies[EnemyNumber].Update(gameTime);
    }

    if (Items != null && Items.Count > 0) {
      Items[ItemNumber].Update(gameTime);
    }

    game.ProjectileManager.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    game.GraphicsDevice.Clear(Color.CornflowerBlue);
    game.SpriteBatch.Begin(
      SpriteSortMode.Deferred,
      BlendState.AlphaBlend,
      SamplerState.PointClamp,
      DepthStencilState.None,
      RasterizerState.CullNone
    );

    if (Blocks != null && Blocks.Count > 0 && BlockNumber < Blocks.Count) {
      Blocks[BlockNumber].Draw(game.SpriteBatch);
    }

    if (Items != null && Items.Count > 0 && ItemNumber < Items.Count) {
      Items[ItemNumber].Draw(game.SpriteBatch);
    }

    Player.Draw(game.SpriteBatch);

    if (Enemies != null && Enemies.Count > 0) {
      Enemies[EnemyNumber].Draw(game.SpriteBatch);
    }

    game.ProjectileManager.Draw(game.SpriteBatch);

    LevelManager.Draw(gameTime);

    game.SpriteBatch.End();
  }
}
