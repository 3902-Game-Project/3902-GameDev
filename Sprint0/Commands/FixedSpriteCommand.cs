using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Interfaces;
using Sprint0.Sprites;

namespace Sprint0.Commands
{
    public class FixedSpriteCommand : ICommand
    {
        private Game1 myGame;
        public FixedSpriteCommand(Game1 game)
        {
            myGame = game;
        }
        public void Execute()
        {
            Texture2D texture = myGame.Texture;

            ISprite newSprite = new FixedSprite(texture, new Vector2(400, 200));
            
            myGame.CurrentSprite = newSprite;
        }
    }
}
