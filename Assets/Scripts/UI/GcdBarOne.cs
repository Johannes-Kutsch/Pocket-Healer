using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// This class manages the gcdbar, i.e. the 1 second you have to wait between instant casts.
/// </summary>
public class GcdBarOne : MonoBehaviour {
    private GamestateOne gamestate;
    public CanvasGroup canvasGroup;
    public Canvas UI;
    private Coroutine timer;
    private Vector3 startPos;
    private Vector3 endPos;
    private float scaleX;
    private RectTransform castTransform;
    private List<Spell> spellDict;

    private float gcdDuration = 1f; //duration of gcd in seconds
    private float gcdProgress; //1 if gcd is over, 0 if gcd has just startet
    private float gcdRate; //increase of gcdProgress for each FixedUpdate
    private bool isInGcd = false; //true if a gcd is currently active
    private float fadeSpeed = 3f;


    /// <summary>
    /// Called on start.
    /// Sets some variables, calculates the endPos and gcdRate.
    /// </summary>
    void Start()
    {
        gamestate = GamestateOne.gamestate;
        gamestate.SetGcdBar(this);
        scaleX = UI.GetComponent<RectTransform>().localScale.x;
        castTransform = GetComponent<RectTransform>();
        startPos = castTransform.position;
        endPos = new Vector3(castTransform.position.x - castTransform.rect.width * scaleX, castTransform.position.y, castTransform.position.z);
        spellDict = gamestate.GetSpells();

        gcdRate = 1f / (gcdDuration * 50);
    }

    /// <summary>
    /// Called in each simulation tick i.e. 50 times a second.
    /// Advances the gcdProgress and starts the FadeOut Coroutine.
    /// </summary>
    void FixedUpdate()
    {
        if (gcdProgress < 1.0)
        {
            castTransform.position = Vector3.Lerp(startPos, endPos, gcdProgress);
            gcdProgress += gcdRate;
        }
        else
        {
            isInGcd = false;
            StartCoroutine("FadeOut");
        }
    }

    /// <summary>
    /// Gets isInGcd.
    /// </summary>
    /// <returns><c>true</c> if is in gcd; otherwise, <c>false</c>.</returns>
    public bool GetIsInGcd()
    {
        return isInGcd;
    }

    /// <summary>
    /// Gets the current gcdTime.
    /// </summary>
    /// <returns>the gcdTime</returns>
    public float GetGcdTime()
    {
        return gcdDuration;
    }

    /// <summary>
    /// Starts a gcd.
    /// </summary>
    public void StartGcd()
    {
        isInGcd = true;
        StartCoroutine("FadeIn");
        gcdProgress = 0f;

        foreach (Spell spell in spellDict)
        {
            spell.StartGcd();
        }
    }

    /// <summary>
    /// Starts a fadeout of the gcd bar.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Starts a fadein of the gcd bar
    /// </summary>
    /// <returns></returns>
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
