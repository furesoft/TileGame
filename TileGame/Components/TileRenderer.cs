using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Components;

public class TileRenderer : GameLoopComponent
{
    public override void Render(SpriteBatch sb, GameTime gameTime)
    {
        var selectable = Object.GetComponent<Selectable>();
        var position = Object.GetComponent<PositionComponent>();

        var color = selectable.IsSelected ? Color.White : Color.Black;

        sb.Begin();
        sb.Draw(Object.GetComponent<TextureComponent>().Texture, position.Bounds, color);
        sb.End();
    }
}