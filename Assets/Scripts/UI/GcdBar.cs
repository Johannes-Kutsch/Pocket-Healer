using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GcdBar : MonoBehaviour {
    private Gamestate gamestate;
    public CanvasGroup canvasGroup;
    public Canvas UI;
    private Coroutine timer;
    private Vector3 startPos;
    private Vector3 endPos;
    private float scaleX;
    private RectTransform castTransform;
    private List<ISpell> spellDict = new List<ISpell>();

    private float castProgress;
    private float castDuration;
    private float rate;
    private bool isInGcd = false;
    private float fadeSpeed = 3f;
    private float gcdTime = 1f;

    void Start()
    {
        gamestate = Gamestate.gamestate;
        gamestate.SetGcdBar(this);
        scaleX = UI.GetComponent<RectTransform>().localScale.x;
        castTransform = GetComponent<RectTransform>();
        startPos = castTransform.position;
        endPos = new Vector3(castTransform.position.x - castTransform.rect.width * scaleX, castTransform.position.y, castTransform.position.z);
    }

    void FixedUpdate()
    {
        if (castProgress <= 1.0)
        {
            castTransform.position = Vector3.Lerp(startPos, endPos, castProgress);
            castProgress += rate;
        }
        else
        {
            isInGcd = false;
            StartCoroutine("FadeOut");
        }
    }

    public bool GetGcd()
    {
        return isInGcd;
    }

    public float GetGcdTime()
    {
        return gcdTime;
    }

    public void StartGcd()
    {
        isInGcd = true;
        StartCoroutine("FadeIn");
        castProgress = 0f;
        castDuration = gcdTime * 50;
        rate = 1f / castDuration;

        spellDict = gamestate.GetSpells();
        foreach (ISpell spell in spellDict)
        {
            spell.StartGcd();
        }
    }

    private IEnumerator FadeOut()
    {
        StopCoroutine("FadeIn");
        while (canvasGroup.alpha > 0f)
        {
            float newValue = fadeSpeed * Time.deltaTime;

            if ((canvasGroup.alpha - newValue) > 0f)
            {
                canvasGroup.alpha -= newValue;
            }
            else
            {
                canvasGroup.alpha = 0;
            }

            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        StopCoroutine("FadeOut");
        while (canvasGroup.alpha < 1f)
        {
            float newValue = fadeSpeed * Time.deltaTime;

            if ((canvasGroup.alpha + newValue) < 1f)
            {
                canvasGroup.alpha += newValue;
            }
            else
            {
                canvasGroup.alpha = 1;
            }

            yield return null;
        }
    }
}
