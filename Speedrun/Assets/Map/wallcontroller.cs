using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallcontroller : MonoBehaviour
{
    public GameObject target;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.SetTrigger("buttonDown");
            Destroy(target);
        }
    }
}
