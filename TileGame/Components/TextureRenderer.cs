using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Components;

public class TextureRenderer : GameLoopComponent
{
    private readonly Color _color;

    public TextureRenderer()
    {
        _color = Color.White;
    }

    public TextureRenderer(Color color)
    {
        _color = color;
    }
    
    public override void Render(SpriteBatch sb, GameTime gameTime)
    {
        sb.Begin();
        
        var position = Object.GetComponent<PositionComponent>();
        var texture = Object.GetComponent<TextureComponent>().Texture;
        sb.Draw(texture, position.Bounds, _color);

        sb.End();
    }
}