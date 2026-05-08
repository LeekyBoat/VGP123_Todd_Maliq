using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{

    [SerializeField, Range(0.5f, 5f)] private float lifetime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Destroy(gameObject, lifetime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }


}
