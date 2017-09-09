using UnityEngine;
using System.Collections;

/// <summary>
/// This class is responsible for managing combat texts (the floating numbers on damage/heal events)
/// </summary>
public class CombatTextManager : MonoBehaviour {
    public static CombatTextManager manager;
    public GameObject dmgTextPrefab;
    public RectTransform canvas;

    /// <summary>
    /// Called when the instance is created.
    /// </summary>
    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Creates the DMG text at a specified position.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="raider">The raider.</param>
    /// <param name="amount">The DMG amount.</param>
    public void CreateDmgText(Vector3 position, IRaider raider, float amount)
    {
        GameObject text = (GameObject)Instantiate(dmgTextPrefab, position, Quaternion.identity);
        text.transform.SetParent(raider.GetGameObject().GetComponentInChildren<Canvas>().transform);
        text.GetComponent<CombatText>().Initialize(new Vector3(-0.15f, 0.5f, -1), new Color32(255,0,0,255), "-" + amount.ToString("F0"));
    }


    /// <summary>
    /// Creates the heal text.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="raider">The raider.</param>
    /// <param name="amount">The Heal amount.</param>
    public void CreateHealText(Vector3 position, IRaider raider, float amount)
    {
        GameObject text = (GameObject)Instantiate(dmgTextPrefab, position, Quaternion.identity);
        text.transform.SetParent(raider.GetGameObject().GetComponentInChildren<Canvas>().transform);
        text.GetComponent<CombatText>().Initialize(new Vector3(0.15f, 0.5f, -1), new Color32(0, 255, 0, 255), amount.ToString("F0"));
    }
}
