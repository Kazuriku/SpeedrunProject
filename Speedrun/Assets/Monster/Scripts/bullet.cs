using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float dieTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Timer", dieTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Die();
        }
    }
    void Timer()
    {
        Die();
        Invoke("Timer", dieTime);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
