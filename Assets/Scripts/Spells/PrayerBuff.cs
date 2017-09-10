using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PrayerBuff : MonoBehaviour, IBuff {
    private List<IRaider> raiderDict;
    public Material image;
    public float heal = 40f;
    public float duration = 10f;
    public float runtime;
    public float timeLeft;
    public int jumps = 4;

    private IRaider raider;

    void Start()
    {
        image = Resources.Load("PrayerOfMending_Buff", typeof(Material)) as Material;
    }

    void FixedUpdate()
    {
        runtime = runtime + (float)0.02;
        timeLeft = duration - runtime;
        if(timeLeft <= 0)
        {
            Destroy();
        }
    }

    public void Reset()
    {
        runtime = 0;
        jumps = 4;
    }

    public void jump()
    {
        raider = GetComponent<IRaider>();
        raider.IncreaseHP(heal);
        if (jumps > 0)
        {
            raiderDict = RaiderDB.GetInstance().GetAllRaiderSortetByHealth();
            raiderDict.Remove(raider);
            IRaider target;
            int anzahlRaider = raiderDict.Count;
            for (int i = 0; i < anzahlRaider; i ++)
            {
                target = raiderDict.First();
                if(!target.GetGameObject().GetComponent<PrayerBuff>())
                {
                    target = raiderDict.First();
                    PrayerBuff buff = target.GetGameObject().AddComponent<PrayerBuff>();
                    target.GetGameObject().GetComponent<PrayerBuff>().jumps = jumps - 1;
                    target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
                    break;
                }
                raiderDict.Remove(target);
            }
        }
        Destroy();
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
        jump();
        return amount;
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
