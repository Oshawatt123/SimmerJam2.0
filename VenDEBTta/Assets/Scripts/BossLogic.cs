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
    public headScript head;

    public ShakeBehaviour shake;
    public bool shaking;

    public Collider2D player;

    private enum attack
    {
        noAttack,
        leftHand,
        rightHand,
        headSlam,
        spawnBanker,
        interest,
        overdraft
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
        Defeated
    };
    private states state;
    private bool defeated;
    public CanvasGroup GameOverScreen;

    public GameObject banker;

    public Transform[] SpawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen.alpha = 0;
        GameOverScreen.interactable = false;
        GameOverScreen.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == states.Phase1)
        {
            nextAttack = attack.noAttack;
            if (leftStage.IsTouching(player) && !leftArm.dead)
            {
                nextAttack = attack.leftHand;
            }
            if (rightStage.IsTouching(player) && !rightArm.dead)
            {
                nextAttack = attack.rightHand;
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
                /*int attackType = Random.Range(0, 100);

                if(attackType < 60)
                {
                    nextAttack = attack.spawnBanker;
                }
                else if (attackType < 80)
                {
                    nextAttack = attack.interest;
                }
                else
                {
                    nextAttack = attack.overdraft;
                }*/

                nextAttack = attack.spawnBanker;
            }

            if(attackTimer <= 0)
            {
                bossAttack(nextAttack);
                attackTimer = attackCooldown;
            }

            if(head.getHealth() <= 0)
            {
                state = states.Defeated;
            }
        }
        else if (state == states.Defeated)
        {
            if(!defeated)
            {
                anim.SetTrigger("Victory");
                GameOverScreen.alpha = 1;
                GameOverScreen.interactable = true;
                GameOverScreen.blocksRaycasts = true;
            }
            defeated = true;
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
        else if (attackToPerform == attack.spawnBanker)
        {
            SpawnBankers();
        }
        else if (attackToPerform == attack.interest)
        {
            Debug.Log("Interest");
        }
        else if (attackToPerform == attack.overdraft)
        {
            Debug.Log("Overdraft");
        }
    }

    public void SpawnBankers()
    {
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            Instantiate(banker, SpawnLocations[Random.Range(0, SpawnLocations.Length)].position, Quaternion.identity);
        }
    }

    public void InterestAttack()
    {
        /* GameObject interestInst = Instantiate(interestPrefab, SpawnLocations[Random.Range(0, SpawnLocations.Length)].position, Quaternion.identity) as GameObject;
         * interestInst.transform.rotation = new Vector3(0f, 0f, Random.Range(-70, -110);
         * let the interest laser motor itself
         */
    }

    public void OverdraftAttack()
    {
        /* GameObject OverdraftInst = Instantiate(OverdraftPrefab, SpawnLocations[Random.Range(0, SpawnLocations.Length)].position, Quaternion.identity) as GameObject;
         * OverdraftInst.transform.rotation = new Vector3(0f, 0f, Random.Range(-70, -110);
         * let the overdraft laser motor itself
         */
    }
}