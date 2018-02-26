using UnityEngine;
using System.Collections;

/// <summary>
/// Script attached to a talent button during talentselection.
/// </summary>
public class TalenteButton : MonoBehaviour {
    private Shader defaultShader;
    private Shader greyscaleShader;

    public int talentId;
    public int talentIdNeighourOne;
    public int talentIdNeighbourTwo;
    public int talentIdNeighbourThree;
    public int unlockedAtLevel;

    public Material picture;
    public Material pictureBW;
    public TalentManager manager;

    /// <summary>
    /// Called on start, loads the "UnknownSkill_BW" picture and destroys this script if the talent is not yet unlocked.
    /// </summary>
    void Start()
    {
        defaultShader = Shader.Find("Standard");
        greyscaleShader = Shader.Find("Custom/Greyscale");

        manager = transform.parent.gameObject.GetComponent<TalentManager>();

        if (unlockedAtLevel > GameControl.control.maxLevelIdUnlocked)
        {
            GetComponent<MeshRenderer>().material = Resources.Load("UnknownSkill_BW", typeof(Material)) as Material;
            Destroy(this);
        }
            else
        {
            GetComponent<MeshRenderer>().material = picture;
        }

        UpdateShader();
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

        manager.UpdateShaders();
    }

    public void OnDestroy()
    {
        picture.shader = defaultShader;
    }

    /// <summary>
    /// Changes the shader of the picture to greyscale if it is not selected or to default if it is selected.
    /// </summary>
    public void UpdateShader()
    {
        if (GameControl.control.talente[talentId])
        {
            GetComponent<MeshRenderer>().material.shader = defaultShader;
        }
        else
        {
            GetComponent<MeshRenderer>().material.shader = greyscaleShader;
        }
    }
}
