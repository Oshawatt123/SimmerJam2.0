using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public float damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit!");
            collision.SendMessageUpwards("TakeDamage", damage);
        }
        Debug.Log("Attack hit something!");
    }
}
