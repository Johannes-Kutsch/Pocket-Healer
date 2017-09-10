using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for displaying a single combat text (the floating numbers on damage/heal events)
/// </summary>
public class CombatText : MonoBehaviour {
    private float speed = 0.4f;
    private Vector3 direction = new Vector3(0, 1, 0);

    /// <summary>
    /// Called in each simulation tick i.e. 50 times a second.
    /// </summary>
    void FixedUpdate()
    {
        float translation = speed * Time.deltaTime;
        transform.Translate(direction * translation);
    }

    /// <summary>
    /// Initializes the combat text at the specified position.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="color">The color.</param>
    /// <param name="text">The text.</param>
    public void Initialize(Vector3 position, Color32 color, string text)
    {
        GetComponent<RectTransform>().localPosition = position;
        GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
        GetComponent<RectTransform>().localScale = new Vector3(0.009f, 0.015f, 1f);
        GetComponent<Text>().color = color;
        GetComponent<Text>().text = text;
        StartCoroutine(Wait(1f));
    }

    /// <summary>
    /// Waits the specified time.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns></returns>
    private IEnumerator Wait(float time)
    {
        float progress = 0f;
        while (progress < 1)
        {
            progress += time * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Fadeout(5f));
    }

    /// <summary>
    /// Fadeouts the text over a specified duration.
    /// </summary>
    /// <param name="duration">The duration.</param>
    /// <returns></returns>
    private IEnumerator Fadeout(float duration)
    {
        float startAlpha = GetComponent<Text>().color.a;
        float progress = 0f;
        while (progress < 1)
        {
            Color tmpColor = GetComponent<Text>().color;

            GetComponent<Text>().color = new Color(tmpColor.r,tmpColor.b,tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));

            progress += duration * Time.deltaTime;

            yield return null;
        }
        Destroy(gameObject);
    }
}
