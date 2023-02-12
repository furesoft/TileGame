using Microsoft.Xna.Framework;
using TileGame.Core;

namespace TileGame.Components;

public class PlayerMovement : IComponent, IUpdatable
{
    private GameObject gameObject;

    public PlayerMovement(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public void Initialize()
    {
        
    }

    public void Start()
    {
        
    }

    public bool Enabled { get; } = true;
    public void Update(GameTime gameTime)
    {
        var parent = gameObject.GetRootParent();

        foreach (var child in parent.Children)
        {
            if (child.Name == "Tile")
            {
                var selectable = child.GetComponent<Selectable>();

                if (selectable.IsSelected)
                {
                    var playerTexture = gameObject.GetComponent<TextureComponent>().Texture;
                    
                    var childCenter = new Vector2(child.Size.Width / 2 - playerTexture.Width/2, 
                        child.Size.Height / 2 - playerTexture.Height/2);
                    
                    gameObject.Position = child.Position + childCenter;
                }
            }
        }
    }
}