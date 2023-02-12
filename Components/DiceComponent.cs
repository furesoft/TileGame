using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Core;

namespace TileGame.Components;

public class DiceComponent  : IRenderable, IUpdatable
{
    private readonly GameObject _gameObject;
    private SpriteFont _font;
    private Vector2 _position;

    public event Action<int> OnDiceRoll;

    public DiceComponent(GameObject gameObject)
    {
        _gameObject = gameObject;
        Initialize();
    }
    
    public void Initialize()
    {
        _font = IComponent.Content.Load<SpriteFont>("Arial");
        _position = _gameObject.Position + new Vector2(_gameObject.Size.Width/4, _gameObject.Size.Height/4);
    }

    public void Start()
    {
        
    }

    public int Number { get; set; }
    public bool Visible { get; } = true;
    public void Render(SpriteBatch sb, GameTime gameTime)
    {
        sb.Begin();
        
        sb.Draw(_gameObject.GetComponent<TextureComponent>().Texture, _gameObject.Bounds, Color.White);
        
        sb.DrawString(_font, Number.ToString(), _position, Color.Black);
        
        sb.End();
    }

    public bool Enabled { get; } = true;
    
    private bool hasRolled = false;
    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        
        if (!_gameObject.IsMouseOverGameObject())
        {
            return;
        }
        
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            Number = Random.Shared.Next(1, 9);
            hasRolled = true;
        }
        
        if (mouseState.LeftButton == ButtonState.Released && hasRolled)
        {
            OnDiceRoll?.Invoke(Number);
            hasRolled = false;
        }
    }
}