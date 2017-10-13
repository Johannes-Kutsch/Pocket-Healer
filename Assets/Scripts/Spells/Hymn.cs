using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// A channeled spell that heals every raider in a short interval while it is channeled.
/// </summary>
public class Hymn : Spell {
    private readonly string SPELLNAME = "Hymn of Hope";
    private readonly float COOLDOWN = 25f;
    private readonly float MANACOST = 50f;
    private readonly float CASTTIME = 3f;
    private readonly string CASTSOUNDNAME = "HymneCast";
    private readonly string IMPACTSOUNDNAME = null;

    private float healAmount = 20f;
    private float ticks = 5f;
    private float secondsPerTick;

    void Awake()
    {
        secondsPerTick = CASTTIME / (ticks - 1);
    }

    /// <summary>
    /// Called when a cast is started. Initiates the HymnChannel coroutine.
    /// </summary>
    public override void OnCastStart()
    {
        StartCoroutine(HymnChannel());
    }

    /// <summary>
    /// Heals every raider every "castTime / (ticks - 1)" seconds. If the corresponding talent is chosen a hot is applied to every raider subsequently.
    /// </summary>
    /// <returns></returns>
    IEnumerator HymnChannel()
    {
        for (float i = 0; i < ticks; i++)
        {
            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders())
            {
                raider.Heal(healAmount);
            }

            if (i < ticks - 1)
            {
                yield return new WaitForSeconds(secondsPerTick);
            }
        }

        foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders())
        {
            if (GameControl.control.talente[5])
            {
                raider.GetGameObject().AddComponent<HymnBuff>();
            }
        }
    }

    /// <summary>
    /// Gets the spellname.
    /// </summary>
    /// <returns>
    /// the spellname
    /// </returns>
    public override string GetSpellname()
    {
        return SPELLNAME;
    }

    /// <summary>
    /// Gets the cooldown.
    /// </summary>
    /// <returns>
    /// the cooldown
    /// </returns>
    public override float GetCooldown()
    {
        return COOLDOWN;
    }

    /// <summary>
    /// Gets the manacost.
    /// </summary>
    /// <returns>
    /// the manacost
    /// </returns>
    public override float GetManacost()
    {
        return MANACOST;
    }

    /// <summary>
    /// Gets the cast time.
    /// </summary>
    /// <returns>
    /// the cast time
    /// </returns>
    public override float GetCastTime()
    {
        return CASTTIME;
    }

    /// <summary>
    /// Gets the name of the cast sound.
    /// </summary>
    /// <returns>
    /// the name of the cast sound
    /// </returns>
    public override string GetCastSoundName()
    {
        return CASTSOUNDNAME;
    }

    /// <summary>
    /// Gets the name of the impact sound.
    /// </summary>
    /// <returns>
    /// the name of the impact sound
    /// </returns>
    public override string GetImpactSoundName()
    {
        return IMPACTSOUNDNAME;
    }
}
