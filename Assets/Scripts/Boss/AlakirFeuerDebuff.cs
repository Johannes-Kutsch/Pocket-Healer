using UnityEngine;
using System.Collections;
using System;

public class AlakirFeuerDebuff : MonoBehaviour, IBuff
{
    //private Color32 debuffColor = new Color32(170, 0, 255, 255);
    public Material image;
    public float damagePerTick = 20f;
    public float tickLength = 1.5f;
    private IRaider raider;

    void Start()
    {
        image = Resources.Load("Feuer_Debuff", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
        }
        raider = GetComponent<IRaider>();
        StartCoroutine(ApplyDamage());
    }

    IEnumerator ApplyDamage()
    {
        while (1 == 1)
        {
            yield return new WaitForSeconds(tickLength);
            raider.ReduceHP(damagePerTick);
        }
    }

    public float GetDuration()
    {
        return -1;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetRemainingTime()
    {
        return (" ");
    }

    public bool IsBuff()
    {
        return false;
    }

    public void Destroy()
    {
        GetComponent<BuffManager>().DeregisterBuff(this);
        Destroy(this);
    }

    public float GlobalDamageTaken(float amount)
    {
        return amount;
    }

    public float GlobalHealingTaken(float amount)
    {
        return amount;
    }

    public float HealingTaken(float amount)
    {
        return amount;
    }

    public float DamageTaken(float amount)
    {
        return amount;
    }

    public float FatalDamage(float amount)
    {
        return amount;
    }

    public bool IsDispellable()
    {
        return false;
    }

    public void Reset()
    {
    }
}
