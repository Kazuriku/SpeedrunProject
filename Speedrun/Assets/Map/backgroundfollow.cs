
using UnityEngine;

public class backgroundfollow : MonoBehaviour
{
    private Rigidbody2D rigid;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
