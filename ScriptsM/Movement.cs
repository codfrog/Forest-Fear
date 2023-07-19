using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Movement : MonoBehaviour
{
    public Transform playerCamera;

    [Range(0.0f, 0.5f)]
    public float mouseSmoothTime = 0.03f;

    public bool cursorLock = true;
    public float mouseSensitivity = 3.5f;
    public float Speed = 6.0f;

    [Range(0.0f, 0.5f)]
    public float moveSmoothTime = 0.3f;

    public float gravity = -30f;
    public Transform groundCheck;
    public LayerMask ground;

    public float jumpHeight = 6f;
    private float velocityY;
    private bool isGrounded;

    private float cameraCap;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    private CharacterController controller;
    private Vector2 currentDir;
    private Vector2 currentDirVelocity;

    public GameObject ProjectilePrefab;
    public GameObject ProjectilePrefabCube;
    public GameObject ProjectilePrefabCylinder;
    public Transform ProjectileSpawnPos;
    public float ProjectileSpeed = 10f;

    public static int Ammo = 5;
    public static int Ammo2 = 30;
    public static int Ammo3 = 10;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();

        /*if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo > 0 && GameManager.instance.Weapon == Weapons.Weapon1)
        {
            StartCoroutine(UpdateProjectile());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo2 > 0 && GameManager.instance.Weapon == Weapons.Weapon2)
        {
            StartCoroutine(UpdateProjectile2());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo > 0 && GameManager.instance.Weapon == Weapons.Weapon3)
        {
            StartCoroutine(UpdateProjectile3());
        }*/
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
    }

    public IEnumerator UpdateProjectile()
    {
        var projectile = Instantiate(ProjectilePrefab, ProjectileSpawnPos.position, ProjectileSpawnPos.rotation);
        projectile.GetComponent<Rigidbody>().velocity = ProjectileSpawnPos.forward * ProjectileSpeed;

        Ammo--;
        yield return new WaitForSeconds(3);
        Destroy(projectile);
    }

    public IEnumerator UpdateProjectile2()
    {
        var projectile = Instantiate(ProjectilePrefabCube, ProjectileSpawnPos.position, ProjectileSpawnPos.rotation);
        projectile.GetComponent<Rigidbody>().velocity = ProjectileSpawnPos.forward * ProjectileSpeed;

        Ammo2--;
        yield return new WaitForSeconds(3);
        Destroy(projectile);
    }

    public IEnumerator UpdateProjectile3()
    {
        var projectile = Instantiate(ProjectilePrefabCylinder, ProjectileSpawnPos.position, ProjectileSpawnPos.rotation);
        projectile.GetComponent<Rigidbody>().velocity = ProjectileSpawnPos.forward * ProjectileSpeed;

        Ammo--;
        yield return new WaitForSeconds(3);
        Destroy(projectile);
    }
}