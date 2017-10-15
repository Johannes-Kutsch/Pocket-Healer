using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script heals every raider with every fixed update. It is used during developmenet to test level mechanics without needing to play the levels.
/// </summary>
public class HealRaidersToFull : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        List<Raider>  targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());

        foreach (Raider raider in targetDict)
        {
            raider.HealSimple(2000, false);
        }
    }
}
