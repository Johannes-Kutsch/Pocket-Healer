using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Responsible for displaying tooltips during talent selection.
/// </summary>
public class TooltipTalente : MonoBehaviour
{
    public static TooltipTalente tooltips;
    private string[] beschreibung = new string[23];

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (tooltips == null)
        {
            tooltips = this;
        }
        else if (tooltips != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called on start, sets the tooltip texts.
    /// </summary>
    void Start()
    {
        beschreibung[0] = "Renew no longer has a cooldown.";
        beschreibung[1] = "The cast time of greater heal is decreased by 25%.";
        beschreibung[2] = "Circle of healing heals two additional targets.";
        beschreibung[3] = "The first time renew would expire it jumps to another partymember and heals him for 10 seconds.";
        beschreibung[4] = "Greater heal now regenerates 5 mana but heals 25% less.";
        beschreibung[5] = "The heal of hymn of hope now lingers and heals every partymember for 25 health over 10 seconds.";
        beschreibung[6] = "Dispell now also resets most buffs, as if they where newly applied.";
        beschreibung[7] = "Circle of healing no longer has a cooldown but instead a cast time of 2 seconds.";
        beschreibung[8] = "Guardian spirit now protects every partymember. When guardian spirit triggers on a target it was not applied to the original target is also healed to full.";
        beschreibung[9] = "A Flame of Hope burns in your tanks. 20% of all healing you do is dupplicated and applied to each tank.";
        beschreibung[10] = "At the beggining of each fight you place a totem that stores all overheal you do. Every 10 seconds the totem releases all saved overheal, evenly distributed across your party.";
        beschreibung[11] = "At the beggining of each fight you place a well that heals the most injured party member for 15 health every second.";
        beschreibung[12] = "TODO";
        beschreibung[13] = "TODO";
        beschreibung[14] = "TODO";
        beschreibung[15] = "TODO";
        beschreibung[16] = "TODO";
        beschreibung[17] = "TODO";
        beschreibung[18] = "The healing you do is increased by 5%.";
        beschreibung[19] = "You start with 10% more mana.";
        beschreibung[20] = "Partymembers below 30% health take 25% increased healing.";
        beschreibung[21] = "TODO";
        beschreibung[22] = "TODO";
    }

    /// <summary>
    /// Called when a spell is clicked, updates the tooltip text.
    /// </summary>
    /// <param name="spellId">The spell identifier.</param>
    public void UpdateTooltip(int talentId)
    {
        GetComponent<Text>().text = beschreibung[talentId];
    }

    /// <summary>
    /// Write a text in the tooltip box.
    /// </summary>
    /// <param name="text">The text.</param>
    public void SetText(string text)
    {
        GetComponent<Text>().text = text;
    }
}
