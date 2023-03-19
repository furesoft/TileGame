using Furesoft.Core.Componenting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Components;

namespace TileGame;

public class Game1 : Game
{
    public static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Scene _currentScene = new();

    public Game1()
    {
        _graphics = new(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        GameLoopComponent.Content = Content;
    }
    
    private void CreateTile(int x, int y, ComponentObject parent)
    {
        var tile = CreateGameObject("Tile");
        
        tile.AddComponent(new PositionComponent(new(x, y), new(50,50)));
        tile.AddComponent(new TextureComponent("tile"));
        tile.AddComponent<TextureRenderer>();
        
        tile.SetParent(parent);
    }

    private void CreatePlayer(ComponentObject tiles, Color color, int startIndex)
    {
        var player = CreateGameObject("player");
        
        player.AddComponent(new PositionComponent(new(), new(25,25)));
        player.AddComponent(new TextureComponent("player"));
        player.AddComponent(new TextureRenderer(color));
        player.AddComponent<PlayerMovement>();
        player.AddComponent<Selectable>();

        var movement = player.GetComponent<PlayerMovement>();
        movement.TileIndex = startIndex;

        var selectable = player.GetComponent<Selectable>();
        selectable.OnSelect += so =>
        {
            selectedPlayer = so;
        };

        player.SetParent(tiles);
    }

    private ComponentObject selectedPlayer;
    private void CreateDice()
    {
        var dice = CreateGameObject("dice");

        var position = new Vector2(GraphicsDevice.Viewport.Width -75, 25);
        dice.AddComponent(new PositionComponent(position, new(50,50)));
        dice.AddComponent(new TextureComponent("dice"));

        var diceComponent = new DiceComponent();
        diceComponent.OnDiceRoll += (num) =>
        {
            var movement = selectedPlayer.GetComponent<PlayerMovement>();

            movement.TileIndex += num;
            movement.RefreshPosition();
        };
        
        dice.AddComponent(diceComponent);
    }

    private ComponentObject CreateGameObject(string name)
    {
        var go = new ComponentObject(name);

        _currentScene.Objects.Add(go);

        return go;
    }

    protected override void LoadContent()
    {
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += (s, e) =>
        {
            Draw(new());
        };
        _spriteBatch = new(GraphicsDevice);

        GameLoopComponent.Content = Content;
        
        var tiles = CreateGameObject("tiles");

        for (int i = 0; i < 5; i++)
        {
            CreateTile( i+1 * (_currentScene.Objects.Count + 50*i), GraphicsDevice.Viewport.Height / 2 - 50, tiles);
        }

        for (int i = 0; i < 4; i++)
        {
            CreateTile(210, 250 + (i * 51), tiles);
        }

        CreatePlayer(tiles, Color.Red,0);
        CreatePlayer(tiles, Color.Blue,1);

        CreateDice();
        
        _currentScene.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _currentScene.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        var spriteBatch = new SpriteBatch(GraphicsDevice);
        _currentScene.Draw(spriteBatch, gameTime);
    }
}
