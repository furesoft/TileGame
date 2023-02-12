using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using TileGame.Components;
using TileGame.Core;

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
        Component.Content = Content;
    }
    
    private void CreateTile(int x, int y, GameObject parent)
    {
        var tile = CreateGameObject("Tile");
        tile.Position = new (x, y);
        tile.Size = new Size(100, 100);
        tile.AddComponent(new TextureComponent("tile"));
        tile.AddComponent<TextureRenderer>();
        
        tile.SetParent(parent);
    }

    private void CreatePlayer(GameObject tiles, Color color, int startIndex)
    {
        var player = CreateGameObject("player");
        player.Size = new(50, 50);
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

    private GameObject selectedPlayer;
    private void CreateDice()
    {
        var dice = CreateGameObject("dice");
        dice.Size = new(50, 50);
        dice.Position = new Vector2(GraphicsDevice.Viewport.Width -75, 25);
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

    private GameObject CreateGameObject(string name)
    {
        var go = new GameObject(name);

        _currentScene.Objects.Add(go);

        return go;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new(GraphicsDevice);

        Component.Content = Content;
        
        var tiles = CreateGameObject("tiles");

        for (int i = 0; i < 4; i++)
        {
            CreateTile(i+1 * (_currentScene.Objects.Count + 210*i), 10, tiles);
        }
        
        for (int i = 0; i < 4; i++)
        {
            CreateTile(i+1 * (_currentScene.Objects.Count + 210*i), 200, tiles);
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
