using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Factories
{
    internal class EnemySpriteFactory
    {
        private Texture2D snakeSpritesheet;
        //...

        private static EnemySpriteFactory instance = new EnemySpriteFactory();

        public static EnemySpriteFactory Instance
        {
            get { return instance; }
        }

        private EnemySpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            snakeSpritesheet = content.Load<Texture2D>("snakeSpritesheet.png");
            //...
        }
        
        public IEnemy CreateSnakeSprite()
        {
            return new SnakeSprite(snakeSpritesheet, 32, 32);
        }
    }
}
