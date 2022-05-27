using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster2behave : MonoBehaviour
{
    public GameObject target;
    private Animator animator;
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("encounter", true);
            pushplayer();
        }
    }
    void pushplayer()
    {
        gameObject.tag = "Monster";
        animator.SetBool("attack", true);
        Invoke("attackEnd", 1);
    }
    void attackEnd()
    {
        animator.SetBool("encounter", false);
        gameObject.tag = "Untagged";
    }
}
