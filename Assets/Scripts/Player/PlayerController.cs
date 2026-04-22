using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour

{
    //Groundcheck
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask groundLayer;


    // Configurable Variables
   [SerializeField] private float moveSpeed = 5f;
   [SerializeField] private float jumpForce = 5f;

    //1. Pull our input so that we can see what our input values are.
    //2. Move our player horizontally based on the horizontal input value.

    //variables
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Animator anim;
    //cached variables
    private Vector2 groundCheckPos => CalculateGroundCheckPos();

    //state variables
    private bool _isGrounded;

    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = col.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
}

    // Update is called once per frame
    void Update()
    {

        _isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool atkInput = Input.GetButton("Fire2") && !_isGrounded;

        if (horizontalInput != 0) SpriteFlip(horizontalInput);

        //2. Move our player horizontally based on the horizontal input value.
        rb.linearVelocityX = horizontalInput * moveSpeed;
        

        if (jumpInput && _isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        anim.SetFloat("horizontalInput", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", _isGrounded);
        anim.SetFloat("yVel", rb.linearVelocityY);

        if (Input.GetButtonDown("Fire1") && _isGrounded)
        {
            anim.SetTrigger("atk1");
        }

       
        anim.SetBool("atk2", atkInput);
        
      
    }
    
    void SpriteFlip(float horizontalInput) => sr.flipX = (horizontalInput < 0);
    //if (sr.flipX && horizontalInput > 0 || !sr.flipX && horizontalInput < 0)
    //    sr.flipX = !sr.flipX;

}
