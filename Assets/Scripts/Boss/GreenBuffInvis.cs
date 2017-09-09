using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GreenBuffInvis : MonoBehaviour, IBuff, ISchutzgeist
{
    private List<IRaider> raiderDict;
    private float raiderCount;
    public Material image = null;
    private IRaider raider;

    void Start()
    {
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
        return 99999999999;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetRemainingTime()
    {
        return " ";
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
        raiderDict = RaiderDB.GetInstance().GetAllRaider();
        raiderCount = raiderDict.Count;
        foreach(IRaider target in raiderDict)
        {
            target.IncreaseHPSimple(amount / raiderCount/2, false);
        }
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