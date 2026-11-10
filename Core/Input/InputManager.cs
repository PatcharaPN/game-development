using Microsoft.Xna.Framework.Input;

namespace untitledJourney.Core.Input;

public class InputManager
{
    private KeyboardState _currentKeyboard;
    private KeyboardState _previousKeyboard;

    public void Update()
    {
        _previousKeyboard = _currentKeyboard;
        _currentKeyboard = Keyboard.GetState();
    }

    // ตรวจสอบว่ากำลังกดปุ่มอยู่หรือไม่
    public bool IsKeyDown(Keys key)
    {
        return _currentKeyboard.IsKeyDown(key);
    }

    // ตรวจสอบว่าเพิ่งกดปุ่มในเฟรมนี้หรือไม่
    public bool IsKeyPressed(Keys key)
    {
        return _currentKeyboard.IsKeyDown(key)
            && _previousKeyboard.IsKeyUp(key);
    }

    // ตรวจสอบว่าเพิ่งปล่อยปุ่มในเฟรมนี้หรือไม่
    public bool IsKeyReleased(Keys key)
    {
        return _currentKeyboard.IsKeyUp(key)
            && _previousKeyboard.IsKeyDown(key);
    }
}