using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 10f; // Kecepatan gerakan karakter
    public float rotationSpeed = 500f; // Kecepatan rotasi karakter

    private Rigidbody rb;

    Vector3 movement;
    public Animator animator;

    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private Transform cameraTransform;

    private bool isJumping = false;

    private void Start()
    {
        Setting.isPlayerActive = true;
        rb = GetComponent<Rigidbody>();

        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(x, y, z);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(StartJumping());
    }

    private IEnumerator StartJumping()
    {
        isJumping = !isJumping;
        animator.SetBool("isJumping", isJumping);

        yield return new WaitForSeconds(2f);

        isJumping = !isJumping;
        animator.SetBool("isJumping", isJumping);
    }

    private void FixedUpdate()
    {
        // float joystickHorizontalInput = Input.GetAxis("Horizontal");
        // float joystickVerticalInput = Input.GetAxis("Vertical");

        float joystickHorizontalInput = joystick != null ? joystick.Horizontal : 0f;
        float joystickVerticalInput = joystick != null ? joystick.Vertical : 0f;

        if (!isJumping)
        {
            // Play animation
            bool isWalking = !Mathf.Approximately(joystickVerticalInput, 0) || !Mathf.Approximately(joystickHorizontalInput, 0);
            animator.SetBool("isWalking", isWalking);

            // Move and rotate player based on input
            MovePlayerFree(joystickHorizontalInput, joystickVerticalInput);
        }

    }

    private void MovePlayerFree(float hInput, float vInput)
    {
        Vector3 movementDirection = new Vector3(hInput, 0, vInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * moveSpeed;
        movementDirection.Normalize();

        // // Use this if you want free look camera instead
        // movementDirection = Quaternion.AngleAxis(cameraTransform.transform.eulerAngles.y, Vector3.up) * movementDirection;

        transform.Translate(movementDirection * Time.fixedDeltaTime * magnitude * Time.fixedDeltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Object"))
        {
            rb.velocity = Vector3.zero;
        }

    }
}
