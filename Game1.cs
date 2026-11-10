using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using untitledJourney.Core;
using untitledJourney.Entities.Player;
using System.IO;

namespace untitledJourney;

public class Game1 : Application
{
    // ==========================================
    // PLAYER
    // ==========================================

    private Texture2D _playerTexture = null!;
    private SpriteSheet _playerSpriteSheet = null!;

    private Vector2 _playerPosition = new(300, 200);

    private float _playerSpeed = 150f;


    // ==========================================
    // COMBAT
    // ==========================================

    private PlayerCombat _playerCombat = new();

    private Texture2D _attackTexture = null!;
    private SpriteSheet _attackSpriteSheet = null!;


    // ==========================================
    // ANIMATION
    // ==========================================

    private int _currentFrame = 0;
    private int _currentRow = 0;

    private float _animationTimer = 0f;

    private const float FrameDuration = 0.12f;

    private const int WalkFrameCount = 12;
    private const int IdleFrameCount = 12;


    // ==========================================
    // IDLE ROWS
    // ==========================================

    private const int IdleDown = 0;
    private const int IdleLeft = 1;
    private const int IdleRight = 2;
    private const int IdleUp = 3;


    // ==========================================
    // WALK ROWS
    // ==========================================

    private const int WalkDown = 4;
    private const int WalkLeft = 5;
    private const int WalkRight = 6;
    private const int WalkUp = 7;


    // ==========================================
    // PLAYER STATE
    // ==========================================

    private int _facingDirection = IdleDown;

    private bool _isMoving = false;
    private bool _wasMoving = false;


    // ==========================================
    // UPDATE
    // ==========================================

    protected override void OnUpdate(GameTime gameTime)
    {
        var keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Escape))
        {
            Exit();
            return;
        }

        float deltaTime =
            (float)gameTime.ElapsedGameTime.TotalSeconds;


        // ======================================
        // COMBAT UPDATE
        // ======================================

        _playerCombat.Update(
            gameTime,
            keyboard,
            _facingDirection
        );


        // ======================================
        // BLOCK MOVEMENT DURING ATTACK
        // ======================================

        if (_playerCombat.IsAttacking)
        {
            _isMoving = false;
            _wasMoving = false;

            _currentFrame = 0;
            _animationTimer = 0f;

            return;
        }


        // ======================================
        // MOVEMENT INPUT
        // ======================================

        Vector2 direction = Vector2.Zero;

        if (keyboard.IsKeyDown(Keys.W) ||
            keyboard.IsKeyDown(Keys.Up))
        {
            direction.Y -= 1;
            _facingDirection = IdleUp;
        }

        if (keyboard.IsKeyDown(Keys.S) ||
            keyboard.IsKeyDown(Keys.Down))
        {
            direction.Y += 1;
            _facingDirection = IdleDown;
        }

        if (keyboard.IsKeyDown(Keys.A) ||
            keyboard.IsKeyDown(Keys.Left))
        {
            direction.X -= 1;
            _facingDirection = IdleLeft;
        }

        if (keyboard.IsKeyDown(Keys.D) ||
            keyboard.IsKeyDown(Keys.Right))
        {
            direction.X += 1;
            _facingDirection = IdleRight;
        }


        // ======================================
        // MOVEMENT STATE
        // ======================================

        _isMoving = direction != Vector2.Zero;


        // ======================================
        // RESET ANIMATION
        // ======================================

        if (_isMoving != _wasMoving)
        {
            _currentFrame = 0;
            _animationTimer = 0f;
        }


        // ======================================
        // PLAYER MOVEMENT
        // ======================================

        if (_isMoving)
        {
            direction.Normalize();

            _playerPosition +=
                direction * _playerSpeed * deltaTime;
        }


        // ======================================
        // SELECT ANIMATION ROW
        // ======================================

        if (_isMoving)
        {
            _currentRow = _facingDirection + 4;
        }
        else
        {
            _currentRow = _facingDirection;
        }


        // ======================================
        // ANIMATION UPDATE
        // ======================================

        _animationTimer += deltaTime;

        if (_animationTimer >= FrameDuration)
        {
            _animationTimer -= FrameDuration;

            int frameCount = _isMoving
                ? WalkFrameCount
                : IdleFrameCount;

            _currentFrame =
                (_currentFrame + 1) % frameCount;
        }


        _wasMoving = _isMoving;
    }


    // ==========================================
    // LOAD CONTENT
    // ==========================================

    protected override void OnLoadContent()
    {
        // Player
        using var stream = File.OpenRead(
            "Content/Textures/player.png"
        );

        _playerTexture = Texture2D.FromStream(
            GraphicsDevice,
            stream
        );

        _playerSpriteSheet = new SpriteSheet(
            _playerTexture,
            12,
            8
        );


        // Player Attack
        using var attackStream = File.OpenRead(
            "Content/Textures/player_attack.png"
        );

        _attackTexture = Texture2D.FromStream(
            GraphicsDevice,
            attackStream
        );

        _attackSpriteSheet = new SpriteSheet(
            _attackTexture,
            8,
            4
        );
    }


    // ==========================================
    // DRAW
    // ==========================================

    protected override void OnDraw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(35, 45, 60));

        Renderer.Begin();

        if (_playerCombat.IsAttacking)
        {
            var source = _attackSpriteSheet.GetFrame(
                _playerCombat.AttackFrame,
                _playerCombat.AttackRow
            );

            Renderer.Draw(
                _attackTexture,
                source,
                _playerPosition,
                2f,
                new Vector2(32, 48)
            );
        }
        else
        {
            var source = _playerSpriteSheet.GetFrame(
                _currentFrame,
                _currentRow
            );

            Renderer.Draw(
                _playerTexture,
                source,
                _playerPosition,
                2f,
                new Vector2(16, 32)
            );
        }

        Renderer.End();
    }
}