using System.Collections.Generic;
using GameProject.Animations;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

public class BombProjectile : IProjectile
{
    private Texture2D texture;
    private Vector2 position;
    private List<Rectangle> sourceRectangles;
    private Rectangle currentSourceRect;
    private Animation bombAnimation;
    private Vector2 origin;
    public bool IsExpired { get; private set; } = false;

  public void Draw(SpriteBatch spriteBatch)
    {
        origin = new Vector2(currentSourceRect.Width / 2, currentSourceRect.Height / 2);

        spriteBatch.Draw(
            texture,
            position,
            currentSourceRect,
            Color.White,
            0f,
            origin,
            1f,
            SpriteEffects.None,
            0f
        );
    }

    public void Update(GameTime gameTime)
    {
        bombAnimation.Update(gameTime);
        currentSourceRect = bombAnimation.CurrentFrame;
    }

    public void Instantiate(Vector2 startPosition, Vector2 direction, float velocity, float lifetime)
    {
        // Logic for instantiating the bomb projectile
        this.position = startPosition;
        this.IsExpired = false;
    }

    public BombProjectile(Texture2D texture, Vector2 startPosition)
    {
        this.texture = texture;
        this.position = startPosition;
        bombAnimation = new Animation(sourceRectangles, 5);
        currentSourceRect = bombAnimation.CurrentFrame;
    }
}
