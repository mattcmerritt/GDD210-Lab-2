using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float Sensitivity;
    public float Gravity = -9.8f;
    public float JumpSpeed;
    public float VerticalSpeed;

    public CharacterController CC;

    public Transform CameraTransform;

    private float CameraRotation;

    public bool OnGround = false;
    public bool HasDoubleJump;

    public CapsuleCollider PlayerCollider;

    public Vector3 StartLocation;
    public Quaternion StartAngle, StartCameraAngle;

    public int Health;

    public UI UI;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartLocation = transform.position;
        StartAngle = transform.rotation;
        StartCameraAngle = CameraTransform.localRotation;
    }

    private void Update()
    {
        // translational movement
        float forwardInput = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        float rightInput = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

        Vector3 movement = (forwardInput * transform.forward) + (rightInput * transform.right);
        VerticalSpeed += (Gravity * Time.deltaTime);

        // checking if on ground
        if (CC.isGrounded && !OnGround)
        {
            // player has hit the ground
            OnGround = true;
        }
        else if (OnGround)
        {
            // check that there is still ground below the player (in case they walk off the edge)
            OnGround = Physics.Raycast(transform.position, Vector3.down, PlayerCollider.height + 0.1f);
        }

        // checking for jumps
        if (OnGround)
        {
            VerticalSpeed = 0f;
            HasDoubleJump = true;
            // check for single jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                VerticalSpeed = JumpSpeed;
                OnGround = false;
            }
        }
        else
        {
            // check for double jump
            if (Input.GetKeyDown(KeyCode.Space) && HasDoubleJump)
            {
                HasDoubleJump = false;
                VerticalSpeed = JumpSpeed;
            }
        }

        movement += (transform.up * VerticalSpeed * Time.deltaTime);

        CC.Move(movement);

        // looking around
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        CameraRotation += mouseY;
        CameraRotation = Mathf.Clamp(CameraRotation, -90f, 90f);

        CameraTransform.localRotation = Quaternion.Euler(CameraRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseX, 0f));

        // raycasting
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, CameraTransform.forward, out hit))
            {
                Debug.Log(hit.collider.name);
                Debug.DrawLine(CameraTransform.position + Vector3.down, hit.point, Color.green, 1f);

                // check if hit object is an enemy
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DestroyEnemy();
                    // increment score?
                }
            }
            else
            {
                Debug.LogWarning("Missed");
                Debug.DrawLine(CameraTransform.position + Vector3.down, CameraTransform.forward * 100f, Color.red, 1f);
            }
        }

        // check if player fell off map
        if (transform.position.y < -3f)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        Health--;
        UI.EnableHitIndicator();
        UI.UpdateHealth(Health);
    }
}
