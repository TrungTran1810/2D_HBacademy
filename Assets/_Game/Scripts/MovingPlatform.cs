using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform aPoint, bPoint;
    [SerializeField] float Speed;
    Vector3 target;
    void Start()
    {
        transform.position=aPoint.position;
        target = bPoint.position;
    }

    
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position, target, Speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            target = bPoint.position;
        }else if(Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            target = aPoint.position;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);    
     
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);

        }
    }

}
