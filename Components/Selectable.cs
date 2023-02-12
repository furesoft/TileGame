using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using TileGame.Core;

namespace TileGame.Components;

public class Selectable : Component
{
    public bool IsSelected { get; private set; }

    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (!GameObject.IsMouseOverGameObject())
            {
                IsSelected = false;
                return;
            }

            var texture = GameObject.GetComponent<TextureComponent>().Texture;

            Color[] pixels = new Color[texture.Height * texture.Width];
            texture.GetData(pixels);

            var mouseVector = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            var relativePosition = mouseVector - GameObject.Position;
            var index = (int) (relativePosition.Y / GameObject.Size.Height * texture.Height * texture.Width +
                               relativePosition.X / GameObject.Size.Width * texture.Width);

            var color = pixels[index % pixels.Length];

            IsSelected = color.A >= 200;
        }
    }
}