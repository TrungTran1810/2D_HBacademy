using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : IState
{
    float randomTime;
    float timer;
    public void OnEnter(Enemy enemy)
    {
      enemy.StopMmoving();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            enemy.changeState(new PatroState()); // Chuyển sang trạng thái tuần tra
        }
    }


    public void OnExit(Enemy enemy)
    {


    }
}
