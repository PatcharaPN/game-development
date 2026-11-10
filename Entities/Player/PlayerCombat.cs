using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace untitledJourney.Entities.Player;

public class PlayerCombat
{
    // ==========================================
    // SETTINGS
    // ==========================================

    private const int AttackFrameCount = 8;

    private const float AttackFrameDuration = 0.08f;


    // ==========================================
    // STATE
    // ==========================================

    private float _timer = 0f;

    private bool _wasAttackPressed = false;

    public bool IsAttacking { get; private set; }

    public int AttackFrame { get; private set; }

    public int AttackRow { get; private set; }


    // ==========================================
    // UPDATE
    // ==========================================

    public void Update(
        GameTime gameTime,
        KeyboardState keyboard,
        int facingDirection
    )
    {
        float deltaTime =
            (float)gameTime.ElapsedGameTime.TotalSeconds;


        // ======================================
        // ATTACK INPUT
        // ======================================

        bool attackPressed =
            keyboard.IsKeyDown(Keys.Space);


        // ======================================
        // START ATTACK
        // ======================================

        if (attackPressed &&
            !_wasAttackPressed &&
            !IsAttacking)
        {
            StartAttack(facingDirection);
        }

        _wasAttackPressed = attackPressed;


        // ======================================
        // ATTACK ANIMATION
        // ======================================

        if (!IsAttacking)
        {
            return;
        }

        _timer += deltaTime;

        if (_timer >= AttackFrameDuration)
        {
            _timer -= AttackFrameDuration;

            AttackFrame++;

            if (AttackFrame >= AttackFrameCount)
            {
                EndAttack();
            }
        }
    }


    // ==========================================
    // START ATTACK
    // ==========================================

    private void StartAttack(int facingDirection)
    {
        IsAttacking = true;

        AttackFrame = 0;

        AttackRow = facingDirection;

        _timer = 0f;
    }


    // ==========================================
    // END ATTACK
    // ==========================================

    private void EndAttack()
    {
        IsAttacking = false;

        AttackFrame = 0;

        _timer = 0f;
    }
}