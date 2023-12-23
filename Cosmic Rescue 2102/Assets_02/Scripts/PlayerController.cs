using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Motion Settings")]
    public float Speed = 3500f;
    public float JumpForce = 4f;
    public float RotationSpeed = 4f;
    public LayerMask groundMask;
    [Header("Player Camera Settings")]//move to cameraMan
    public float mX_MIN = 100;
    public float mX_MAX = -100;
    public float mY_MIN = 100;
    public float mY_MAX = -100;
    [Header("Button Action Delay")]
    public float HoldTime = 1f;
    [Header("Build Menu Settings")]
    [SerializeField] private GameObject canvas;

    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;
    
    private Rigidbody rb;
    private GravityReceiver gravityReceiver;
    private InputHelper inputHelper;


    public float CurrentHoldTime = 0f;
    private bool BuildMenuActive = false;
    private float mouseX;
    private float mouseY;
    private bool StopMotion;
    private bool isGrounded;

    void Start()
    {
      inputHelper = new InputHelper();
      OriginalPosition = transform.position;
      OriginalRotation = transform.rotation;

      rb = GetComponent<Rigidbody>();
      gravityReceiver = GetComponent<GravityReceiver>();
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse_X");
        mouseY = Input.GetAxis("Mouse_Y");
        canvas.SetActive(BuildMenuActive);
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, inputHelper.Mouse_axisLimits(mouseX, mX_MIN, mX_MAX,out float vx) *RotationSpeed);
        transform.Rotate(Vector3.forward, inputHelper.Mouse_axisLimits(mouseY, mY_MIN, mY_MAX, out float vy) * RotationSpeed);

        Debug.DrawRay(transform.position, -transform.up * 20.1f, isGrounded ? Color.green : Color.red);

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(Vector3.forward);
            NewMoveMethod();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = OriginalPosition;
            transform.rotation = OriginalRotation;
        }
        if (Input.GetKey(KeyCode.E))
        {
            Jump();          
        }
        if (Input.GetKey(KeyCode.F))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.T))
        {
                Debug.Log("The Target Planets Gravity is: " + gravityReceiver.Gravity);
        }
        if (Input.GetKey(KeyCode.G))
        {          
            if (InputDelay())
            {
                BuildMenuActive = !BuildMenuActive;                
                CurrentHoldTime = 0f;
            }

        }
        
        
    }

    private void NewMoveMethod()
    {
        // Calculate the movement direction based on the input and the character's rotation
        Vector3 inputDirection = new Vector3(mouseX, 0f, mouseY).normalized;

        // Get the character's current rotation in radians
        float characterRotation = transform.eulerAngles.y * Mathf.Deg2Rad;

        // Calculate the movement direction in world space using trigonometric functions
        float moveX = Mathf.Sin(characterRotation) * inputDirection.z + Mathf.Cos(characterRotation) * inputDirection.x;
        float moveZ = Mathf.Cos(characterRotation) * inputDirection.z - Mathf.Sin(characterRotation) * inputDirection.x;

        // Combine the movement components into a single vector
        Vector3 movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Use Rigidbody.AddForce to move the character
        rb.AddForce(movement * Speed);
    }

    private void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 10.1f,groundMask);
        Debug.DrawRay(transform.position, -transform.up * 20.1f, isGrounded ? Color.green : Color.red);
        if (isGrounded)
        {
            Vector3 surfaceNormal = GetSurfaceNormal();
            Vector3 jumpDirection = surfaceNormal == Vector3.up ? Vector3.up : Vector3.ProjectOnPlane(Vector3.up,surfaceNormal).normalized;
            rb.AddForce(jumpDirection*JumpForce, ForceMode.Impulse);
        }
    }

    private Vector3 GetSurfaceNormal()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 10.1f, groundMask))
        {
            return hit.normal;
        }
        return Vector3.up;
    }

    private bool InputDelay()
    {
        CurrentHoldTime += Time.deltaTime;
        if(CurrentHoldTime>=HoldTime)
        {            
            return true;
        }
        else
        {
            return false;
        }
    }
}
