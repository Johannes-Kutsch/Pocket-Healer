using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for drawing the manabar.
/// </summary>
public class ManaBar : MonoBehaviour
{
    private Gamestate gamestate;
    private Vector3 endPos;
    private Vector3 startPos;
    public Canvas UI;
    public Text manaText;
    private RectTransform manaTransform;
    private float scaleX;

    /// <summary>
    /// Called on Start.
    /// Sets some variables and calculates some positions.
    /// </summary>
    void Start()
    {
        gamestate = Gamestate.gamestate;
        gamestate.SetManaBar(this);
        scaleX = UI.GetComponent<RectTransform>().localScale.x;
        manaTransform = GetComponent<RectTransform>();
        startPos = manaTransform.position;
        endPos = new Vector3(manaTransform.position.x - manaTransform.rect.width * scaleX, manaTransform.position.y, manaTransform.position.z);
    }

    /// <summary>
    /// Sets the mana bar.
    /// </summary>
    /// <param name="maxMana">The maximum mana.</param>
    /// <param name="currentMana">The current mana.</param>
    public void setManaBar(float maxMana, float currentMana)
    {
       manaTransform.position = Vector3.Lerp(endPos, startPos, currentMana/maxMana);
       manaText.text = currentMana.ToString("F0") + "/" + maxMana.ToString("F0");
    }
}
