using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class armScript : MonoBehaviour
{

    public float MaxHealth;
    private float health;

    public bool dead;

    public float iFrame;
    private float damageTimer;

    public Image healthBar;
    public TextMeshProUGUI name;

    public SpawnCoins coinSpawner;

    public bool isLeft;

    private void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            if (health <= 0)
            {
                Debug.Log("Dead");
                coinSpawner.CoinShower(2, 5, isLeft, Random.Range(0,10)>5);
                dead = true;
                name.text = "Dead";
            }

            healthBar.fillAmount = (health / MaxHealth);

            damageTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if(damageTimer <= 0)
        {
            damageTimer = iFrame;
            health -= damage;
        }
    }

    public float GetHealth()
    {
        return health;
    }
}
