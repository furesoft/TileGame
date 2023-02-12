using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TileGame.Core;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TileGame.Components;

public class Selectable : Component
{
    public bool IsSelected { get; private set; }

    public event Action<GameObject> OnSelect;

    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (!GameObject.IsMouseOverGameObject())
            {
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

            if (!IsSelected) return;
            
            OnSelect?.Invoke(GameObject);
        }
    }
}