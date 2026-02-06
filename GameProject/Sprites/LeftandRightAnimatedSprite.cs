using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Interfaces;
using System.Collections.Generic;

namespace GameProject.Sprites;

public class LeftandRightAnimatedSprite : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private Vector2 startPosition;

    private List<Rectangle> sourceRectangles;

    private int direction = -1;
    private int speed = 3;
    private int sprintLength = 100;

    private int currentFrame;
    private double timer;
    private double FrameInterval = 0.2;

    public LeftandRightAnimatedSprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.startPosition = position;
        this.timer = 0;
        this.currentFrame = 0;

        sourceRectangles = new List<Rectangle>();
        sourceRectangles.Add(new Rectangle(65, 0, 25, 25));
        sourceRectangles.Add(new Rectangle(90, 0, 17, 25));
        sourceRectangles.Add(new Rectangle(108, 0, 22, 25));
    }

    public void Update(GameTime gameTime)
    {
        timer += gameTime.ElapsedGameTime.TotalSeconds;
        position.X += speed * direction;

        if (direction == -1 && position.X <= startPosition.X - sprintLength)
        {
            direction = 1;
        }
        else if (direction == 1 && position.X >= startPosition.X+100)
        {
            direction = -1;
        }

        if (timer >= FrameInterval)
        {
            currentFrame++;
            if (currentFrame >= sourceRectangles.Count)
            {
                currentFrame = 0;
            }
            timer = 0;
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = sourceRectangles[currentFrame];
        SpriteEffects effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        spriteBatch.Draw(
            texture,
            position,
            sourceRectangle,
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            effects,
            0f
        );
    }
}