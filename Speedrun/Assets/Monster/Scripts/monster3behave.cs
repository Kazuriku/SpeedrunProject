using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster3behave : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextmove;
    private int movePower = 7;
    private bool facingright = true;
    private Transform target;
    private bool stopfollow;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        followtarget();
        rigid.velocity = new Vector2(nextmove*movePower, rigid.velocity.y);
        //밑에 레이캐스팅해서 절벽확인
        Vector2 frontVec = new Vector2(rigid.position.x + nextmove*3, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 10, LayerMask.GetMask("Ground"));
        if(rayHit.collider == null)
        {
            stopfollow = true;
            if (followtarget())
            {
                moveChange();
            }
        }
        Vector2 rightVec = new Vector2(rigid.position.x + nextmove*3.5f, rigid.position.y);
        RaycastHit2D rightrayHit = Physics2D.Raycast(rightVec, Vector2.right, 3, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rightVec, Vector2.right, Color.green);
        if (rightrayHit.collider != null && followtarget())
        {
            moveChange();
        }
        Vector2 leftVec = new Vector2(rigid.position.x + nextmove * 3.5f, rigid.position.y);
        RaycastHit2D leftrayHit = Physics2D.Raycast(leftVec, Vector2.left, 3, LayerMask.GetMask("Ground"));
        Debug.DrawRay(leftVec, Vector2.left, Color.green);
        if (leftrayHit.collider != null && followtarget())
        {
            moveChange();
        }
    }
    void moveChange()
    {
        nextmove *= -1;
        CancelInvoke();
        Invoke("Think", 4);
        Flip();
    }
    void Think()
    {
        stopfollow = false;
        nextmove = Random.Range(-1,2);
        float thinkTime = Random.Range(2f, 5f);
        if(nextmove < 0 && facingright == true)
        {
            Flip();
        }else if(nextmove > 0 && facingright == false)
        {
            Flip();
        }
        Invoke("Think", thinkTime);
    }
    bool followtarget()
    {
        if (Vector2.Distance(transform.position, target.position) < 20 && transform.position.x-1 > target.position.x)
        {
            if (!stopfollow)
            {
                CancelInvoke();
                if (facingright == true)
                {
                    Flip();
                }
                nextmove = -1;
                return false;
            }
            else
                return true;
        }
        else if (Vector2.Distance(transform.position, target.position) < 20 && transform.position.x+1< target.position.x)
        {
            if (!stopfollow)
            {
                CancelInvoke();
                if (facingright == false)
                {
                    Flip();
                }
                nextmove = 1;
                return false;
            }
            else
                return true;
        }
        else
            return true;
        
    }
    private void Flip()
    {
        if (facingright == true)
        {
            Vector3 currnetscale = gameObject.transform.localScale;
            currnetscale.x *= -1;
            gameObject.transform.localScale = currnetscale;
            facingright = false;
        }
        else if (facingright == false)
        {
            Vector3 currnetscale = gameObject.transform.localScale;
            currnetscale.x *= -1;
            gameObject.transform.localScale = currnetscale;
            facingright = true;
        }
    }
}
