using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlier : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    private Rigidbody _rigidbody;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float MinXLook;
    public float MaXLook;
    private float camCurXRot;
    public float LookSensitivity;

    private Vector2 mouseDelta;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }


    void Start()
    {
        //커서 안보임 
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //context 현재 상태를 받아올수있다 
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    private void CamLook()
    {
        camCurXRot += mouseDelta.y * LookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, MinXLook, MaXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);


        this.transform.eulerAngles += new Vector3(0, mouseDelta.x * LookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 dir = this.transform.forward * curMovementInput.y + this.transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; //점프를 했을때만 

        _rigidbody.velocity = dir;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGround())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CamLook();
    }


    bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(this.transform.position+(this.transform.forward*0.2f)+(this.transform.up*0.01f),Vector3.down),
            new Ray(this.transform.position+(-this.transform.forward*0.2f)+(this.transform.up*0.01f),Vector3.down),
            new Ray(this.transform.position+(this.transform.right*0.2f)+(this.transform.up*0.01f),Vector3.down),
            new Ray(this.transform.position+(-this.transform.right*0.2f)+(this.transform.up*0.01f),Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }


    void Update()
    {

    }
}
