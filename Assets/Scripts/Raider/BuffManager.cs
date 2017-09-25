using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The Buff Manager manages all active buffs (and debuffs) for a raider.
/// This includes keeping track of all active buffs and displaying the 3 buffs with the shortest duration.
/// </summary>
public class BuffManager : MonoBehaviour {
    public Image imageOne; //Images for Buffs/Debuffs
    public Image imageTwo;
    public Image imageThree;

    public float amountOfBuffs;
    private List<IBuff> buffDict = new List<IBuff>();
    private Image[] imageArray = new Image[3];

    /// <summary>
    /// Called on Start.
    /// </summary>
    void Start()
    {
        imageArray[0] = imageOne;
        imageArray[1] = imageTwo;
        imageArray[2] = imageThree;
    }

    /// <summary>
    /// Called in each simulation tick i.e. 50 times a second.
    /// Recalculates the order in which buffs should be displayed.
    /// </summary>
    void FixedUpdate()
    {
        //ToDo this is really messy and only needed when a buff gets uptated, need to implement a better way
        List<IBuff> currentBuffDict = new List<IBuff>(GetAllBuffsSortetByDuration()); //we need to reorder the buffs with every tick, because the duration of buffs can be changed after they are applied
        amountOfBuffs = currentBuffDict.Count;
        for (int i = 0; i < amountOfBuffs && i < imageArray.Length; i++) //we can currently only disply 3 buffs
        {
            imageArray[i].material = currentBuffDict.First().GetMaterial();
            imageArray[i].GetComponentInChildren<Text>().text = currentBuffDict.First().GetRemainingDuration();
            imageArray[i].GetComponentInChildren<Text>().color = new Color32(0, 0, 0, 255);
            currentBuffDict.Remove(currentBuffDict.First());
        }

        for (int i = 2; i >= amountOfBuffs; i--)
        {
            imageArray[i].material = null;
            imageArray[i].GetComponentInChildren<Text>().color = new Color32(0, 0, 0, 0);
        }
    }

    /// <summary>
    /// Registers a buff.
    /// </summary>
    /// <param name="buff">The buff.</param>
    public void RegisterBuff(IBuff buff)
    {
        buffDict.Add(buff);
    }
    
    /// <summary>
    /// Deregisters a buff.
    /// </summary>
    /// <param name="buff">The buff.</param>
    public void DeregisterBuff(IBuff buff)
    {
        buffDict.Remove(buff);
    }

    /// <summary>
    /// Gets all buffs sorted by duration.
    /// </summary>
    /// <returns>a sorted List containing the buffs</returns>
    public List<IBuff> GetAllBuffsSortetByDuration()
    {
        return buffDict.OrderBy(o => o.GetDuration()).ToList();
    }

    /// <summary>
    /// Removes all buffs.
    /// </summary>
    public void ClearBuffs()
    {
        foreach (IBuff buff in buffDict)
        {
            Destroy((Component)buff);
        }
        buffDict.Clear();
    }
}
