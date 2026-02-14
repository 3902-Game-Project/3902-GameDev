using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles {
    public class BulletDefault : IProjectile {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle sourceRectangle;
        private Vector2 origin;
        private Vector2 direction;
        private float velocity;

        public void Draw(SpriteBatch spriteBatch) {
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

            spriteBatch.Draw(
                texture,
                position,
                sourceRectangle,
                Color.White,
                0f,
                origin,
                1f,
                SpriteEffects.None,
                0f
            );
        }

        public void Update(GameTime gameTime) {
            // Logic for updating the bullet's position and state
            position += direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Instantiate(Vector2 startPosition, Vector2 direction, float velocity) {
            this.position = startPosition;
            this.direction = direction;
            this.velocity = velocity;
            // Logic for setting the bullet's direction and velocity
        }

        public BulletDefault(Texture2D texture) {
            this.texture = texture;
            this.sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }
    }
}