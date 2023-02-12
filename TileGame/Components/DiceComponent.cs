using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Core;

namespace TileGame.Components;

public class DiceComponent : Component
{
    private SpriteFont _font;
    private Vector2 _position;

    public event Action<int> OnDiceRoll;

    public override void Initialize()
    {
        _font = Content.Load<SpriteFont>("Arial");
        _position = GameObject.Position + new Vector2(GameObject.Size.Width/4, GameObject.Size.Height/4);
    }

    public int Number { get; set; }
    public override void Render(SpriteBatch sb, GameTime gameTime)
    {
        sb.Begin();
        
        sb.Draw(GameObject.GetComponent<TextureComponent>().Texture, GameObject.Bounds, Color.White);
        
        sb.DrawString(_font, Number.ToString(), _position, Color.Black);
        
        sb.End();
    }

    private bool hasRolled = false;
    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        
        if (!GameObject.IsMouseOverGameObject())
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