using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement1 : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    
    [Header("Animations")]
    public Animator animator;

    public float jumpHeight = 6f;
    float velocityY;
    float velocityX;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;           
        }

        if (targetDir == new Vector2(-1f, 0f))
            animator.SetBool ("Bool1", true);
        else
            animator.SetBool("Bool1", false);

        if (targetDir == new Vector2(1f, 0f))
            animator.SetBool("Bool2", true);
        else
            animator.SetBool("Bool2", false);

        if (targetDir == new Vector2(0f, 1f))
            animator.SetBool("Bool3", true);
        else
            animator.SetBool("Bool3", false);

        if (targetDir == new Vector2(0f, -1f))
            animator.SetBool("Bool4", true);
        else
            animator.SetBool("Bool4", false);

        if (targetDir.x > 0.5f && targetDir.y > 0.5f)
            animator.SetBool("Bool5", true);
        else
            animator.SetBool("Bool5", false);

        if (targetDir.x < -0.5f && targetDir.y > 0.5f)
            animator.SetBool("Bool6", true);
        else
            animator.SetBool("Bool6", false);

        if (targetDir.x > 0.5f && targetDir.y < -0.5f)
            animator.SetBool("Bool7", true);
        else
            animator.SetBool("Bool7", false);

        if (targetDir.x < -0.6f && targetDir.y < -0.6f)
            animator.SetBool("Bool8", true);
        else
            animator.SetBool("Bool8", false);

        Debug.Log(targetDir);



        
    }


}