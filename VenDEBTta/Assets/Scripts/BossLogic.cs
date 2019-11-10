using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{

    public BoxCollider2D leftStage;
    public BoxCollider2D rightStage;
    public BoxCollider2D mainStage;

    public ShakeBehaviour shake;
    public bool shaking;

    public Collider2D player;

    private enum attack
    {
        leftHand,
        rightHand,
        headSlam
    };

    private attack nextAttack;

    public float attackCooldown;
    private float attackTimer;

    public Animator anim;

    private float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftStage.IsTouching(player))
        {
            nextAttack = attack.leftHand;
        }
        if(rightStage.IsTouching(player))
        {
            nextAttack = attack.rightHand;
        }
        if (mainStage.IsTouching(player))
        {
            nextAttack = attack.headSlam;
        }

        if(attackTimer <= 0)
        {
            bossAttack(nextAttack);
            attackTimer = attackCooldown;
        }

        if(shaking)
        {
            shake.TriggerShake(0.5f);
            shaking = false;
        }

        attackTimer -= Time.deltaTime;
    }

    private void bossAttack(attack attackToPerform)
    {
        if(attackToPerform == attack.leftHand)
        {
            anim.SetTrigger("LeftAttack");
        }
        else if (attackToPerform == attack.rightHand)
        {
            anim.SetTrigger("RightAttack");
        }
        else if (attackToPerform == attack.headSlam)
        {
            anim.SetTrigger("LeftAttack");
        }
    }

    public void TakeDamage(float damage)
    {

    }
}
