using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Gamestate gamestate;
    private Vector3 endPos;
    private Vector3 startPos;
    public Canvas UI;
    public Text manaText;
    private RectTransform manaTransform;
    private float scaleX;

    void Start()
    {
        gamestate = Gamestate.gamestate;
        gamestate.SetManaBar(this);
        scaleX = UI.GetComponent<RectTransform>().localScale.x;
        manaTransform = GetComponent<RectTransform>();
        startPos = manaTransform.position;
        endPos = new Vector3(manaTransform.position.x - manaTransform.rect.width * scaleX, manaTransform.position.y, manaTransform.position.z);
    }

    public void setzeManaBar(float maxMana, float currentMana)
    {
       manaTransform.position = Vector3.Lerp(endPos, startPos, currentMana/maxMana);
        manaText.text = currentMana.ToString("F0") + "/" + maxMana.ToString("F0");
    }
}
