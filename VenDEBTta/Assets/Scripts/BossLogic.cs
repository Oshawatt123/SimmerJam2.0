using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{

    public BoxCollider2D leftStage;
    public BoxCollider2D rightStage;
    public BoxCollider2D mainStage;

    public armScript leftArm;
    public armScript rightArm;

    public ShakeBehaviour shake;
    public bool shaking;

    public Collider2D player;

    private enum attack
    {
        leftHand,
        rightHand,
        headSlam,
        spawnBanker
    };
    private attack nextAttack;

    public float attackCooldown;
    private float attackTimer;

    public Animator anim;

    private float health;

    private enum states
    {
        Phase1,
        Phase2,
    };
    private states state;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == states.Phase1)
        {
            if (leftStage.IsTouching(player) && leftArm.GetHealth() > 0)
            {
                nextAttack = attack.leftHand;
            }
            if (rightStage.IsTouching(player) && rightArm.GetHealth() > 0)
            {
                nextAttack = attack.rightHand;
            }
            if (mainStage.IsTouching(player))
            {
                nextAttack = attack.headSlam;
            }

            if (attackTimer <= 0)
            {
                bossAttack(nextAttack);
                attackTimer = attackCooldown;
            }

            if (shaking)
            {
                shake.TriggerShake(0.5f);
                shaking = false;
            }
            
            if(leftArm.GetHealth() <= 0 && rightArm.GetHealth() <= 0)
            {
                anim.SetTrigger("Phase2");
                state = states.Phase2;
            }
        }
        else if (state == states.Phase2)
        {
            if(mainStage.IsTouching(player))
            {
                nextAttack = attack.spawnBanker;
            }
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
