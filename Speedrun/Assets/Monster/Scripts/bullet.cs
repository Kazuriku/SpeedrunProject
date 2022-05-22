using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float dieTime = 3f;
    public float knockPower = 0.5f;
    public float knockDuration = 4;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Timer", dieTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }*/
    void Timer()
    {
        Die();
        Invoke("Timer", dieTime);
    }

    void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(PlayerMove.instance.Knockback(knockDuration, knockPower, this.transform));
            Die();
        }
    }
}
