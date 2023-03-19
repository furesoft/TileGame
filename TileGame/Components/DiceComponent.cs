using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TileGame.Components;

public class DiceComponent : GameLoopComponent
{
    private SpriteFont _font;
    private Vector2 _position;

    public event Action<int> OnDiceRoll;

    public override void Initialize()
    {
        _font = Content.Load<SpriteFont>("Arial");

        var position = Object.GetComponent<PositionComponent>();
        _position = position.Position + new Vector2(position.Size.Width/4, position.Size.Height/4);
    }

    public int Number { get; set; }
    public override void Render(SpriteBatch sb, GameTime gameTime)
    {
        sb.Begin();
        
        var position = Object.GetComponent<PositionComponent>();
        sb.Draw(Object.GetComponent<TextureComponent>().Texture, position.Bounds, Color.White);
        
        sb.DrawString(_font, Number.ToString(), _position, Color.Black);
        
        sb.End();
    }

    private bool hasRolled = false;
    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        
        if (!Object.IsMouseOverGameObject())
        {
            return;
        }
        
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            Number = Random.Shared.Next(1, 6);
            hasRolled = true;
        }
        
        if (mouseState.LeftButton == ButtonState.Released && hasRolled)
        {
            OnDiceRoll?.Invoke(Number);
            hasRolled = false;
        }
    }
}