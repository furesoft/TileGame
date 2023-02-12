﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class TextureRenderer : IRenderable
{
    private GameObject _gameObject;

    public TextureRenderer(GameObject gameObject)
    {
        this._gameObject = gameObject;
    }
    
    public void Initialize()
    {
        
    }

    public void Start()
    {
        
    }

    public bool Visible { get; } = true;
    public void Render(SpriteBatch sb, GameTime gameTime)
    {
        sb.Begin();
        
        var texture = _gameObject.GetComponent<TextureComponent>().Texture;
        sb.Draw(texture, _gameObject.Bounds, Color.White);

        sb.End();
    }
}