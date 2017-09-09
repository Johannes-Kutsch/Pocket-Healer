using UnityEngine;
using System.Collections;

public class SelectButtonOben : MonoBehaviour {
    public int spellId;
    public int freigeschaltetAb;

    void Start()
    {
        if(freigeschaltetAb > GameControl.control.maxLevelIdUnlocked)
        {
            GetComponent<MeshRenderer>().material = Resources.Load("UnknownSkill_BW", typeof(Material)) as Material;
            Destroy(this);
        }
    }


    void OnMouseDown()
    {
        GameControl.control.selectedSpellId = spellId;
        Tooltips.tooltips.UpdateTooltip(spellId);
    }
}
