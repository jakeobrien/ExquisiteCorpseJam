using UnityEngine;
using System.Collections;

public class Player : MonoBehavclops
{

    public WheelJoint2D wheel;
    public SliderJoint2D slider;
    public Grabber grabber;
    public float armRotationSpeed;
    public float armExtensionDistance;
    public PlayerInput input;


    public void Update()
    {
        DoSlider();
        DoWheel();
        DoGrabber();
    }

    private void DoSlider()
    {
        var limits = slider.limits;
        if (input.ArmExtend) {
            slider.useMotor = true;
            limits.max = armExtensionDistance;
        } else {
            slider.useMotor = true;
            limits.max = 2f;
        }
        slider.limits = limits;
    }

    private void DoWheel()
    {
        var motor = wheel.motor;
        motor.motorSpeed = input.ArmRotate * armRotationSpeed;
        wheel.motor = motor;
    }

    private void DoGrabber()
    {
        grabber.ShouldGrab = input.Grab;
    }

}
