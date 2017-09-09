using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RaiderDB {
    public System.Random _random = new System.Random();
    private GameObject myGO;
    private int n;
    private static RaiderDB self;
    private List<IRaider> raiderDict;
    private List<IRaider> tankDict;
    private List<IRaider> ddDict;

    private RaiderDB()
    {
        raiderDict = new List<IRaider>();
        tankDict = new List<IRaider>();
        ddDict = new List<IRaider>();
    }

    public void RegisterRaider(IRaider raider)
    {
        raiderDict.Add(raider);
    }

    public void DeRegisterRaider(IRaider raider)
    {
        raiderDict.Remove(raider);
    }

    public void RegisterTank(IRaider raider)
    {
        raiderDict.Add(raider);
        tankDict.Add(raider);
    }

    public void DeRegisterTank(IRaider raider)
    {
        raiderDict.Remove(raider);
        tankDict.Remove(raider);
    }

    public void RegisterDD(IRaider raider)
    {
        raiderDict.Add(raider);
        ddDict.Add(raider);
    }

    public void DeRegisterDD(IRaider raider)
    {
        raiderDict.Remove(raider);
        ddDict.Remove(raider);
    }

    public List<IRaider> GetAllRaider()
    {
        return raiderDict.OrderBy(o => Random.Range(1, 100)).ToList();
    }

    public List<IRaider> GetAllTanks()
    {
        return tankDict.OrderBy(o => Random.Range(1, 100)).ToList();
    }

    public List<IRaider> GetAllDDs()
    {
        return ddDict.OrderBy(o => Random.Range(1, 100)).ToList();
    }

    public static RaiderDB GetInstance()
    {
        if (self == null)
        {
            self = new RaiderDB();
        }
        return self;
    }

    public List<IRaider> GetAllRaiderSortetByHealth()
    {
        return raiderDict.OrderBy(o => o.GetHealth()).ToList();
    }


}