using UnityEngine;
using System.Collections;
using System;

public class KillDebuff : MonoBehaviour, IBuff {
    public Material image;
    public float duration = 10f;
    public float runtime;
    private IRaider raider;
    private Color32 debuffColor = new Color32(170, 0, 255, 255);

    void Start()
    {
        image = Resources.Load("Kill_Debuff", typeof(Material)) as Material;
        raider = GetComponent<IRaider>();
        raider.ChangeBackgroundColor(debuffColor);
    }

    void Update()
    {
        if (!(raider == FindObjectOfType<Gamestate>().GetTarget()))
        {
            raider.ChangeBackgroundColor(debuffColor);
        }
    }

    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
        if(duration-runtime <= 0)
        {
            raider.Die();
            GetComponent<BuffManager>().DeregisterBuff(this);
            Destroy(this);
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
        return (duration - runtime).ToString("F0");
    }

    public bool IsBuff()
    {
        return false;
    }

    public void Destroy()
    {
        if (raider == FindObjectOfType<Gamestate>().GetTarget())
        {
            raider.ChangeBackgroundColor(raider.GetTargetColor());
        } else
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
