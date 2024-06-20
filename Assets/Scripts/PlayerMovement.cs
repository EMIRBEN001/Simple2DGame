using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        //Grab References
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed ,body.velocity.y);


        // this is basically for flipping the character if it moves left or right and faces that direction
        if(horizontalInput > 0.01f)
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1); //if u try to put this position on the unity editor it's gonna flip it

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())//Jump
            Jump();

        //set animator parameters
        anim.SetBool("run", horizontalInput != 0); //basically if arrow key is not pressed the horinput is 0, is 0 not equal to 0? = false, run = false(no running sad)
        anim.SetBool("grounded", isGrounded());

        
    }

    private void Jump()
    {

        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 01f, groundLayer);
        return rayCastHit.collider != null;
    }
}
