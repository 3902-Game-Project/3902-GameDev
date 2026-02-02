using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using Sprint0.Commands;
using Sprint0.Interfaces;
public class KeyboardController : IController
{
    private Game1 myGame;
    private Dictionary<Keys, ICommand> keyMappings;
    
    public KeyboardController(Game1 game)
    {
        keyMappings = new Dictionary<Keys, ICommand>
        {
            {Keys.D0, new QuitCommand(game)},
            {Keys.D1, new FixedSpriteCommand(game)},
            {Keys.D2, new AnimatedSpriteCommand(game)},
            {Keys.D3, new UpandDownCommand(game)},
            {Keys.D4, new LeftandRightAnimatedCommand(game)}
        };
    }
    public void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        foreach (Keys key in keyMappings.Keys)
        {
            if(keyboardState.IsKeyDown(key))
            {
                keyMappings[key].Execute();
            }
        }
    }
}