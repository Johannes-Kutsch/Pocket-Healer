using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RenewHot : MonoBehaviour, IBuff {
    private List<IRaider> raiderDict;
    public Material image;
    public float healPerTick = 10f;
    public float tickLength = 2f;
    public float duration = 10f;
    public float runtime;
    public float timeLeft;
    public int ticksSinceLastEffect = 0;
    public int jumps = 1;

    private IRaider raider;

    void Start()
    {
        image = Resources.Load("Renew_Buff", typeof(Material)) as Material;
        raider = GetComponent<IRaider>();
        StartCoroutine(ApplyHeal());
    }

    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
        timeLeft = duration - runtime;
    }

    public void Reset()
    {
        ticksSinceLastEffect = 0;
        runtime = 0;
        jumps = 1;
    }

    IEnumerator ApplyHeal()
    {
        float ticks = (duration / tickLength);
        for (ticksSinceLastEffect = 0; ticksSinceLastEffect < ticks; ticksSinceLastEffect++)
        {
            yield return new WaitForSeconds(tickLength);
            if (raider != null)
                raider.Heal(healPerTick);
        }
        if (GameControl.control.talente[3])
            jump();
        Destroy();
    }

    public void jump()
    {
        if (jumps > 0)
        {
            raiderDict = RaiderDB.GetInstance().GetAllRaiderSortedByHealth();
            raiderDict.Remove(raider);
            IRaider target;
            int anzahlRaider = raiderDict.Count;
            for (int i = 0; i < anzahlRaider; i++)
            {
                target = raiderDict.First();
                if (!target.GetGameObject().GetComponent<RenewHot>())
                {
                    target = raiderDict.First();
                    RenewHot buff = target.GetGameObject().AddComponent<RenewHot>();
                    target.GetGameObject().GetComponent<RenewHot>().jumps = jumps - 1;
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
        return (duration- runtime).ToString("F0");
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
