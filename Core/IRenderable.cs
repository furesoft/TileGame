using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Core;

public interface IRenderable : IComponent
{
    bool Visible { get; }
    void Render(SpriteBatch sb, GameTime gameTime);
}