using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class contains a series of ability description for bosses, used for the boss description display during the level select scene.
/// </summary>
public class BossDescription : MonoBehaviour {
    public Text name;
    public Text description;
    public Text skillOne;
    public Text skillTwo;
    public Text skillThree;
    public Text skillFour;
    public Text skillFive;
    public Image skillOneImage;
    public Image skillTwoImage;
    public Image skillThreeImage;
    public Image skillFourImage;
    public Image skillFiveImage;

    /// <summary>
    /// Called on start, loads the images and texts associated to the current level in the description.
    /// </summary>
    void Start () {
        switch (GameControl.control.currentLevelId)
        {
            case 1:
                LevelOne();
                break;
            case 2:
                LevelTwo();
                break;
            case 3:
                LevelThree();
                break;
            case 4:
                LevelFour();
                break;
            case 5:
                LevelFive();
                break;
            case 6:
                LevelSix();
                break;
            case 7:
                LevelSeven();
                break;
            case 8:
                LevelEight();
                break;
            case 9:
                LevelNine();
                break;
            case 10:
                LevelTen();
                break;
            case 11:
                LevelEleven();
                break;
            case 12:
                LevelTwelve();
                break;
        }
    }

    //ToDo: English

    /// <summary>
    /// Loads images and texts for level 1 in the description.
    /// </summary>
    public void LevelOne()
    {
        name.text = "Hogger";
        description.text = "Hogger ist ein kleiner Gnoll, der das örtliche Farmland unsicher macht. Stelle dich im um den Bauern wieder ein sicheres Leben zu ermöglichen und erste Kampferfahrungen zu sammeln.";
        skillOneImage.sprite = Resources.Load("Klaue", typeof(Sprite)) as Sprite;
        skillOne.text = "Hoggers schlägt mit seinen starken Klauen nach deinem Tank.";
        skillTwoImage.sprite = Resources.Load("Giftwolke", typeof(Sprite)) as Sprite;
        skillTwo.text = "Hogger wird von einer Giftwolke umgeben welche regelmäßig einzelne Spieler verletzt.";
        skillThreeImage.sprite = Resources.Load("ThrowRock", typeof(Sprite)) as Sprite;
        skillThree.text = "Hogger wirft regelmäßig mit einem Stein nach einem Mitspieler.";
    }

    /// <summary>
    /// Loads images and texts for level 2 in the description.
    /// </summary>
    public void LevelTwo()
    {
        name.text = "Horridon";
        description.text = "Horridon ist ein stachelieges, gepanzertes Reptiel das sich in einen nahen Wald verirrt hat. Töte ihn um das Land ein kleines Stück sicherer zu machen.";
        skillOneImage.sprite = Resources.Load("Klaue", typeof(Sprite)) as Sprite;
        skillOne.text = "Horridon schlägt mit seinen starken Klauen nach deinem Tank.";
        skillTwoImage.sprite = Resources.Load("Stacheln", typeof(Sprite)) as Sprite;
        skillTwo.text = "Horridons Körper ist mit Stacheln übersäht an dem sich einzelne Spieler verletzen.";
        skillThreeImage.sprite = Resources.Load("Trampeln", typeof(Sprite)) as Sprite;
        skillThree.text = "Horridon ist so groß das er deine Gruppe zertrampeln kann.";
    }

    /// <summary>
    /// Loads images and texts for level 3 in the description.
    /// </summary>
    public void LevelThree()
    {
        name.text = "Coldarra";
        description.text = "Coldarra ist ein Drache der die Viehherden einiger localen Bauern stark dezimiert hat. Töte Coldarra um die Region vor einer Hungersnot zu bewahren.";
        skillOneImage.sprite = Resources.Load("DragonKlaue", typeof(Sprite)) as Sprite;
        skillOne.text = "Coldarra schlägt mit ihren starken Klauen nach deinen Tanks.";
        skillTwoImage.sprite = Resources.Load("DragonSpikes", typeof(Sprite)) as Sprite;
        skillTwo.text = "Coldarras Körper ist mit Stacheln übersäht an dem sich einzelne Spieler verletzen.";
        skillThreeImage.sprite = Resources.Load("FeuerAtem", typeof(Sprite)) as Sprite;
        skillThree.text = "Coldarra kann mit ihrem Feurigem Atem die ganze Gruppe in Brand stecken.";
        skillFourImage.sprite = Resources.Load("Schwanzschlag", typeof(Sprite)) as Sprite;
        skillFour.text = "Coldarra hohlt mit ihrem Schwanz aus und schlägt einzelne Spieler zu boden.";
    }

    /// <summary>
    /// Loads images and texts for level 4 in the description.
    /// </summary>
    public void LevelFour()
    {
        name.text = "Ko'ragh";
        description.text = "Ko'ragh ist ein Magier, welcher der dunklen Seite verfallen ist. Er hat einige Kinder aus einem nahem Dorf entführt. Töte ihn und bring die Kinder zu ihren Familien zurück.";
        skillOneImage.sprite = Resources.Load("MagicAutoAttack", typeof(Sprite)) as Sprite;
        skillOne.text = "Ko'ragh schießt Magische geschosse mit seinem Stab.";
        skillTwoImage.sprite = Resources.Load("KillDebuff", typeof(Sprite)) as Sprite;
        skillTwo.text = "Ko'ragh verflucht einen Spieler, welcher dispellt werden muss oder ansonsten stirbt.";
        skillThreeImage.sprite = Resources.Load("Fireball", typeof(Sprite)) as Sprite;
        skillThree.text = "Ko'ragh erzeugt einen großen Feuerball, welcher mehrere Spieler trifft.";
        skillFourImage.sprite = Resources.Load("Blizzard", typeof(Sprite)) as Sprite;
        skillFour.text = "Ko'ragh beschwört einen Blizzard welcher kontinuierlich Schaden an der gesamten Gruppe anrichtet.";
    }

    /// <summary>
    /// Loads images and texts for level 5 in the description.
    /// </summary>
    public void LevelFive()
    {
        name.text = "Kargath";
        description.text = "Kargath ist ein Schurke, der durch Bestechung die Kontrolle über ein kleines Dorf erlangt hat. Töte ihn um das Dorf wieder zu befreien.";
        skillOneImage.sprite = Resources.Load("Dagger", typeof(Sprite)) as Sprite;
        skillOne.text = "Kargath schlägt mit seinen Dolchen nach deinen Tanks.";
        skillTwoImage.sprite = Resources.Load("Shuriken", typeof(Sprite)) as Sprite;
        skillTwo.text = "Kargath wirft regelmäßig mit Shurikens nach deiner Gruppe.";
        skillThreeImage.sprite = Resources.Load("MarkedDebuff", typeof(Sprite)) as Sprite;
        skillThree.text = "Kargath markiert einen Spieler, welche starken Schaden erleidet und mehr Schaden verursacht.";
    }

    /// <summary>
    /// Loads images and texts for level 6 in the description.
    /// </summary>
    public void LevelSix()
    {
        name.text = "Varian";
        description.text = "Varian war einst ein edler Krieger, welcher sich auf die Suche nach ewigem Leben begeben hat und dort dem Wahnsinn verfallen ist. Beende sein Leben mit Würde.";
        skillOneImage.sprite = Resources.Load("Schwert", typeof(Sprite)) as Sprite;
        skillOne.text = "Varian schlägt mit seinem Schwert nach deinen Tanks.";
        skillTwoImage.sprite = Resources.Load("Wahnsinn", typeof(Sprite)) as Sprite;
        skillTwo.text = "Varians Wahnsinn sucht regelmäßig deine Gruppenmitglieder heim und fügt ihnen Schaden zu.";
        skillThreeImage.sprite = Resources.Load("Cleave", typeof(Sprite)) as Sprite;
        skillThree.text = "Varian holt zu einem mächtigen Schlag aus der beide Tanks trifft.";
        skillFourImage.sprite = Resources.Load("Fixate", typeof(Sprite)) as Sprite;
        skillFour.text = "Varian fixiert sich auf einen Tank und deckt ihn mit einer Abfolge von mehreren Schlägen ein.";
    }

    /// <summary>
    /// Loads images and texts for level 7 in the description.
    /// </summary>
    public void LevelSeven()
    {
        name.text = "Tectus";
        description.text = "Tectus ist ein Steingigant der in einer Höhle nahe einer großen Straße haust und Reisenden auflauert. Gerüchte sagen er würde die Reisenden versteinern und in seine Höhle schleppen.";
        skillOneImage.sprite = Resources.Load("StoneFist", typeof(Sprite)) as Sprite;
        skillOne.text = "Tectus schlägt mit seine Steinernen Fäusten nach deinen Tanks.";
        skillTwoImage.sprite = Resources.Load("StoneRange", typeof(Sprite)) as Sprite;
        skillTwo.text = "Tectus reist den Boden auf und fügt einzelnen Gruppenmitgliedern Schaden zu.";
        skillThreeImage.sprite = Resources.Load("Stein_1", typeof(Sprite)) as Sprite;
        skillThree.text = "Das Ziel ist versteinert und erhält Schaden. Wenn es vollgeheilt wird fügt es der Gruppe Schaden zu.";
        skillFourImage.sprite = Resources.Load("Stein_2", typeof(Sprite)) as Sprite;
        skillFour.text = "Das Ziel ist versteinert und erhält regelmäßig Schaden bis es vollgeheilt wird.";
    }

    /// <summary>
    /// Loads images and texts for level 8 in the description.
    /// </summary>
    public void LevelEight()
    {
        name.text = "Brackenspore";
        description.text = "Brackenspore ist ein Sumpfmonster, welches einem düsterem Moor enstammt und nun ein kleines Dorf belauert. Töte es um das Dorf zu beschützen.";
        skillOneImage.sprite = Resources.Load("Ranken", typeof(Sprite)) as Sprite;
        skillOne.text = "Brackenspore schlägt mit seinen Ranken nach deinen Tanks.";
        skillTwoImage.sprite = Resources.Load("Sporen", typeof(Sprite)) as Sprite;
        skillTwo.text = "Brackenspore wird von einer Wolke aus giftigen Sporen umgeben, welche deiner ganzen Gruppe schaden.";
        skillThreeImage.sprite = Resources.Load("Breath", typeof(Sprite)) as Sprite;
        skillThree.text = "Brackenspore entläd regelmäßig seinen fauligen Atem über deiner Gruppe ";
        skillFourImage.sprite = Resources.Load("Pilz", typeof(Sprite)) as Sprite;
        skillFour.text = "Aus dem Boden spriesen regelmäßig Pilze welche sobald geheilt deine Gruppe unterstützen.";
    }

    /// <summary>
    /// Loads images and texts for level 9 in the description.
    /// </summary>
    public void LevelNine()
    {
        name.text = "Valeera";
        description.text = "Valeera ist die Anführerin einer großen Diebesgilde. Töte sie um diese Gilde zu zerschmettern.";
        skillOneImage.sprite = Resources.Load("Dagger", typeof(Sprite)) as Sprite;
        skillOne.text = "Valeera schlägt mit ihrem Dolch nach deinen Tanks.";
        skillTwoImage.sprite = Resources.Load("Cleave", typeof(Sprite)) as Sprite;
        skillTwo.text = "Valeera holt zu einem mächtigen Schlag aus der beide Tanks trifft.";
        skillThreeImage.sprite = Resources.Load("Fixate", typeof(Sprite)) as Sprite;
        skillThree.text = "Valeera fixiert sich auf einen Tank und deckt ihn mit einer Abfolge von mehreren Schlägen ein.";
        skillFourImage.sprite = Resources.Load("Dot_Debuff", typeof(Sprite)) as Sprite;
        skillFour.text = "Valeera vergiftet ein Gruppenmitglied. Dieser Debuff kann dispellt werden.";
        skillFiveImage.sprite = Resources.Load("Heal_Debuff", typeof(Sprite)) as Sprite;
        skillFive.text = "Valeera erschüttert ein Gruppenmitglied und verhindert das es Heilung erhält.";
    }

    /// <summary>
    /// Loads images and texts for level 10 in the description.
    /// </summary>
    public void LevelTen()
    {
        name.text = "Al'akir";
        description.text = "Al'akir ist ein mächtiger Elementarlord. Er wurde korrupiert und versucht nun die Elemente in ein Ungleichgewicht zu bringen.";
        skillOneImage.sprite = Resources.Load("ElementalAttack", typeof(Sprite)) as Sprite;
        skillOne.text = "Al'akir unterwirft die Elemente und fügt einzelnen Spielern regelmäßig Schaden zu.";
        skillTwoImage.sprite = Resources.Load("AlakirPhasen", typeof(Sprite)) as Sprite;
        skillTwo.text = "Al'akir wechselt im Verlauf des Kampfes zwichen einzelnen Phasen.";
        skillThreeImage.sprite = Resources.Load("Feuer_Debuff", typeof(Sprite)) as Sprite;
        skillThree.text = "In der Feuerphase verbrennt Al'akir Spieler, sie erleiden regelmäßig Schaden.";
        skillFourImage.sprite = Resources.Load("Luft", typeof(Sprite)) as Sprite;
        skillFour.text = "In der Luftphase verringert Al'akir langsam die von Spielern erhaltene Heilung.";
        skillFiveImage.sprite = Resources.Load("Blizzard", typeof(Sprite)) as Sprite;
        skillFive.text = "In der Wasserphase zieht ein Blizzard auf der Verbrennungen entfernt und Schaden verursacht.";
    }

    /// <summary>
    /// Loads images and texts for level 11 in the description.
    /// </summary>
    public void LevelEleven()
    {
        name.text = "Ultraxion";
        description.text = "Ultraxion ist ein mächtiger dunkler Drache. Er zwingt anderen Drachen seinen Willen auf. Heile diese Drachen damit sie dich im Kampf unterstützen.";
        skillOneImage.sprite = Resources.Load("Claw", typeof(Sprite)) as Sprite;
        skillOne.text = "Ultraxion klauen können beide Tanks gleichzeitig verletzen und er Fixiert regelmäßig einen Tank.";
        skillTwoImage.sprite = Resources.Load("ShadowAura", typeof(Sprite)) as Sprite;
        skillTwo.text = "Ultraxion wird von einer mächtigen Aura umgeben, die regelmäßig Schaden verursacht.";
        skillThreeImage.sprite = Resources.Load("red", typeof(Sprite)) as Sprite;
        skillThree.text = "Der Rote Drache verdoppelt deine Heilung sobald er geheilt wurde.";
        skillFourImage.sprite = Resources.Load("blue", typeof(Sprite)) as Sprite;
        skillFour.text = "Der Blaue Drache füllt sobald er geheilt wurde regelmäßig dein Mana auf.";
        skillFiveImage.sprite = Resources.Load("green", typeof(Sprite)) as Sprite;
        skillFive.text = "Der Grüne Drache dupliziert deine Heilung und verteilt sie gleichmäßig auf deine Gruppe.";
    }

    /// <summary>
    /// Loads images and texts for level 12 in the description.
    /// </summary>
    public void LevelTwelve()
    {
        name.text = "Dia Darkwhisper";
        description.text = "Dot der immer stärker wird, kann dispellt werden und verdoppelt sich dann. In bestimmten intervalls werden alle Dots gecleared. Ein dispell cleared alle aktiven dots!";
        skillOneImage.sprite = Resources.Load("AUTOATTACK", typeof(Sprite)) as Sprite;
        skillOne.text = "AUTOATTACK";
        skillTwoImage.sprite = Resources.Load("DOTLEICHT", typeof(Sprite)) as Sprite;
        skillTwo.text = "PLACEHOLDER";
        skillThreeImage.sprite = Resources.Load("DOTMITTEL", typeof(Sprite)) as Sprite;
        skillThree.text = "PLACEHOLDER";
        skillFourImage.sprite = Resources.Load("DOTSCHWER", typeof(Sprite)) as Sprite;
        skillFour.text = "PLACEHOLDER";
    }
}
