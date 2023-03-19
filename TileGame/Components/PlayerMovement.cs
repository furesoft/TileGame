using System.Linq;
using Furesoft.Core.Componenting;
using Microsoft.Xna.Framework;

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
        var playerTexture = Object.GetComponent<TextureComponent>().Texture;
        
        var position = Object.GetComponent<PositionComponent>();
        var childCenter = new Vector2(position.Size.Width / 2 - playerTexture.Width / 2,
            position.Size.Height / 2 - playerTexture.Height / 2);

        var tiles = Object.GetRootParent().Children.Where(_=> _.Name == "Tile").ToArray();

        TileIndex++;
        TileIndex %= tiles.Length;
        
        position.Position = tiles[TileIndex].GetComponent<PositionComponent>().Position + childCenter;
    }
}