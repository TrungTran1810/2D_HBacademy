using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Enemy : Charactor
{
    [SerializeField] float AttackRange = 1.5f; // Tầm tấn công
    [SerializeField] float MoveSpeed = 5f; // Tốc độ di chuyển
    [SerializeField] Rigidbody2D rb;
    private IState currentState;
    [SerializeField] GameObject attackArea;

    bool isRight=true;
    Charactor target;
    public Charactor Target=>target;
    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }
    internal void SetTarget(Charactor charactor)
    {
        this.target = charactor;
        if (IsTargetInRange())
        {
            changeState(new AttackState());
        }else if (target != null)
        {
            changeState(new PatroState());
        }
        else
        {
            changeState(new idleState());
        }

    }
    public override void OnInit()
    {
        base.OnInit();
        changeState(new idleState()); // Bắt đầu ở trạng thái Idle
        DeActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(heathBar.gameObject);
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        changeState(null);
        base.OnDeath();
    }

    public void changeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void Moving()
    {
        ChangeAnim("Run");
        rb.velocity = transform.right * MoveSpeed; // Di chuyển theo hướng transformS
    }

    public void StopMmoving()
    {
        ChangeAnim("Idle");
        rb.velocity = Vector2.zero; // Dừng di chuyển
    }

    public void Attack()
    {
        ChangeAnim("Attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack),0.5f);
    }

    public bool IsTargetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= AttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

        }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {

            ChangeDirection(!isRight);

        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation=isRight? Quaternion.Euler(Vector3.zero): Quaternion.Euler(Vector3.up*180);
    }

   
    void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }


}
