using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster1behave : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPos;
    private float shootTime = 1f, shootSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("shoot", 3f);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    void shoot()
    {
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * -1, 0f);
        Invoke("shoot", shootTime);
    }
}
