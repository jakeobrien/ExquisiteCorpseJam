using UnityEngine;
using System.Collections;

public class Player : MonoBehavclops
{

    public WheelJoint2D wheel;
    public SliderJoint2D slider;
    public Grabber grabber;
    public float armRotationSpeed;
    public float armExtensionDistance;

    public void Update()
    {
        DoSlider();
        DoWheel();
        DoGrabber();
    }

    private void DoSlider()
    {
        var limits = slider.limits;
        if (Input.GetKey(KeyCode.W)) {
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
        if (Input.GetKey(KeyCode.A)) {
            motor.motorSpeed = -armRotationSpeed;
        } else if (Input.GetKey(KeyCode.S)) {
            motor.motorSpeed = armRotationSpeed;
        } else {
            motor.motorSpeed = 0f;
        }
        wheel.motor = motor;
    }

    private void DoGrabber()
    {
        grabber.ShouldGrab = Input.GetKey(KeyCode.Space);
    }

}
