using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Revolver : IItem {
    private Texture2D texture;
    private Vector2 position;
    private List<Rectangle> sourceRectangles;
    private int currentFrame;
    private double animationTimer;
    private int fps;
    private Vector2 origin;

    public void Draw(SpriteBatch spriteBatch) {
        origin = new Vector2(sourceRectangles[currentFrame].Width / 2, sourceRectangles[currentFrame].Height / 2);

        spriteBatch.Draw(
            texture,
            position,
            sourceRectangles[currentFrame],
            Color.White,
            0f,
            origin,
            1f,
            SpriteEffects.None,
            0f
        );
    }

    public void Update(GameTime gameTime) {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        animationTimer += dt;
        if (animationTimer >= 1.0 / fps)
        {
            currentFrame = (currentFrame + 1) % sourceRectangles.Count;
            animationTimer = 0;
        }
    }

    public void OnPickup() {
        
    }

    public Revolver(Texture2D texture, Vector2 startPosition) {
        this.texture = texture;
        this.position = startPosition;
        animationTimer = 0;
    }
}