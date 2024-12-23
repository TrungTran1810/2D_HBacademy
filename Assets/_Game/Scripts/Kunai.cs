using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject Hitvfx;
    public Rigidbody2D rb;
    void Start()
    {
       OnInit();
    }

    public void OnInit()
    {
        rb.velocity = transform.right * 4f;
    }
  
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Charactor>().OnHit(30f);
            Instantiate(Hitvfx,transform.position, transform.rotation);
            OnDespawn();
        }
    }
}
