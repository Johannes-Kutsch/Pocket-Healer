using UnityEngine;
using System.Collections;
using System;

public class DotDebuff : MonoBehaviour, IBuff
{
    private Color32 debuffColor = new Color32(170, 0, 255, 255);
    public Material image;
    public float duration = 12f;
    public float runtime;
    public float damagePerTick = 20f;
    public float tickLength = 1.5f;
    private int ticksSinceLastEffect = 0;
    private IRaider raider;

    void Start()
    {
        image = Resources.Load("Dot_Debuff", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
        }
        raider = GetComponent<IRaider>();
        raider.ChangeBackgroundColor(debuffColor);
        StartCoroutine(ApplyDamage());
    }

    void Update()
    {
        if (!(raider == FindObjectOfType<Gamestate>().target))
        {
            raider.ChangeBackgroundColor(debuffColor);
        }
    }

    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
    }

    IEnumerator ApplyDamage()
    {
        float ticks = (duration / tickLength);
        for (ticksSinceLastEffect = 0; ticksSinceLastEffect < ticks; ticksSinceLastEffect++)
        {
            yield return new WaitForSeconds(tickLength);
            raider.ReduceHP(damagePerTick);
        }
        Destroy();
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
        return (duration - runtime).ToString("F0");
    }

    public bool IsBuff()
    {
        return false;
    }

    public void Destroy()
    {
        if (raider == FindObjectOfType<Gamestate>().target)
        {
            raider.ChangeBackgroundColor(raider.GetTargetColor());
        }
        else
        {
            raider.ChangeBackgroundColor(raider.GetNotTargetColor());
        }
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
        return true;
    }

    public void Reset()
    {
        runtime = 0;
    }
}
