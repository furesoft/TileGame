using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class PlayerMovement : Component
{
    public override void Initialize()
    {
        RefreshPosition();
    }

    public int TileIndex { get; set; }
    public void RefreshPosition()
    {
        var playerTexture = GameObject.GetComponent<TextureComponent>().Texture;
        
        var childCenter = new Vector2(GameObject.Size.Width / 2 - playerTexture.Width / 2,
            GameObject.Size.Height / 2 - playerTexture.Height / 2);

        var tiles = GameObject.GetRootParent().Children.Where(_=> _.Name == "Tile").ToArray();

        TileIndex++;
        TileIndex %= tiles.Length;
        
        GameObject.Position = tiles[TileIndex].Position + childCenter;
    }
}