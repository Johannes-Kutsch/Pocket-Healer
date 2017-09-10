using UnityEngine;
using System.Collections;
using System;

public class AlakirWasserDebuff : MonoBehaviour, IBuff
{
    //private Color32 debuffColor = new Color32(170, 0, 255, 255);
    public Material image;
    private float tickLength = 1f;
    private float healMultiplyer = 1f;
    private float healMultiplyerIncrease = 0.05f;
    //private IRaider raider;

    void Start()
    {
        image = Resources.Load("Luft", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            healMultiplyerIncrease *= GameControl.control.easyMultiplyer;
        }
        //raider = GetComponent<IRaider>();
        //raider = GetComponent<IRaider>();
        StartCoroutine(ApplyDamage());
    }

    IEnumerator ApplyDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickLength);
            if (healMultiplyer - healMultiplyerIncrease >= 0)
                healMultiplyer -= healMultiplyerIncrease;
            else
                healMultiplyer = 0;
        }
    }

    public float GetDuration()
    {
        return -2;
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
        return amount * healMultiplyer;
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
