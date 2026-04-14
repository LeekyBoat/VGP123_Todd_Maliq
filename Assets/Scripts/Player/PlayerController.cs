using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour


{
    // Configurable Variables
   [SerializeField] private float moveSpeed = 5f;
   [SerializeField] private float jumpForce = 5f;

    //1. Pull our input so that we can see what our input values are.
    //2. Move our player horizontally based on the horizontal input value.

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");
        //2. Move our player horizontally based on the horizontal input value.

        rb.linearVelocityX = horizontalInput * 5f;

        if (jumpInput)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
