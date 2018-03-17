using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This class manages the castbar.
/// </summary>
public class CastBarOne : MonoBehaviour {
    private GamestateOne gamestate;
    public Text castTime;
    public Text spellName;
    public CanvasGroup canvasGroup;
    public Canvas UI;
    private Coroutine timer;
    private Vector3 startPos;
    private Vector3 endPos;
    private float scaleX;
    private RectTransform castTransform;

    private float castProgress; //1 if cast is over, 0 if cast has just startet
    private float castDuration; //duration of the current cast in seconds
    private float castRate;  //increase of castProgress for each FixedUpdate
    private bool isCasting = false; //true if a cast is currently in progress
    private float fadeSpeed = 3f;

    /// <summary>
    /// Called on start.
    /// Sets some variables and calculates the startPos.
    /// </summary>
    void Start()
    {
        gamestate = GamestateOne.gamestate;
        gamestate.SetCastBar(this);
        scaleX = UI.GetComponent<RectTransform>().localScale.x;
        castTransform = GetComponent<RectTransform>();
        endPos = castTransform.position;
        startPos = new Vector3(castTransform.position.x - castTransform.rect.width * scaleX, castTransform.position.y, castTransform.position.z);
    }

    /// <summary>
    /// Called in each simulation tick i.e. 50 times a second.
    /// Advances the castProgress, updates the castduration text and starts the FadeOut Coroutine.
    /// </summary>
    void FixedUpdate()
    {
        if(castProgress < 1.0)
        {
            castTransform.position = Vector3.Lerp(startPos, endPos, castProgress);

            //set text
            float max = castDuration / 50f;
            float aktuell = castProgress * max;
            this.castTime.text = aktuell.ToString("F1") + " / " + max.ToString("F1");
            castProgress += castRate;

        }
        else
        {
            isCasting = false;
            StartCoroutine("FadeOut");
        }
    }

    /// <summary>
    /// Manually disables/enables the ability to cast.
    /// Be carefull when using this while a cast is in progress!
    /// </summary>
    /// <param name="casting">if set to <c>true</c> [casting].</param>
    public void disableCasting(bool casting)
    {
        isCasting = casting;
    }

    /// <summary>
    /// Determines whether a cast is in progrees.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if a cast is in progress; otherwise, <c>false</c>.
    /// </returns>
    public bool IsCasting()
    {
        return isCasting;
    }

    /// <summary>
    /// Starts a new cast.
    /// </summary>
    /// <param name="castTime">The cast time.</param>
    /// <param name="spellName">Name of the spell.</param>
    public void Cast(float castTime, string spellName)
    {
        isCasting = true;
        StartCoroutine("FadeIn");
        castProgress = 0f;
        castDuration = castTime * 50;
        castRate = 1f / castDuration;
        this.spellName.text = spellName;
    }

    /// <summary>
    /// Starts a fadeout of the castbar.
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
    /// Starts a fadein of the castbar.
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
