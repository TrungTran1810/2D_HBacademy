using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTEXT : MonoBehaviour
{
    [SerializeField] private Text HpText;

    public void OnInit(float damege)
    {
        HpText.text=damege.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
