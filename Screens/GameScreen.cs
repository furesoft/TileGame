using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using TileGame.Components;
using TileGame.Core;

namespace TileGame.Screens;



public class GameScreen : Screen
{
    private readonly Game1 _game;

    private List<GameObject> _gameObjects = new();

    public GameScreen(Game1 game)
    {
        _game = game;
    }

    public override void Initialize()
    {
        var tiles = CreateGameObject("tiles");
        CreateTile(10, 10, tiles);
        CreateTile(150, 150, tiles);
        
        CreatePlayer(tiles);
    }

    private void CreateTile(int x, int y, GameObject parent)
    {
        var tile = CreateGameObject("Tile");
        tile.Position = new (x, y);
        tile.Size = new Size(100, 100);
        tile.AddComponent(new TextureComponent("tile"));
        tile.AddComponent<TileRenderer>();
        tile.AddComponent<Selectable>();
        
        tile.SetParent(parent);
    }

    private void CreatePlayer(GameObject tiles)
    {
        var player = CreateGameObject("player");
        player.Size = new(50, 50);
        player.AddComponent<PlayerMovement>();
        player.AddComponent(new TextureComponent("player"));
        player.AddComponent<TextureRenderer>();
        player.SetParent(tiles);
    }

    private GameObject CreateGameObject(string name)
    {
        var go = new GameObject(name);

        _gameObjects.Add(go);
        
        go.Initialize();
        
        return go;
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var entity in _gameObjects)
            entity.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        var spriteBatch = new SpriteBatch(_game.GraphicsDevice);
        
        foreach (var entity in _gameObjects)
        {
            entity.Draw(spriteBatch, gameTime);
        }
    }
    
    
}