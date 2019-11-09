using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveModifier;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private Rigidbody2D rb2d;

    private Vector2 movement;

    public Grounded grounded;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Jump();
        }

        // Move & reset movement vector for nex
        transform.Translate(movement * Time.deltaTime);
        movement = Vector2.zero;
    }

    private void Jump()
    {
        rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }
}
