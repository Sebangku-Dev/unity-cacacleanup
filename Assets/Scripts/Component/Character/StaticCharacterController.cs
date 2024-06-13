using UnityEngine;

public class StaticCharacterController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] bool isJumping = true;

    private void Start()
    {
        // must be used with Animator component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("isJumping", isJumping);
    }
}
