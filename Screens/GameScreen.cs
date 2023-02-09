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
        CreateTile(10, 10);
        CreateTile(150, 150);
    }

    private void CreateTile(int x, int y)
    {
        var tile = CreateGameObject("Tile1");
        tile.Position = new Vector2(x, y);
        tile.Size = new Size(100, 100);
        tile.AddComponent(new TextureComponent(IComponent.Content.Load<Texture2D>("Tile")));
        tile.AddComponent<TileRenderer>();
        tile.AddComponent<Selectable>();
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