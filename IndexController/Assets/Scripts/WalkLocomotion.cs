using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class WalkLocomotion : MonoBehaviour {
    public SteamVR_Input_Sources movementHand;
    public SteamVR_Action_Vector2 walkAction;
    public SteamVR_Action_Boolean jumpAction;
    public GameObject head;
    public Transform movementHandPosition;
    public PhysicMaterial noFrictionMaterial;
    public PhysicMaterial frictionMaterial; public float jumpHeight;
    public float maxMovementSpeed;
    public float acceleration;
    [Range(0f, 0.1f)]
    public float maxTurn;
    [Range(0.01f, 1f)]
    public float deadzone;

    private Vector2 joystick;
    private Vector3 moveDirection;
    private CapsuleCollider capCollider;
    private int groundCount;
    private float movementSpeed;
    private float lastMovementSum;
    private float maxMovementSum;
    private float standardAcceleration;

    private void Start() {
        capCollider = GetComponent<CapsuleCollider>();
        standardAcceleration = acceleration;
    }

    void Update() {
        updateInput();
        updateCollider();
        Rigidbody RBody = GetComponent<Rigidbody>();
        Vector3 velocity = new Vector3(0, 0, 0);

        // Get the angle of the touch and correct it for the rotation of the controller or head
        // Controller
        // moveDirection = Quaternion.AngleAxis(Angle(trackpad) + movementHandPosition.transform.localRotation.eulerAngles.y, Vector3.up) * Vector3.forward; //
        // Head
        moveDirection = Quaternion.AngleAxis(Angle(joystick) + head.transform.localRotation.eulerAngles.y, Vector3.up) * Vector3.forward;

        // Calc jump
        if (jumpAction.GetStateDown(movementHand) && groundCount > 0) {
            float jumpSpeed = Mathf.Sqrt(2 * jumpHeight * 10f);
            RBody.AddForce(0, jumpSpeed, 0, ForceMode.VelocityChange);
        }

        // Calc movement
        if (groundCount > 0) {
            if (joystick.magnitude > deadzone) {
                capCollider.material = noFrictionMaterial;
                velocity = moveDirection;

                // Turn slowdown calculation
                float movementSum = moveDirection.x + moveDirection.z;
                // empty check
                if (lastMovementSum == 0)
                    lastMovementSum = movementSum;
                // difference between this position and last position
                float minusMovementSum = Mathf.Abs(Mathf.Abs(lastMovementSum) - Mathf.Abs(movementSum));
                // when difference greater maxTurn decrease acceleration
                if (minusMovementSum > maxTurn)
                    acceleration = -standardAcceleration;
                else
                    acceleration = standardAcceleration;

                lastMovementSum = movementSum;

                // movementSpeed calculation
                movementSpeed += acceleration / 10f;

                // maxSpeed calcualtion
                float maxSpeed = joystick.magnitude * maxMovementSpeed;
                if (movementSpeed > maxSpeed)
                    movementSpeed = maxSpeed;
                if (movementSpeed < 0)
                    movementSpeed = 0;

                // Force caclulation
                RBody.AddForce(velocity.x * movementSpeed - RBody.velocity.x, 0f, velocity.z * movementSpeed - RBody.velocity.z, ForceMode.VelocityChange);

                //Debug.Log("Movement Speed: " + movementSpeed);
                //Debug.Log("Velocity: " + velocity);
                //Debug.Log("Movement Direction: " + moveDirection);
                //Debug.Log("Movement Direction Plus : " + (moveDirection.x + moveDirection.z));
            }
            else {
                // stops speed and slide of player
                capCollider.material = frictionMaterial;
                movementSpeed = 0f;
            }

        }
        else if (groundCount > 0) {
            // stops speed and slide of player
            capCollider.material = frictionMaterial;
            movementSpeed = 0f;
        }
    }

    public static float Angle(Vector2 p_vector2) {
        if (p_vector2.x < 0) {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

    private void updateCollider() {
        capCollider.height = head.transform.localPosition.y;
        capCollider.center = new Vector3(head.transform.localPosition.x, head.transform.localPosition.y / 2, head.transform.localPosition.z);
    }

    private void updateInput() {
        joystick = walkAction.GetAxis(movementHand); ;
    }

    // Checks if you touch the ground
    private void OnCollisionEnter(Collision collision) {
        groundCount++;
    }
    private void OnCollisionExit(Collision collision) {
        groundCount--;
    }
}