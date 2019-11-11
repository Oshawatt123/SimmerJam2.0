using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class headScript : MonoBehaviour
{

    public float MaxHealth;
    private float health;

    public bool dead;

    public float iFrame;
    private float damageTimer;

    public Image healthBar;
    public TextMeshProUGUI name;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            if(health <= 0)
            {
                dead = true;
                name.text = "Payed Off in Full";
            }

            healthBar.fillAmount = (health / MaxHealth);

            damageTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (damageTimer <= 0)
        {
            damageTimer = iFrame;
            health -= damage;
        }
    }

    public float getHealth()
    {
        return health;
    }
}
