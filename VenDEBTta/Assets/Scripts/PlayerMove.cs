using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveModifier;
    public bool lookingRight = true;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float maxFallSpeed;

    public Animator anim;

    private float scale;

    private Rigidbody2D rb2D;

    public Vector2 movement;

    public Grounded grounded;

    public float playerMaxHealth;
    private float playerHealth;
    public float invulnerabilityTime;
    private float damageTimer;
    public Image healthBar;

    private Vector3 respawn = new Vector3(0f, 4f, 0f);

    public TextMeshProUGUI coinCounterUI;
    public int coins;

    public AudioSource audioSource;
    public AudioClip healthPickUp;
    public AudioClip coinPickUp;
    public AudioClip jump;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        rb2D = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth > 0)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                movement.x += Input.GetAxis("Horizontal") * moveModifier;
                lookingRight = Input.GetAxis("Horizontal") > 0;
            }

            if (Input.GetButtonDown("Jump") && grounded.isGrounded)
            {
                movement.y = 0f;
                Jump();
            }

            rb2D.velocity = movement * Time.deltaTime;

            // Gravity & jumping
            if (!grounded.isGrounded)
            {
                movement.y = Mathf.Max(-maxFallSpeed, movement.y - rb2D.gravityScale);
                anim.SetBool("inAir", true);
            }
            else
            {
                anim.SetBool("inAir", false);
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
            }

            movement.x = 0f;

            anim.SetFloat("velocityMagnitude", rb2D.velocity.magnitude);
            damageTimer -= Time.deltaTime;

            Color newColor = new Color();
            newColor.r = 1/playerHealth;
            newColor.g = 1 - 1 / playerHealth;
            newColor.b = 0;
            newColor.a = 1;

            healthBar.color = newColor;
            healthBar.fillAmount = playerHealth / playerMaxHealth;

            coinCounterUI.text = coins.ToString();
        }
        else
        {
            Debug.Log("Dead AF");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DeathBox"))
        {
            transform.position = respawn;
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            respawn = collision.transform.position;
        }
        else if(collision.CompareTag("Coin"))
        {
            coins += 1;
            audioSource.PlayOneShot(coinPickUp, 0.5f);
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Health"))
        {
            playerHealth += 1;
            audioSource.PlayOneShot(healthPickUp, 0.5f);
            Destroy(collision.gameObject);
        }
    }

    private void Jump()
    {
        movement.y += jumpHeight;
        audioSource.PlayOneShot(jump, 0.3f);
    }

    public void TakeDamage(float damage)
    {
        if(damageTimer <= 0)
        {
            Debug.Log("Player damaged");
            playerHealth -= damage;

            damageTimer = invulnerabilityTime;
        }
    }
}
