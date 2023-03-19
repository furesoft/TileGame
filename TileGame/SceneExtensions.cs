using Furesoft.Core.Componenting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame;

public static class SceneExtensions
{
    public static void Update(this Scene scene, GameTime gameTime)
    {
        foreach (var entity in scene.Objects)
            entity.Update(gameTime);
    }
    
    public static void Draw(this Scene scene, SpriteBatch sb, GameTime gameTime)
    {
        foreach (var entity in scene.Objects)
            entity.Draw(sb, gameTime);
    }
}