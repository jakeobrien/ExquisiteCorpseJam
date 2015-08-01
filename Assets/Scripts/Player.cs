using UnityEngine;
using System.Collections;

public class Player : MonoBehavclops
{

    public WheelJoint2D wheel;
    public SliderJoint2D slider;
    public Grabber grabber;
    public PlayerInput input;

    public float armRotationSpeed;
    public float armExtensionDistance;
    public float grabDelay;
    public float retractionDelay;

    private bool _isExtended;
    private bool _isGrabbing;

    public void Update()
    {
        DoSlider();
        DoWheel();
        DoGrabber();
    }

    private void DoSlider()
    {
        if (input.ArmExtend && !_isExtended ) 
        {
            _isExtended = true;
            StartCoroutine( StartRetractArm() );
        }
    }

    private void DoWheel()
    {
        var motor = wheel.motor;
        motor.motorSpeed = input.ArmRotate * armRotationSpeed;
        wheel.motor = motor;
    }

    private void DoGrabber()
    {
        //grabber.ShouldGrab = input.Grab;
        grabber.ShouldGrab = _isExtended;
    }
    
    private IEnumerator StartRetractArm()
    {
        var limits = slider.limits;

        slider.useMotor = true;
        limits.max = armExtensionDistance;
        _isGrabbing = true;
        slider.limits = limits;

        yield return new WaitForSeconds( grabDelay );

        _isGrabbing = false;

        yield return new WaitForSeconds( retractionDelay );
        _isExtended = false;

        limits.max = 2f;
        slider.limits = limits;
    }
}
