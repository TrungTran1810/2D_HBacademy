using System;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] protected CombatTEXT combatTEXTprefab;
    float HP;
    string CurrenAnim;

    public bool IsDead => HP <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        HP = 100;

        if (heathBar != null)
        {
            heathBar.OnInit(100,transform);
        }
        else
        {
            Debug.LogError("HeathBar is not assigned!");
        }
    }

    public virtual void OnDespawn()
    {
        // Despawn logic
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("Die");
        Invoke(nameof(OnDespawn), 2f);
    }

    protected void ChangeAnim(string nameAnim)
    {
        if (Anim == null)
        {
            Debug.LogError("Animator is not assigned!");
            return;
        }

        if (CurrenAnim != nameAnim)
        {
            Anim.ResetTrigger(nameAnim);
            CurrenAnim = nameAnim;
            Anim.SetTrigger(CurrenAnim);
        }
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            HP -= damage;

            if (IsDead)
            {
                HP = 0;
                OnDeath();
            }
            heathBar.SetNewHP(HP);
            Instantiate(combatTEXTprefab,transform.position+Vector3.up,Quaternion.identity).OnInit(damage);
        }
    }
}
