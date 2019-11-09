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
    private states state = states.seeking;

    private Vector2 moveVector;

    private Transform player = null;

    [SerializeField]
    private float Health;

    public float InvulnerabilityTime = 0.5f;
    private float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            if (state == states.seeking)
            {
                // seek the player
                moveVector = player.transform.position - this.transform.position;

                // if in range then state == states.attacking
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
}
