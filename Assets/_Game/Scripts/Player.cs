using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charactor
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float Speed;
    
    [SerializeField] float jumpForce=350f;

    [SerializeField] Kunai kuniaPrebabs;
    [SerializeField] Transform ThrowPoint;
    [SerializeField] GameObject attackArea;
  
    bool isGrounded;
    bool isJumping;
    bool isAttack;
    bool isDeath=false;

    float horizontal;

   

    int Coin=0;
    Vector3 SavePoin;

    private void Awake()
    {
        Coin = PlayerPrefs.GetInt("Coin", 0);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (IsDead)
        {
            return;
        }
        
        isGrounded = CheckGrounded();
       // horizontal = Input.GetAxisRaw("Horizontal");

        if (isAttack) { 
            rb.velocity = Vector2.zero;
            return;
        }

        if (isGrounded) {

            if (isJumping) { 
               return;
            } 
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("Run");
            }

            //Attack
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
            }

            //Throw
            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }
          


            }
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("Fall");
            isJumping = false;
        }


        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnim("Run");
            rb.velocity = new Vector2(horizontal * Time.deltaTime * Speed, rb.velocity.y);
            transform.rotation=Quaternion.Euler(new Vector3(0,horizontal>0 ? 0 : 180, 0 ));
        }

        else if (isGrounded && !isAttack && !isJumping)
        {
            ChangeAnim("Idle");
             rb.velocity = Vector2.zero;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        
        isAttack = false;

        transform.position = SavePoin;
        ChangeAnim("Idle"); 
        DeActiveAttack();

        savePoint();
        UIManager.instance.setCoin(Coin);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
       
    }
    // isGrounded = true
    bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.6f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);

        return hit.collider != null;
    }
   public void Attack()
    {
        if (!isAttack)
        {
            ChangeAnim("Attack");
            isAttack = true;
            Invoke(nameof(ResetAttack), 0.5f);
            ActiveAttack();
            Invoke(nameof(DeActiveAttack), 0.5f);
        }
       
    }
   public void Throw()
    {
        if (!isAttack)
        {
            ChangeAnim("Throw");
            isAttack = true;
            Invoke(nameof(ResetAttack), 0.5f);
            Instantiate(kuniaPrebabs,ThrowPoint.position,ThrowPoint.rotation);
        }
    }
    void ResetAttack()
    {
       
        isAttack = false;
        if (isGrounded && Mathf.Abs(horizontal) < 0.1f)
        { Debug.Log("1");
            ChangeAnim("Idle");
        }
    }
   public  void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x,0);
        isJumping = true;
        ChangeAnim("Jump");
        rb.AddForce(jumpForce * Vector2.up);
    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin") {
            Coin++;
            PlayerPrefs.SetInt("Coin",Coin);
            UIManager.instance.setCoin(Coin);
         Destroy(collision.gameObject);
        }
        if (collision.tag == "Deathzone")
        {
            
            ChangeAnim("Die");
            Invoke(nameof(OnInit), 1f);
        }
    }

    internal void savePoint()
    {
      SavePoin=transform.position;
    }
    void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
}

