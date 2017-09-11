using UnityEngine;
using System.Collections;

/// <summary>
/// Script attached to a talent button during talentselection.
/// </summary>
public class TalenteButton : MonoBehaviour {
    public int talentId;
    public int talentIdNeighourOne;
    public int talentIdNeighbourTwo;
    public int talentIdNeighbourThree;
    public int unlockedAtLevel;
    public Material picture;
    public Material pictureBW;

    /// <summary>
    /// Called on start, loads the "black_white" picture and destroys this script if the talent is not yet unlocked.
    /// </summary>
    void Start()
    {
        if (unlockedAtLevel > GameControl.control.maxLevelIdUnlocked)
        {
            GetComponent<MeshRenderer>().material = Resources.Load("UnknownSkill_BW", typeof(Material)) as Material;
            Destroy(this);
        }
    }

    /// <summary>
    /// Called on every update, refreshes the talent picture.
    /// </summary>
    void Update()
    {
        UpdatePicture(); //This is really sloppy, we should only update when a talent is changed.
    }

    /// <summary>
    /// Updates the picture, normal if the talent is selected, bw if not.
    /// </summary>
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

    /// <summary>
    /// Called when the talent is clicked, updates the tooltip and selects the talent.
    /// </summary>
    void OnMouseDown()
    {
        TooltipTalente.tooltips.UpdateTooltip(talentId);
        GameControl.control.talente[talentId] = true;
        GameControl.control.talente[talentIdNeighourOne] = false;
        GameControl.control.talente[talentIdNeighbourTwo] = false;
        GameControl.control.talente[talentIdNeighbourThree] = false;
    }
}
