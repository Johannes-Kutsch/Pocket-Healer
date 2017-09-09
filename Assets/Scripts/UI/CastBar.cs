using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CastBar : MonoBehaviour {
    private Gamestate gamestate;
    public Text castTime;
    public Text spellName;
    public CanvasGroup canvasGroup;
    public Canvas UI;
    private Coroutine timer;
    private Vector3 startPos;
    private Vector3 endPos;
    private float scaleX;
    private RectTransform castTransform;

    private float castProgress;
    private float castDuration;
    private float rate;
    private bool isCasting = false;
    private float fadeSpeed = 3f;

    void Start()
    {
        gamestate = Gamestate.gamestate;
        gamestate.SetCastBar(this);
        scaleX = UI.GetComponent<RectTransform>().localScale.x;
        castTransform = GetComponent<RectTransform>();
        endPos = castTransform.position;
        startPos = new Vector3(castTransform.position.x - castTransform.rect.width * scaleX, castTransform.position.y, castTransform.position.z);
    }

    void FixedUpdate()
    {
        if(castProgress <= 1.0)
        {
            castTransform.position = Vector3.Lerp(startPos, endPos, castProgress);
            float max = castDuration / 50f;
            float aktuell = castProgress * max;
            this.castTime.text = aktuell.ToString("F1") + " / " + max.ToString("F1");
            castProgress += rate;
        }
        else
        {
            StartCoroutine("FadeOut");
        }
    }

    public void SetzeCasting(bool casting)
    {
        isCasting = casting;
    }

    public bool IsCasting()
    {
        return isCasting;
    }

    public void Caste(float castTime, string spellName)
    {
        StartCoroutine("FadeIn");
        castProgress = 0f;
        castDuration = castTime * 50;
        rate = 1f / castDuration;
        this.spellName.text = spellName;
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
