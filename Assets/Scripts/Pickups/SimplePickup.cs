using UnityEngine;

public class SimplePickup : MonoBehaviour
{
    public enum PickupType
    {
        Health,
        JumpBoost,
    }

    [SerializeField] private PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case PickupType.Health:
                    //ControllerColliderHit.lives++;
                    break;

                case PickupType.JumpBoost:
                    //ControllerColliderHit.JumpForceChange();
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
