using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class TileRenderer : IRenderable
{
    private readonly GameObject _gameObject;
    
    public void Initialize()
    {
      
    }

    public TileRenderer(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public void Start()
    {
        
    }

    public bool Visible { get; } = true;
    public void Render(SpriteBatch sb, GameTime gameTime)
    {
        var selectable = _gameObject.GetComponent<Selectable>();

        var color = selectable.IsSelected ? Color.White : Color.Black;

        sb.Begin();
        sb.Draw(_gameObject.GetComponent<TextureComponent>().Texture, new Rectangle((int)_gameObject.Position.X, (int)_gameObject.Position.Y, _gameObject.Size.Width, _gameObject.Size.Height), color);
        sb.End();
    }
}