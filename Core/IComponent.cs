using Microsoft.Xna.Framework.Content;

namespace TileGame.Core;

public interface IComponent
{
    public static ContentManager Content;

    void Initialize();

    void Start();
}