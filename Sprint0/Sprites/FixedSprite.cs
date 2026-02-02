using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Interfaces;
using System.Threading;

namespace Sprint0.Sprites
{
    public class FixedSprite : ISprite
    {
        private Texture2D texture;
        private Vector2 position;
        private int timer = 0;

        public FixedSprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            // Static sprites don't change, so this can be empty!
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(0, 0, 22, 30);
            spriteBatch.Draw(texture, position, sourceRect, Color.White);
        }
    }
}