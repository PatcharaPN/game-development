using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace untitledJourney.Core.Graphics;

public class SpriteRenderer
{
    private readonly SpriteBatch _spriteBatch;

    public SpriteRenderer(GraphicsDevice graphicsDevice)
    {
        _spriteBatch = new SpriteBatch(graphicsDevice);
    }

    public void Begin()
    {
        _spriteBatch.Begin();
    }

    public void End()
    {
        _spriteBatch.End();
    }

    public void Draw(
        Texture2D texture,
        Rectangle sourceRectangle,
        Vector2 position,
        float scale = 1f,
        Vector2? origin = null
    )
    {
        _spriteBatch.Draw(
            texture,
            position,
            sourceRectangle,
            Color.White,
            0f,
            origin ?? Vector2.Zero,
            scale,
            SpriteEffects.None,
            0f
        );
    }
}