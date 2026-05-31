using UnityEngine;

public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float detectionRange = 5f;

    private Transform player;
    private bool playerInRange;
    private float timeSinceLastShot = 0f;

    Shoot shoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        shoot = GetComponent<Shoot>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (fireRate <= 0f)
        {
            Debug.LogWarning("Fire rate must be graeater than 0. Setting to default of 2 seconds.");
            fireRate = 2f;
        }

        shoot.OnShotFired += () => timeSinceLastShot = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        playerInRange = distance <= detectionRange;

        if (player.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (playerInRange && stateInfo.IsName("Idle"))
        {
            if (Time.time >= timeSinceLastShot + fireRate)
            {
                anim.SetTrigger("Fire");
            }
        }
    }
}
