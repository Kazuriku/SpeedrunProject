using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float runSpeed;
    public float jumpForce;
    private float moveInput;
    private bool iswalking;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    private bool facingright = true;

    public static PlayerMove instance;
    public bool isDamaged = false;
    SpriteRenderer spriteRenderer;

    private Animator animator;

    [SerializeField] public AudioSource knockbacksound;
    [SerializeField] public AudioSource walksound;
    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (GameController.instance.GetPlaying())
        {
            if (isDamaged == false)
            {
                moveInput = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
            }
        }
        Flip();
    }
    void anim()
    {
        if((isDamaged == false && moveInput < 0) || (isDamaged == false && moveInput > 0))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    void stepsound()
    {
        if (rb.velocity.x != 0 && isGrounded)
        {
            iswalking = true;
        }
        else
            iswalking = false;
        if (iswalking)
        {
            if (!walksound.isPlaying)
            {
                walksound.Play();
            }
        }else if(!iswalking && walksound.isPlaying)
        {
            walksound.Stop();
        }
    }
    void Update()
    {
        if (GameController.instance.GetPlaying())//3,2,1�� �� �����̰� �ϴ°�
        {
            anim();
            stepsound();
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && isDamaged == false)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                animator.SetTrigger("isUp");
            }

            if (Input.GetKey(KeyCode.Space) && isJumping == true && isDamaged == false)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    animator.SetTrigger("isFalling");
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) || isDamaged == true)
            {
                animator.SetTrigger("isFalling");
                isJumping = false;
                
            }
        }

         if (Input.GetKeyUp(KeyCode.Space) || isDamaged == true)
        {
            animator.SetTrigger("isFalling");
            isJumping = false;
        }


      
    }
    private void Flip()
    {
        if (facingright == true && moveInput < 0)
        {
            Vector3 currnetscale = gameObject.transform.localScale;
            currnetscale.x *= -1;
            gameObject.transform.localScale = currnetscale;
            facingright = false;
        }
        else if (facingright == false && moveInput > 0)
        {
            Vector3 currnetscale = gameObject.transform.localScale;
            currnetscale.x *= -1;
            gameObject.transform.localScale = currnetscale;
            facingright = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            isDamaged = true;
            onDamaged(collision.transform.position);
        }
        
    }

    void onDamaged(Vector2 targetPos)
    {
        rb.velocity = Vector2.zero;
        gameObject.layer = 11;
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc, 1) * 40, ForceMode2D.Impulse);
        knockbacksound.Play();
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("stop", 0.5f);
        Invoke("offDamaged", 4);
    }
    void stop()
    {
        rb.AddForce(new Vector2(-rb.velocity.x, 1), ForceMode2D.Impulse);
        Invoke("stunover", 2.5f);
    }
    void stunover()
    {
        isDamaged = false;
    }
    void offDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

}
