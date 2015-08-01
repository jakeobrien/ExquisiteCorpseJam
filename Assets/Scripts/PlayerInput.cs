using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{

    public enum Mode
    {
        Keyboard,
        Joystick
    }
    public Mode mode;
    
    [System.Serializable]
    public struct KeyboardControls
    {
        public KeyCode armRotateLeft;
        public KeyCode armRotateRight;
        public KeyCode armExtend;
        public KeyCode grab;
    }
    public KeyboardControls keyboard;
    
    [System.Serializable]
    public struct JoystickControls
    {
        public string armRotate;
        public string armExtend;
        public string grab;
    }
    public JoystickControls joystick;

    private float _armRotate;
    public  float ArmRotate
    {
        get { return _armRotate; }
    }

    private bool _armExtend;
    public  bool ArmExtend
    {
        get { return _armExtend; }
    }

    private bool _grab;
    public  bool Grab
    {
        get { return _grab; }
    }

    public void Update()
    {
        if (mode == Mode.Keyboard) {
            DoKeyboard();
        } else {
            DoJoystick();
        }
    }

    private void DoKeyboard()
    {
        if (Input.GetKey(keyboard.armRotateLeft)) {
            _armRotate = -1f;
        } else if (Input.GetKey(keyboard.armRotateRight)) {
            _armRotate = 1f;
        } else {
            _armRotate = 0f;
        }
        _armExtend = Input.GetKeyDown(keyboard.armExtend);
        _grab = Input.GetKey(keyboard.grab);
    }

    private void DoJoystick()
    {
        var rotateAxis = Input.GetAxis(joystick.armRotate);
        if (Mathf.Abs(rotateAxis) > 0.2f) {
            _armRotate = rotateAxis;
        } else {
            _armRotate = 0f;
        }
        _armExtend = Input.GetButton(joystick.armExtend);
        _grab = Input.GetAxis(joystick.grab) > 0.5f;
    }

}
