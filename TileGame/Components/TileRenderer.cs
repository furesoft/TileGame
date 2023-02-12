using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class TileRenderer : Component
{
    public override void Render(SpriteBatch sb, GameTime gameTime)
    {
        var selectable = GameObject.GetComponent<Selectable>();

        var color = selectable.IsSelected ? Color.White : Color.Black;

        sb.Begin();
        sb.Draw(GameObject.GetComponent<TextureComponent>().Texture, GameObject.Bounds, color);
        sb.End();
    }
}