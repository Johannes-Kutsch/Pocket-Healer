using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Responsible for displaying tooltips during talent selection.
/// </summary>
public class TooltipTalente : MonoBehaviour
{
    public static TooltipTalente tooltips;
    private string[] beschreibung = new string[23];

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
    void Start()
    {
        beschreibung[0] = "Erneuerung hat keine Abklingzeit mehr."; //ToDo: English
        beschreibung[1] = "Die Zauberzeit von Großer Heilung wird um 25% beschleunigt.";
        beschreibung[2] = "Kreis der Heilung heilt zwei weitere Ziele.";
        beschreibung[3] = "Erneuerung springt auf ein anderes Ziel sobald sie ausläuft";
        beschreibung[4] = "Große Heilung regeneriert nun 5 Mana, heilt dafür aber 25% weniger.";
        beschreibung[5] = "Die Heilung von Hymne der Hoffnung klingt nach und heilt die Gruppe um 25 Leben im verlauf von 10 Sekunden.";
        beschreibung[6] = "Dispell setzt alle Verstärkungszauber auf eurem Ziel zurück.";
        beschreibung[7] = "Kreis der Heilung hat keine Abklingzeit mehr, dafür eine kurze Zauberzeit.";
        beschreibung[8] = "Wenn ein anderes Gruppenmitglied sterben würde, wird der Schutzgeist nun ebenfalls aktiviert. Anschliesend werden beide Ziele vollgeheilt.";
        beschreibung[9] = "Eine Flamme brennt in euren Tanks. 20% aller Heilung wird dupliziert und auf sie geleitet.";
        beschreibung[10] = "Du stellst zum Beginn des Kampfes ein Totem welches alle Überheilung speichert und nach 10 Sekunden gleichmäßig auf den Raid verteilt.";
        beschreibung[11] = "Du stellst zum Beginn des Kampges einen Brunnen welcher jede Sekunde das am schwersten verletzte Gruppenmitglied heilt.";
        beschreibung[12] = "TODO";
        beschreibung[13] = "TODO";
        beschreibung[14] = "TODO";
        beschreibung[15] = "TODO";
        beschreibung[16] = "TODO";
        beschreibung[17] = "TODO";
        beschreibung[18] = "Deine Heilzauber heilen 5% mehr.";
        beschreibung[19] = "Dein Mana ist um 10% erhöht.";
        beschreibung[20] = "Deine Heilzauber heilen an Zielen mit weniger als 30% Gesundheit 25% mehr.";
        beschreibung[21] = "TODO";
        beschreibung[22] = "TODO";
    }

    /// <summary>
    /// Called when a spell is clicked, updates the tooltip text.
    /// </summary>
    /// <param name="spellId">The spell identifier.</param>
    public void UpdateTooltip(int talentId)
    {
        GetComponent<Text>().text = beschreibung[talentId];
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
