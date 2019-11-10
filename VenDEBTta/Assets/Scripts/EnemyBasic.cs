using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Vector2 vectorToPlayer;
    private float scale;

    [SerializeField]
    public float maxHealth;
    private float Health;

    public Image healthBar;

    public float InvulnerabilityTime = 0.5f;
    private float damageTimer;

    public EnemyAttackMelee attackScript;

    public float knockBackForce;
    public GameObject hitParticleEffect;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            vectorToPlayer = player.transform.position - this.transform.position;
            if (state == states.seeking)
            {
                // seek the player
                rb2d.velocity = new Vector2(vectorToPlayer.x, 0f) * moveSpeed * Time.deltaTime;

                // if in range then state == states.attacking
                if(vectorToPlayer.magnitude < attackRange)
                {
                    state = states.attacking;
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
                anim.SetTrigger("Attacking");
                state = states.seeking;
                attackScript.eventAttacking = true;
            }

            if (vectorToPlayer.x < 0)
            {
                transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            Debug.Log("Player not found");
        }

        

        if(Health <= 0)
        {
            Die();
        }

        damageTimer -= Time.deltaTime;

        anim.SetFloat("velocityMagnitude", rb2d.velocity.magnitude);
    }

    public void TakeDamage(float damage)
    {
        if(damageTimer <= 0)
        {
            Health -= damage;
            damageTimer = InvulnerabilityTime;
            Instantiate(hitParticleEffect, transform);
            KnockBack();

            healthBar.fillAmount = Health / maxHealth;
        }
    }

    private void Die()
    {
        Instantiate(hitParticleEffect, transform, true);
        Destroy(this.gameObject);
    }

    public void KnockBack()
    {
        rb2d.AddForce(-vectorToPlayer.normalized * knockBackForce, ForceMode2D.Impulse);
        Debug.Log("Knockback: " + -vectorToPlayer.normalized * knockBackForce);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, seekRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
