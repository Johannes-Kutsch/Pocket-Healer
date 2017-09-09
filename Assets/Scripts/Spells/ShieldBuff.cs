using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShieldBuff : MonoBehaviour, IBuff
{
    private List<IRaider> raiderDict;
    public Material image = Resources.Load("Shield_Buff", typeof(Material)) as Material;
    public float absorb = 80f;
    public float duration = 15f;
    public float runtime;
    public float timeLeft;

    private IRaider raider;

    void Start()
    {
    }

    void FixedUpdate()
    {
        runtime = runtime + (float)0.02;
        timeLeft = duration - runtime;
        if (timeLeft <= 0 || absorb <= 0)
        {
            Destroy();
        }
    }

    public void Reset()
    {
        runtime = 0;
        absorb = 80;
    }

    public float GetDuration()
    {
        return duration;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetRemainingTime()
    {
        return (duration - runtime).ToString("F0");
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
        if (absorb >= amount)
        {
            absorb -= amount;
            return 0;
        }
        else
        {
            absorb -= amount;
            return absorb*-1;
        }
    }

    public float FatalDamage(float amount)
    {
        return amount;
    }

    public bool IsBuff()
    {
        return true;
    }

    public bool IsDispellable()
    {
        return false;
    }

    public void Destroy()
    {
        GetComponent<BuffManager>().DeregisterBuff(this);
        Destroy(this);
    }

}
