using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Responsible for displaying tooltips during spellselect.
/// </summary>
public class Tooltips : MonoBehaviour {
    public static Tooltips tooltips;
    private string[] beschreibung = new string[13];

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (tooltips == null)
        {
            tooltips = this;
        }
        else if (tooltips != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called on start, sets the tooltip texts.
    /// </summary>
    void Start ()
    {
        beschreibung[1] = "Ein Spontanzauber der ein Ziel über Zeit heilt. Hat eine kurze Abklingzeit und kostet wenig Mana."; //ToDo: English
        beschreibung[2] = "Eine Manaeffiziente Heilung mit langer Zauberzeit. Hat keine Abklingzeit und kostet wenig Mana.";
        beschreibung[3] = "Ein Spontanzauber der mehrere Ziele heilt. Hat eine moderate Abklingzeit und kostet viel Mana.";
        beschreibung[4] = "Ein Verstärkungszauber der sein Ziel heilt sobald es das nächste mal Schaden kriegt und dann auf einen anderen Spieler springt. Hat eine moderate Abklingzeit und kostet eine moderate Menge an Mana.";
        beschreibung[5] = "TODO";
        beschreibung[6] = "TODO";
        beschreibung[7] = "Kanalisiert eine Hymne der Hoffnung welche die gesamte Gruppe heilt. Hat eine hohe Abklingzeit und kostet viel Mana.";
        beschreibung[8] = "Entfernt sofort alle negativen Effekte von einem Ziel. Hat keine Abklingzeit und kostet wenig Mana.";
        beschreibung[9] = "Eine schnelle Heilung die ein Ziel heilt. Hat keine Abklingzeit und kostet eine moderate Menge an Mana.";
        beschreibung[10] = "Schildet ein Ziel und absorbiert eine große Menge an Schaden. Hat eine kurze Abklingzeit und kostet eine moderate Menge an Mana.";
        beschreibung[11] = "Eine Heilung die euer Ziel und das am schwerste verwundete Gruppenmitglied heilt. Hate keine Abklingzeit und kostet eine moderate Menge an Mana.";
        beschreibung[12] = "Beschwört einen Schutzgeist der sein Ziel bewacht und verhindert das es sterben kann. Hat eine moderate Abklingzeit und kostet eine moderate Menge an Mana.";
    }

    /// <summary>
    /// Called when a spell is clicked, updates the tooltip text.
    /// </summary>
    /// <param name="spellId">The spell identifier.</param>
    public void UpdateTooltip(int spellId)
    {
        GetComponent<Text>().text = beschreibung[spellId];
    }

    /// <summary>
    /// Write a text in the tooltip box.
    /// </summary>
    /// <param name="text">The text.</param>
    public void SetText(string text)
    {
        GetComponent<Text>().text = text;
    }
}
