using Furesoft.Core.Componenting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Components;

namespace TileGame;

public static class GameObjectExtensions
{
    public static void Update(this ComponentObject gameObject, GameTime gameTime)
    {
        for (int i = 0; i < gameObject.Components.Count; i++)
        {
            var component = gameObject.Components[i];
            
            if (component.Enabled && component is GameLoopComponent gameLoopComponent)
            {
                gameLoopComponent.Update(gameTime);
            }
        }
    }

    public static void Draw(this ComponentObject gameObject, SpriteBatch sb, GameTime gameTime)
    {
        for (int i = 0; i < gameObject.Components.Count; i++)
        {
            var component = gameObject.Components[i];
            
            if (component.Visible && component is GameLoopComponent gameLoopComponent)
            {
                gameLoopComponent.Render(sb, gameTime);
            }
        }
    }
    
    public static bool IsMouseOverGameObject(this ComponentObject gameObject)
    {
        var mouseState = Mouse.GetState();

        return gameObject.GetComponent<PositionComponent>().Bounds.Contains(mouseState.Position);
    }
}