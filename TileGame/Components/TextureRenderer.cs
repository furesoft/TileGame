using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class TextureRenderer : Component
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
        
        var texture = GameObject.GetComponent<TextureComponent>().Texture;
        sb.Draw(texture, GameObject.Bounds, _color);

        sb.End();
    }
}