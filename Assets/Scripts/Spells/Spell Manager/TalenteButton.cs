using UnityEngine;
using System.Collections;

public class TalenteButton : MonoBehaviour {
    public int talentId;
    public int talentIdNachbarOne;
    public int talentIdNachbarTwo;
    public int talentIdNachbarThree;
    public int freigeschaltetAb;
    public Material picture;
    public Material pictureBW;

    void Start()
    {
        if (freigeschaltetAb > GameControl.control.maxLevelIdUnlocked)
        {
            GetComponent<MeshRenderer>().material = Resources.Load("UnknownSkill_BW", typeof(Material)) as Material;
            Destroy(this);
        }
    }

    void Update()
    {
        UpdatePicture();
    }

    public void UpdatePicture()
    {
        if (GameControl.control.talente[talentId])
        {
            GetComponent<MeshRenderer>().material = picture;
        }
        else
        {
            GetComponent<MeshRenderer>().material = pictureBW;
        }
    }

    void OnMouseDown()
    {
        TooltipTalente.tooltips.UpdateTooltip(talentId);
        GameControl.control.talente[talentId] = true;
        GameControl.control.talente[talentIdNachbarOne] = false;
        GameControl.control.talente[talentIdNachbarTwo] = false;
        GameControl.control.talente[talentIdNachbarThree] = false;
    }
}
