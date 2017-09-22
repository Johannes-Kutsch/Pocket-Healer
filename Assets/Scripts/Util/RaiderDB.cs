using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This class represents a database consisting of 3 dictionaries, one containing all raiders, one containing all tanks and one containing all dds.
/// A tank is also a raider, a dd is also a raider but a raider does not have to be a tank or dd.
/// </summary>
public class RaiderDB {
    public System.Random _random = new System.Random();
    private GameObject myGO;
    private int n;
    private static RaiderDB self;
    private List<IRaider> raiderDict;
    private List<IRaider> tankDict;
    private List<IRaider> ddDict;

    /// <summary>
    /// Constructor, creates the dictionaries.
    /// </summary>
    private RaiderDB()
    {
        raiderDict = new List<IRaider>();
        tankDict = new List<IRaider>();
        ddDict = new List<IRaider>();
    }

    /// <summary>
    /// Makes sure the is only one Instance of this class.
    /// </summary>
    /// <returns></returns>
    public static RaiderDB GetInstance()
    {
        if (self == null)
        {
            self = new RaiderDB();
        }
        return self;
    }

    /// <summary>
    /// Registers a raider.
    /// </summary>
    /// <param name="raider">The raider.</param>
    public void RegisterRaider(IRaider raider)
    {
        raiderDict.Add(raider);
    }

    /// <summary>
    /// Deregisters a raider.
    /// </summary>
    /// <param name="raider">The raider.</param>
    public void DeRegisterRaider(IRaider raider)
    {
        if (raiderDict.Contains(raider))
        {
            raiderDict.Remove(raider);
        }
    }

    /// <summary>
    /// Registers a tank.
    /// </summary>
    /// <param name="tank">The tank.</param>
    public void RegisterTank(IRaider tank)
    {
        raiderDict.Add(tank);
        tankDict.Add(tank);
    }

    /// <summary>
    /// Deregisters a tank.
    /// </summary>
    /// <param name="tank">The tank.</param>
    public void DeRegisterTank(IRaider tank)
    {
        if (raiderDict.Contains(tank))
        {
            raiderDict.Remove(tank);
        }

        if(tankDict.Contains(tank))
        {
        tankDict.Remove(tank);
        }
    }

    /// <summary>
    /// Registers a dd.
    /// </summary>
    /// <param name="dd">The dd.</param>
    public void RegisterDD(IRaider dd)
    {
        raiderDict.Add(dd);
        ddDict.Add(dd);
    }

    /// <summary>
    /// Deregisters a dd.
    /// </summary>
    /// <param name="dd">The dd.</param>
    public void DeRegisterDD(IRaider dd)
    {
        if (raiderDict.Contains(dd))
        {
            raiderDict.Remove(dd);
        }

        if (ddDict.Contains(dd))
        {
            ddDict.Remove(dd);
        }
    }

    /// <summary>
    /// Gets all raiders in random order.
    /// </summary>
    /// <returns>a list containing all raiders in random order</returns>
    public List<IRaider> GetAllRaiders()
    {
        return raiderDict.OrderBy(o => Random.Range(1, 100)).ToList();
    }

    /// <summary>
    /// Gets all tanks in random order.
    /// </summary>
    /// <returns>a list containing all tanks in random order</returns>
    public List<IRaider> GetAllTanks()
    {
        return tankDict.OrderBy(o => Random.Range(1, 100)).ToList();
    }

    /// <summary>
    /// Gets all dds in random order.
    /// </summary>
    /// <returns>a list containing all dds in random order</returns>
    public List<IRaider> GetAllDDs()
    {
        return ddDict.OrderBy(o => Random.Range(1, 100)).ToList();
    }

    /// <summary>
    /// Gets all raiders sorted by health. (ascending, lowest health first)
    /// </summary>
    /// <returns>a list containing all raiders sorted by health</returns>
    public List<IRaider> GetAllRaiderSortedByHealth()
    {
        return raiderDict.OrderBy(o => o.GetHealth()).ToList();
    }


}