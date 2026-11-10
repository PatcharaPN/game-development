using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using untitledJourney.Core.Input;
using untitledJourney.Core.Graphics;

namespace untitledJourney.Core;

public abstract class Application : Microsoft.Xna.Framework.Game
{
    protected InputManager Input { get; private set; } = null!;
    private readonly GraphicsDeviceManager _graphics;

    protected SpriteBatch SpriteBatch { get; private set; } = null!;
    protected SpriteRenderer Renderer { get; private set; } = null!;
    protected Application()
    {
        _graphics = new GraphicsDeviceManager(this);
        Input = new InputManager();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected sealed override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        Renderer = new SpriteRenderer(GraphicsDevice);
        OnLoadContent();
    }

    protected sealed override void Update(GameTime gameTime)
    {
        OnUpdate(gameTime);
        base.Update(gameTime);
    }

    protected sealed override void Draw(GameTime gameTime)
    {
        Input.Update();
        OnDraw(gameTime);
        base.Draw(gameTime);
    }

    protected virtual void OnLoadContent() { }

    protected virtual void OnUpdate(GameTime gameTime) { }

    protected virtual void OnDraw(GameTime gameTime) { }
}