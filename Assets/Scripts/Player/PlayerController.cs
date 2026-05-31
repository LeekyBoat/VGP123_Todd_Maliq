using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour

{
    //Groundcheck
    [Header("Ground CHeck Settings")]
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask groundLayer;


    // Configurable Variables
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    //[SerializeField] private int maxLives = 9;

    [Header("Powerup Settings")]
    [SerializeField] private float jumpForcePowerup = 15f;
    [SerializeField] private float initialPowerupDuration = 5f;


    //1. Pull our input so that we can see what our input values are.
    //2. Move our player horizontally based on the horizontal input value.

    //private int _lives = 3;
    //public int lives
    //{
    //    get { return _lives; }
    //    set
    //    {
    //        if (value > maxLives)
    //        {
    //            _lives = maxLives;
    //        }
    //        else if (value < 0)
    //        {
    //            _lives = 0;
    //            //go to game over
    //        }
    //        else
    //        {
    //            _lives = value;
    //        }

    //        Debug.Log($"Lives have changed to {_lives}");
    //    }
    //}

    private int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            value = Mathf.Max(0, value);
            _score = value;
        }
    }

    //variables
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Animator anim;
    //cached variables
    private Vector2 groundCheckPos => CalculateGroundCheckPos();

    //state variables
    private bool _isGrounded;
    private GroundCheck groundCheck;

    private float currentPowerupDuration = 0f;
    private float initialJumpForce = 5f;

    private Coroutine jumpForceCoroutine = null;

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
        groundCheck = new GroundCheck(col, rb, groundCheckRadius, groundLayer);

        initialJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);

        _isGrounded = groundCheck.CheckGrounded();

        if (Input.GetButtonDown("Fire1") && _isGrounded)
        {
            anim.SetTrigger("atk1");

        }

        float horizontalInput;
        bool jumpInput, atkInput;
        HandlePlayerInput(out horizontalInput, out jumpInput, out atkInput);

        void HandlePlayerInput(out float horizontalInput, out bool jumpInput, out bool atkInput)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetButtonDown("Jump");
            atkInput = Input.GetButton("Fire2") && !_isGrounded;
        }
        if (horizontalInput != 0) SpriteFlip(horizontalInput);


        void SpriteFlip(float horizontalInput) => sr.flipX = (horizontalInput < 0);

        anim.SetBool("atk2", atkInput);

        if (atkInput && clipInfo[0].clip.name != "atk1")
        {
            anim.SetTrigger("atk1");
        }

        /*if (clipInfo[0].clip.name == "atk1")
        {
            rb.linearVelocity = Vector2.zero;
        }*/

        //2. Move our player horizontally based on the horizontal input value.
        rb.linearVelocityX = horizontalInput * moveSpeed;


        if (jumpInput && _isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        anim.SetFloat("horizontalInput", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", _isGrounded);
        anim.SetFloat("yVel", rb.linearVelocityY);

    }



        public void JumpForceChange()
    {
        if (jumpForceCoroutine != null)
        {
            StopCoroutine(jumpForceCoroutine);
            jumpForceCoroutine = null;
            jumpForce = initialJumpForce;
        }

        jumpForceCoroutine = StartCoroutine(JumpForceChangeCoroutine());
    }

    IEnumerator JumpForceChangeCoroutine()
    {
        currentPowerupDuration = initialPowerupDuration + currentPowerupDuration;
        jumpForce = jumpForcePowerup;

        while (currentPowerupDuration > 0)
        {
            currentPowerupDuration -= Time.deltaTime;
            Debug.Log($"Jump Powerup Time Remaining {currentPowerupDuration}");
            yield return null;

        }

        jumpForce = initialJumpForce;
        jumpForceCoroutine = null;
        currentPowerupDuration = 0f;
    }

    //For projectiles
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    //For player pickups
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish") && rb.linearVelocityY < 0)
        {
            BaseEnemy enemy = collision.GetComponentInParent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(0, BaseEnemy.DamageType.JumpedOn);
                rb.linearVelocityY = 0;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        Debug.Log("PlayerController: OnTriggerEnter2D called with collider " + collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("PlayerController: OnTriggerExit2D called with collider " + collision.name);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("PlayerController: OnTriggerStay2D called with collider " + collision.name);
    }

}


 
