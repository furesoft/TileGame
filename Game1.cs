using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using TileGame.Core;
using TileGame.Screens;
using GameScreen = TileGame.Screens.GameScreen;

namespace TileGame;

public class Game1 : Game
{
    public static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public readonly ScreenManager _screenManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _screenManager = new ScreenManager();
        IComponent.Content = Content;
        Components.Add(_screenManager);
    }

    protected override void Initialize()
    {
        var menuScreen = new GameScreen(this);
        _screenManager.LoadScreen(menuScreen);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _screenManager.Initialize();

        IComponent.Content = Content;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

       _screenManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _screenManager.Draw(gameTime);
        
        base.Draw(gameTime);
    }
}
