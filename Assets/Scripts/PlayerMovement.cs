using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI WINTEXT;
    [SerializeField] private float jumpCooldown = 0.2f; // Time between jumps

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool canJump = true;

    private void Awake()
    {
        // Grab references
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip character based on movement direction
        if (horizontalInput > 0.01f)
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && canJump)
            Jump();

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
        canJump = false;
        Invoke(nameof(ResetJump), jumpCooldown); //Reset jump after cooldown
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Win!!!")
        {
            WINTEXT.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
