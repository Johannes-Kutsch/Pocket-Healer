using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Responsible for displaying tooltips during spellselect.
/// </summary>
public class Tooltips : MonoBehaviour {
    public static Tooltips tooltips;
    private string[] tooltip = new string[13];

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
    void Start ()
    {
        //Renew
        RenewHot renewHot = new RenewHot();
        Renew renew = new Renew();
        renew.Awake();
        tooltip[1] = "Applies a buff that heals the target for " + (renewHot.HEALPERTICK * (renewHot.DURATION / renewHot.INTERVALLTICKS)) + " health over " + renewHot.DURATION + " seconds. ";
        tooltip[1] += GenerateSpellString(renew);

        //greaterHeal
        GreaterHeal greaterHeal = new GreaterHeal();
        greaterHeal.Awake();
        tooltip[2] = "A manaefficient spell that heals the target for " + greaterHeal.healAmount + " health. ";
        tooltip[2] += GenerateSpellString(greaterHeal);


        //placeholder
        CircleOfHealing circleOfHealing = new CircleOfHealing();
        circleOfHealing.Awake();
        tooltip[3] = "A spell that heals the " + circleOfHealing.numberTargets + " partymembers, which currently have the lowest health, for " + circleOfHealing.healAmount + " health each. ";
        tooltip[3] += GenerateSpellString(circleOfHealing);

        //prayer of mending
        PrayerBuff prayerBuff = new PrayerBuff();
        Prayer prayer = new Prayer();
        tooltip[4] = "Places a ward on a target that heals them for " + prayerBuff.HEALAMOUNT + " health the next time it takes damage, and then jumps to another target. Jumps up to " + prayerBuff.jumpsLeft + " times and lasts " + prayerBuff.DURATION + " seconds. ";
        tooltip[4] += GenerateSpellString(prayer);

        //NYI
        tooltip[5] = "NYI";

        //NYI
        tooltip[6] = "NYI";

        //hymn of hope
        Hymn hymn = new Hymn();
        tooltip[7] = "Channels a hymn of hope that heals every partymember for " + (hymn.HEALAMOUNT * hymn.TICKS) + " health over " + hymn.CASTTIME + " seconds. ";
        tooltip[7] += GenerateSpellString(hymn);

        //dispell
        Dispell dispell = new Dispell();
        tooltip[8] = "Removes all dispellable debuffs from your target. ";
        tooltip[8] += GenerateSpellString(dispell);

        //greaterHeal
        FlashHeal flashHeal = new FlashHeal();
        greaterHeal.Awake();
        tooltip[9] = "A fast spell that heals the target for " + flashHeal.HEALAMOUNT + " health. ";
        tooltip[9] += GenerateSpellString(flashHeal);

        //shield
        Shield shield = new Shield();
        ShieldBuff shieldBuff = new ShieldBuff();
        tooltip[10] = "Places a shield on a target that lasts " + shieldBuff.DURATION + " seconds and absorbs the next " + shieldBuff.absorbAmount + " damage it takes. ";
        tooltip[10] += GenerateSpellString(shield);

        //binding heal
        BindingHeal bindingHeal = new BindingHeal();
        tooltip[11] = "A spell that heals the target and the partymember that currently has the lowest health for " + bindingHeal.HEALAMOUNT + " health each. ";
        tooltip[11] += GenerateSpellString(bindingHeal);

        //guardian spirit
        GuardianSpirit guardianSpirit = new GuardianSpirit();
        GuardianSpiritBuff guardianSpiritBuff = new GuardianSpiritBuff();
        tooltip[12] = "Calls upon a guardian spirit to watch over your target for " + guardianSpiritBuff.DURATION + " seconds and preventing the target from dying by sacificing itself, healing the target to full health. ";
        tooltip[12] += GenerateSpellString(guardianSpirit);
    }

    /// <summary>
    /// Called when a spell is clicked, updates the tooltip text.
    /// </summary>
    /// <param name="spellId">The spell identifier.</param>
    public void UpdateTooltip(int spellId)
    {
        GetComponent<Text>().text = tooltip[spellId];
    }

    /// <summary>
    /// Write a text in the tooltip box.
    /// </summary>
    /// <param name="text">The text.</param>
    public void SetText(string text)
    {
        GetComponent<Text>().text = text;
    }

    /// <summary>
    /// Generates a string containing informations for the cast time, mana cost and cooldown of a spell.
    /// </summary>
    /// <param name="spell">The spell.</param>
    /// <returns>the string</returns>
    private string GenerateSpellString(Spell spell)
    {
        string str = "";

        if (spell.GetCastTime() == 0)
        {
            str += "Has no cast time, ";
        }
        else
        {
            str += "Has a cast time of " + spell.GetCastTime() + " seconds, ";
        }

        if (spell.GetManacost() == 0)
        {
            str += "costs no mana ";
        }
        else if (spell.GetManacost() > 0)
        {
            str += "costs " + spell.GetManacost() + " mana ";
        }
        else
        {
            str += "restores " + spell.GetManacost() * -1 + " mana ";
        }

        if (spell.GetCooldown() == 0)
        {
            str += "and has no cooldown.";
        }
        else
        {
            str += "and has a cooldown of " + spell.GetCooldown() + " seconds.";
        }

        return str;

    }
}
