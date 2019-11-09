using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    private enum states
    {
        idle,
        seeking,
        attacking
    };
    private states state = states.idle;

    public float seekRange;
    public float attackRange;

    public float moveSpeed;
    private Rigidbody2D rb2d;

    private Transform player = null;

    [SerializeField]
    private float Health;

    public float InvulnerabilityTime = 0.5f;
    private float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            Vector2 vectorToPlayer = player.transform.position - this.transform.position;
            if (state == states.seeking)
            {
                // seek the player
                rb2d.velocity = vectorToPlayer * moveSpeed * Time.deltaTime;

                // if in range then state == states.attacking
                if(vectorToPlayer.magnitude < attackRange)
                {

                }
            }
            else if (state == states.idle)
            {
                if(vectorToPlayer.magnitude < seekRange)
                {
                    state = states.seeking;
                }
            }
            else if (state == states.attacking)
            {

            }
        }
        else
        {
            Debug.Log("Player not found");
        }

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }

        damageTimer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if(damageTimer <= 0)
        {
            Health -= damage;
            damageTimer = InvulnerabilityTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, seekRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
