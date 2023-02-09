using Microsoft.Xna.Framework;

namespace TileGame.Core;

public interface IUpdatable : IComponent
{
    bool Enabled { get; }
    void Update(GameTime gameTime);
}