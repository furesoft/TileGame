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
            if (new Rectangle(new Point((int) _gameObject.Position.X, (int) _gameObject.Position.Y), _gameObject.Size)
                .Contains(new Point2(mouseState.X, mouseState.Y)))
            {
                var texture = _gameObject.GetComponent<TextureComponent>().Texture;

                Color[] pixels = new Color[texture.Height * texture.Width];
                texture.GetData(pixels);
                
                var index = mouseState.X + (mouseState.Y * texture.Width);
                var color = pixels[index % pixels.Length]; //ToDo: fix selection

                IsSelected = color.A == 255;
            }
            else
            {
                IsSelected = false;
            }
            
        }
    }
}