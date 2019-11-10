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
        health = MaxHealth / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {

        }

        healthBar.fillAmount = (health / MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
