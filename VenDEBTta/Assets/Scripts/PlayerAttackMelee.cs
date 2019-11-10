using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{

    [SerializeField]
    private BoxCollider2D attackBox;
    public float attackCooldown = 0.5f;
    private float attackTimer = 0f;
    private bool attacking = false;

    public float damage;

    public Animator anim;

    private void Awake()
    {
        attackBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Attack") > 0 && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;

            attackBox.enabled = true;

            if(anim)
            {
                anim.SetTrigger("Attacking");
            }
        }

        if(attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackBox.enabled = false;
            }
        }

        Debug.Log(attackTimer);
    }
}