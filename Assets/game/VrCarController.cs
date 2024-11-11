using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRCarController : MonoBehaviour
{
    public float speed = 1500f;  // Adjust forward speed
    public float turnSpeed = 30f;  // Adjust turn speed
    private Rigidbody rb;

    private InputDevice leftController;
    private InputDevice rightController;

    void Start()
    {
        // Initialize the Rigidbody
        rb = GetComponent<Rigidbody>();

        // Get VR controllers
        InitializeControllers();
    }

    void InitializeControllers()
    {
        List<InputDevice> leftHandDevices = new List<InputDevice>();
        List<InputDevice> rightHandDevices = new List<InputDevice>();

        // Locate the left and right VR controllers
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count > 0)
            leftController = leftHandDevices[0];

        if (rightHandDevices.Count > 0)
            rightController = rightHandDevices[0];
    }

    void FixedUpdate()
    {
        if (!leftController.isValid || !rightController.isValid)
        {
            InitializeControllers();  // Reinitialize if controllers aren't detected
            return;
        }

        // Get input for forward movement from the left controller’s trigger
        leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        Vector3 moveDirection = transform.forward * triggerValue * speed * Time.deltaTime;
        rb.AddForce(moveDirection, ForceMode.Force);

        // Get input for turning from the right controller’s joystick
        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickInput);
        float turn = joystickInput.x * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
