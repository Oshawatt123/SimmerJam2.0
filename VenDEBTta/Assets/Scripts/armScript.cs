using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class armScript : MonoBehaviour
{

    public float MaxHealth;
    private float health;

    public float iFrame;
    private float damageTimer;

    public Image healthBar;

    private void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Dead");
        }

        healthBar.fillAmount = (health / MaxHealth);

        damageTimer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if(damageTimer <= 0)
        {
            damageTimer = iFrame;
            health -= damage;
        }
    }
}
