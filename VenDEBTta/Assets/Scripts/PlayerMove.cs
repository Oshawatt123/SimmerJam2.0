using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveModifier;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float maxFallSpeed;

    public Animator anim;

    private float scale;

    private Rigidbody2D rb2D;

    private Vector2 movement;

    public Grounded grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            movement.x += Input.GetAxis("Horizontal") * moveModifier;
        }

        if(Input.GetButtonDown("Jump") && grounded.isGrounded)
        {
            movement.y = 0f;
            Jump();
        }

        rb2D.velocity = movement * Time.deltaTime;

        // Gravity & jumping
        if(!grounded.isGrounded)
        {
            movement.y = Mathf.Max(-maxFallSpeed, movement.y - rb2D.gravityScale);
            anim.SetBool("inAir", true);
        }
        else
        {
            anim.SetBool("inAir", false);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }

        movement.x = 0f;

        anim.SetFloat("velocityMagnitude", rb2D.velocity.magnitude);
    }

    private void Jump()
    {
        movement.y += jumpHeight;
    }
}
