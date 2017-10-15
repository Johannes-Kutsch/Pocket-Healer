using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Flamme : MonoBehaviour {
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private string spellName = "Flamme des Glaubens";
    private List<Raider> raiderDict;
    private AudioSource source;

    void Start()
    {
        gamestate = Gamestate.gamestate;
        cooldownOverlay = GetComponentInChildren<Image>();
        source = GetComponent<AudioSource>();
        Cast();
    }

    public void Cast()
    {
        raiderDict = RaiderDB.GetInstance().GetAllTanks();
        foreach (Raider raider in raiderDict)
        {
            GenerateBuff(raider);
        }
    }

    private void GenerateBuff(Raider raider)
    {
        if (!raider.GetGameObject().GetComponent<FlammeBuff>())
        {
            FlammeBuff buff = raider.GetGameObject().AddComponent<FlammeBuff>();
            raider.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
        }
        else
        {
            raider.GetGameObject().GetComponent<FlammeBuff>().Reset();
        }
    }

    public void RemoveSpellFromButton()
    {
        GetComponent<MeshRenderer>().material = null;
        Destroy(this);
    }

}

