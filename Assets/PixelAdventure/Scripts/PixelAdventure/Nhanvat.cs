using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nhanvat : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer sprite;
    private float huong_x;

    private int melons = 0;
    private UIItem uiItem;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jump = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private AudioSource deathAudioEffect;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        uiItem = FindObjectOfType<UIItem>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        UpdateAnimation();
    }

    private void MoveCharacter()
    {
        huong_x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(huong_x * moveSpeed, rb.velocity.y);
        /*rb.rotation = 0;
        rb.angularVelocity = 0;*/
        //transform.position += new Vector3(huong_x * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown("space") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    private void UpdateAnimation()
    {
        MovementState state = MovementState.idle;
        if (huong_x > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (huong_x < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Melon")
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            melons++;
            uiItem.setScore("Fruits: " + melons);
        }

        if(collision.gameObject.tag == "head")
        {
            //Destroy(collision.gameObject);
            string name = collision.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
        }

        if (collision.gameObject.tag == "body")
        {
            rb.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("death");
            deathAudioEffect.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            rb.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("death");
            deathAudioEffect.Play();
        }

       
    }

    public void AutoRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
