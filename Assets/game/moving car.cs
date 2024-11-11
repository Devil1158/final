using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 1500f; // Controls forward speed
    public float turnSpeed = 15f; // Controls turn speed

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input for forward and backward movement
        float moveInput = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys

        // Move the car forward and backward
        Vector3 moveDirection = transform.forward * moveInput * speed * Time.deltaTime;
        rb.AddForce(moveDirection, ForceMode.Force);

        // Rotate the car
        float turn = turnInput * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}

