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

    private float healAmount = 20f;
    private float ticks = 5f;
    

    void Awake()
    {
        base.castSound = Resources.Load("HymneCast", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

    /// <summary>
    /// Called when a cast is started. Initiates the HymnChannel coroutine.
    /// </summary>
    public override void OnCastStart()
    {
        StartCoroutine(HymnChannel());
    }

    /// <summary>
    /// Heals every raider every "castTime / (ticks - 1) - 0.05f" seconds. If the corresponding talent is chosen a hot is applied to every raider subsequently.
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

            yield return new WaitForSeconds(castTime / (ticks - 1) - 0.05f);
        }

        foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders())
        {
            raider.Heal(healAmount);
            if (GameControl.control.talente[5])
            {
                GenerateHot(raider);
            }
        }
    }

    /// <summary>
    /// Method used to generate the hot for a target.
    /// </summary>
    /// <param name="target">The target.</param>
    private void GenerateHot(IRaider target)
    {
        if (!target.GetGameObject().GetComponent<HymneBuff>())
        {
            HymneBuff hot = target.GetGameObject().AddComponent<HymneBuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(hot);
        }
        else
        {
            target.GetGameObject().GetComponent<HymneBuff>().Reset();
        }
    }
}
