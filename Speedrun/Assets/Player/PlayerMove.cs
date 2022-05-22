using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float runSpeed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    private bool facingright = true;

    public static PlayerMove instance;

    private Animator animator;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
        Flip();
    }
    void anim()
    {
        if(moveInput < 0 || moveInput > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    void Update()
    {
        anim();
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetTrigger("isUp");
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            animator.SetTrigger("isFalling");
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
    public IEnumerator Knockback (float knockDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;
        while(knockDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            rb.AddForce(-direction * knockbackPower);
        }
        yield return 0;
    }

}
