using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace untitledJourney.Core;

public class SpriteSheet
{
    private readonly Texture2D _texture;

    private readonly int _columns;
    private readonly int _rows;

    public SpriteSheet(
        Texture2D texture,
        int columns,
        int rows
    )
    {
        _texture = texture;

        _columns = columns;
        _rows = rows;
    }

    public Rectangle GetFrame(int column, int row)
    {
        int frameWidth = _texture.Width / _columns;
        int frameHeight = _texture.Height / _rows;

        return new Rectangle(
            column * frameWidth,
            row * frameHeight,
            frameWidth,
            frameHeight
        );
    }
}