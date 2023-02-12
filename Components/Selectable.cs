using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using TileGame.Core;

namespace TileGame.Components;

public class Selectable : IUpdatable
{
    private readonly GameObject _gameObject;
    public bool IsSelected { get; private set; }
    
    public Selectable(GameObject gameObject)
    {
        _gameObject = gameObject;
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
        var mouseState = Mouse.GetState();
        
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (_gameObject.IsMouseOverGameObject())
            {
                var texture = _gameObject.GetComponent<TextureComponent>().Texture;

                Color[] pixels = new Color[texture.Height * texture.Width];
                texture.GetData(pixels);

                var mouseVector = new Vector2(mouseState.Position.X, mouseState.Position.Y);
                var relativePosition = mouseVector - _gameObject.Position;
                var index = (int)((relativePosition.Y/_gameObject.Size.Height*texture.Height)* texture.Width + (relativePosition.X/_gameObject.Size.Width*texture.Width) );
                
                var color = pixels[index];

                IsSelected = color.A >= 200;
            }
            else
            {
                IsSelected = false;
            }
            
        }
    }
}