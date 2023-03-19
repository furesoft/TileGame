using System;
using Furesoft.Core.Componenting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TileGame.Components;

public class Selectable : GameLoopComponent
{
    public bool IsSelected { get; private set; }

    public event Action<ComponentObject> OnSelect;

    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (!Object.IsMouseOverGameObject())
            {
                return;
            }

            var texture = Object.GetComponent<TextureComponent>().Texture;

            Color[] pixels = new Color[texture.Height * texture.Width];
            texture.GetData(pixels);

            var mouseVector = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            var position = Object.GetComponent<PositionComponent>();
            var relativePosition = mouseVector - position.Position;
            var index = (int) (relativePosition.Y / position.Size.Height * texture.Height * texture.Width +
                               relativePosition.X / position.Size.Width * texture.Width);

            var color = pixels[index % pixels.Length];

            IsSelected = color.A >= 200;

            if (!IsSelected) return;
            
            OnSelect?.Invoke(Object);
        }
    }
}