﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroState : IState
{
    float randomTime;
    float timer;
    public void OnEnter(Enemy enemy)
    {
        
        timer = 0;
        randomTime = Random.Range(5f, 10f);
        enemy.Moving(); // Bắt đầu di chuyển
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (enemy.Target != null) {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            if (enemy.IsTargetInRange())
            {
                enemy.changeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }

           

           
        }
        else
        {
            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.changeState(new idleState());
            }
        }
        
       
    }

    public void OnExit(Enemy enemy)
    {
        enemy.StopMmoving(); // Dừng di chuyển khi thoát khỏi trạng thái tuần tra
    }

}
