using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMotion : MonoBehaviour
{

    public float VelocityX = 5f;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * VelocityX;
    }

    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") || collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("TakeDamage", damage);
            Destroy(this.gameObject);
        }
    }
}