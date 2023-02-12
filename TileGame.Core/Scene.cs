using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Core;

public class Scene
{
    public List<GameObject> Objects = new();

    public void Initialize()
    {
        foreach (var entity in Objects)
            entity.Initialize();
    }
    
    public void Update(GameTime gameTime)
    {
        foreach (var entity in Objects)
            entity.Update(gameTime);
    }
    
    public void Draw(SpriteBatch sb, GameTime gameTime)
    {
        foreach (var entity in Objects)
            entity.Draw(sb, gameTime);
    }
}