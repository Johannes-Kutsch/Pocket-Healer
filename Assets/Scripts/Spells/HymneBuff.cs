using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HymneBuff : MonoBehaviour, IBuff {
    private List<IRaider> raiderDict;
    public Material image;
    public float healPerTick = 5f;
    public float tickLength = 2f;
    public float duration = 10f;
    public float runtime;
    public float timeLeft;
    public int ticksSinceLastEffect = 0;

    private IRaider raider;

    void Start()
    {
        image = Resources.Load("Hymne_Buff", typeof(Material)) as Material;
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
    }

    IEnumerator ApplyHeal()
    {
        int ticks = (int)(duration / tickLength);
        for (ticksSinceLastEffect = 0; ticksSinceLastEffect < ticks; ticksSinceLastEffect++)
        {
            yield return new WaitForSeconds(tickLength);
            if (raider != null)
                raider.Heal(healPerTick);
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

    public string GetRemainingDuration()
    {
        return (duration - runtime).ToString("F0");
    }

    public float OnGlobalDamageTaken(float amount)
    {
        return amount;
    }

    public float OnGlobalHealingTaken(float amount)
    {
        return amount;
    }

    public float OnHealingTaken(float amount)
    {
        return amount;
    }

    public float OnDamageTaken(float amount)
    {
        return amount;
    }

    public float OnFatalDamage(float amount)
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
