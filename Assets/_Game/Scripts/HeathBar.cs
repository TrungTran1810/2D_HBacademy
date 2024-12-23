using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;
    float HP;
    float MaxHP;
    Transform target;
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount,HP/MaxHP,Time.deltaTime*5f);
        transform.position=target.position+offset;
    }
    public void OnInit(float MaxHP,Transform target)
    {
        this.target = target;
        this.MaxHP = MaxHP;
        HP = MaxHP;
        imageFill.fillAmount = 1;
    }
    public void SetNewHP(float HP)
    {
        this.HP = HP;
       
    }
}
