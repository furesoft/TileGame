using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class TextureComponent : IComponent
{
    public TextureComponent(Texture2D texture)
    {
        Texture = texture;
    }

    public Texture2D Texture { get; set; }
    public void Initialize()
    {
        
    }

    public void Start()
    {
        
    }
}