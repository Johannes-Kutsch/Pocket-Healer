using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FlammeBuff : MonoBehaviour, IBuff {
    private List<IRaider> raiderDict;
    public Material image;
    private float scaler = 0.2f;

    private IRaider raider;

    void Start()
    {
        image = Resources.Load("Flamme_Buff", typeof(Material)) as Material;
        raider = GetComponent<IRaider>();
    }

    void FixedUpdate()
    {

    }

    public void Reset()
    {

    }


    public float GetDuration()
    {
        return 100000000000;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetRemainingTime()
    {
        return "";
    }

    public float GlobalDamageTaken(float amount)
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

    public float GlobalHealingTaken(float amount)
    {
        raider.HealSimple(amount * scaler, true);
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