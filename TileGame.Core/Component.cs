using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Core;

public abstract class Component
{
    public static ContentManager Content;
    public GameObject GameObject { get; set; }

    public virtual void Initialize() { }

    public virtual void Start() { }

    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual bool Enabled { get; } = true;

    public virtual bool Visible { get; } = true;
    public virtual void Render(SpriteBatch sb, GameTime gameTime){}
}