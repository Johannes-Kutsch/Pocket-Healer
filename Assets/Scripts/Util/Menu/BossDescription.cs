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

    /// <summary>
    /// Loads images and texts for level 1 in the description.
    /// </summary>
    public void LevelOne()
    {
        name.text = "Hogger";
        description.text = "Hogger is a small Gnoll who terrorizes the surrounding farmland. Fight him to gather first combat experiences and make the peasants life more secure.";
        skillOneImage.sprite = Resources.Load("Klaue", typeof(Sprite)) as Sprite;
        skillOne.text = "Hoggers hits your tank with his strong claws.";
        skillTwoImage.sprite = Resources.Load("Giftwolke", typeof(Sprite)) as Sprite;
        skillTwo.text = "Hogger is surrounded by a cloud of poison, which regularly injures individual partymembers.";
        skillThreeImage.sprite = Resources.Load("ThrowRock", typeof(Sprite)) as Sprite;
        skillThree.text = "Hogger regularly throws a stone after fellow partymembers.";
    }

    /// <summary>
    /// Loads images and texts for level 2 in the description.
    /// </summary>
    public void LevelTwo()
    {
        name.text = "Horridon";
        description.text = "Horridon is a prickly, armored reptile that has strayed into a nearby forest. Kill him to make the country a little safer.";
        skillOneImage.sprite = Resources.Load("Klaue", typeof(Sprite)) as Sprite;
        skillOne.text = "Hoggers hits your tank with his strong claws.";
        skillTwoImage.sprite = Resources.Load("Stacheln", typeof(Sprite)) as Sprite;
        skillTwo.text = "Horridon's body is littered with spines on which individual partymembers are injured.";
        skillThreeImage.sprite = Resources.Load("Trampeln", typeof(Sprite)) as Sprite;
        skillThree.text = "Horridon is so big that he can crush your party.";
    }

    /// <summary>
    /// Loads images and texts for level 3 in the description.
    /// </summary>
    public void LevelThree()
    {
        name.text = "Coldarra";
        description.text = "Coldarra is a dragon that has severely decimated the livestock of some local peasants. Kill Coldarra to save the region from famine.";
        skillOneImage.sprite = Resources.Load("DragonKlaue", typeof(Sprite)) as Sprite;
        skillOne.text = "Coldarra hits your tanks with her strong claws.";
        skillTwoImage.sprite = Resources.Load("DragonSpikes", typeof(Sprite)) as Sprite;
        skillTwo.text = "Coldarra's body is littered with spines on which individual partymembers are injured.";
        skillThreeImage.sprite = Resources.Load("FeuerAtem", typeof(Sprite)) as Sprite;
        skillThree.text = "Coldarra can set the whole party on fire with her fiery breath.";
        skillFourImage.sprite = Resources.Load("Schwanzschlag", typeof(Sprite)) as Sprite;
        skillFour.text = "Coldarra has a strong tail with which she can use to beat individual partymembers to ground.";
    }

    /// <summary>
    /// Loads images and texts for level 4 in the description.
    /// </summary>
    public void LevelFour()
    {
        name.text = "Ko'ragh";
        description.text = "Ko'ragh is a magician who has fallen to the dark side. He has kidnapped some children from a nearby village. Kill him and bring the children back to their families.";
        skillOneImage.sprite = Resources.Load("MagicAutoAttack", typeof(Sprite)) as Sprite;
        skillOne.text = "Ko'ragh can shoot magic bolts at your tanks with his staff.";
        skillTwoImage.sprite = Resources.Load("KillDebuff", typeof(Sprite)) as Sprite;
        skillTwo.text = "Ko'ragh curses a partymember who must be dispelled or dies otherwise.";
        skillThreeImage.sprite = Resources.Load("Fireball", typeof(Sprite)) as Sprite;
        skillThree.text = "Ko'ragh creates a large fireball that hits several partymembers.";
        skillFourImage.sprite = Resources.Load("Blizzard", typeof(Sprite)) as Sprite;
        skillFour.text = "Ko'ragh summons a blizzard that causes damage to the entire party.";
    }

    /// <summary>
    /// Loads images and texts for level 5 in the description.
    /// </summary>
    public void LevelFive()
    {
        name.text = "Kargath";
        description.text = "Kargath is a rogue who has gained control of a small village through bribery. Kill him to free the village.";
        skillOneImage.sprite = Resources.Load("Dagger", typeof(Sprite)) as Sprite;
        skillOne.text = "Kargath hits your tanks with his daggers.";
        skillTwoImage.sprite = Resources.Load("Shuriken", typeof(Sprite)) as Sprite;
        skillTwo.text = "Kargath throws her litter stars regularly at your party.";
        skillThreeImage.sprite = Resources.Load("MarkedDebuff", typeof(Sprite)) as Sprite;
        skillThree.text = "Kargath marks a partymember who suffers severe damage but deals more damage in return.";
    }

    /// <summary>
    /// Loads images and texts for level 6 in the description.
    /// </summary>
    public void LevelSix()
    {
        name.text = "Varian";
        description.text = "Varian was once a noble warrior, who has set out on the search for eternal life but fell for madness instead. End his life with dignity.";
        skillOneImage.sprite = Resources.Load("Schwert", typeof(Sprite)) as Sprite;
        skillOne.text = "Varian hits your tanks with his sword.";
        skillTwoImage.sprite = Resources.Load("Wahnsinn", typeof(Sprite)) as Sprite;
        skillTwo.text = "Varian's insanity regularly tempts a party members and damages them.";
        skillThreeImage.sprite = Resources.Load("Cleave", typeof(Sprite)) as Sprite;
        skillThree.text = "Varian takes a powerful punch which hits both tanks.";
        skillFourImage.sprite = Resources.Load("Fixate", typeof(Sprite)) as Sprite;
        skillFour.text = "Varian fixeades himself on a tank and covers it with a sequence of several hits.";
    }

    /// <summary>
    /// Loads images and texts for level 7 in the description.
    /// </summary>
    public void LevelSeven()
    {
        name.text = "Tectus";
        description.text = "Tectus is a stone giant who lingers in a cave near a large street and lurks there for travelers. Rumors say he would petrify the travelers and drag him into his cave.";
        skillOneImage.sprite = Resources.Load("StoneFist", typeof(Sprite)) as Sprite;
        skillOne.text = "Tectus hits your tanks with his stone fists.";
        skillTwoImage.sprite = Resources.Load("StoneRange", typeof(Sprite)) as Sprite;
        skillTwo.text = "Tectus rips the ground and damages individual party members.";
        skillThreeImage.sprite = Resources.Load("Stein_1", typeof(Sprite)) as Sprite;
        skillThree.text = "Tectus petrifies a target for a short time. When it dies or is healed to full, it splinters and damages your whole party.";
        skillFourImage.sprite = Resources.Load("Stein_2", typeof(Sprite)) as Sprite;
        skillFour.text = "Tectus petrifies a target until it is healed to full. When it dies it splinters and damages your whole party.";
    }

    /// <summary>
    /// Loads images and texts for level 8 in the description.
    /// </summary>
    public void LevelEight()
    {
        name.text = "Brackenspore";
        description.text = "Brackenspore is a marsh monster, which descends from a gloomy moor and threatens a small village. Kill it to protect the village.";
        skillOneImage.sprite = Resources.Load("Ranken", typeof(Sprite)) as Sprite;
        skillOne.text = "Brackenspore is striking after your tanks with his tendrils.";
        skillTwoImage.sprite = Resources.Load("Sporen", typeof(Sprite)) as Sprite;
        skillTwo.text = "Brackenspore is surrounded by a cloud of poisonous spores that harm your whole party.";
        skillThreeImage.sprite = Resources.Load("Breath", typeof(Sprite)) as Sprite;
        skillThree.text = "Brackenspore regularly discharges his putrid breath over your party.";
        skillFourImage.sprite = Resources.Load("Pilz", typeof(Sprite)) as Sprite;
        skillFour.text = "Mushrooms spawn refularly from the ground which will heal your party when healed to full.";
    }

    /// <summary>
    /// Loads images and texts for level 9 in the description.
    /// </summary>
    public void LevelNine()
    {
        name.text = "Valeera";
        description.text = "Valeera is the leader of a great thieves guild. Kill her to smash this guild.";
        skillOneImage.sprite = Resources.Load("Dagger", typeof(Sprite)) as Sprite;
        skillOne.text = "Valeera hits your tanks with her dagger.";
        skillTwoImage.sprite = Resources.Load("Cleave", typeof(Sprite)) as Sprite;
        skillTwo.text = "Valeera takes a powerful punch which hits both tanks.";
        skillThreeImage.sprite = Resources.Load("Fixate", typeof(Sprite)) as Sprite;
        skillThree.text = "Valeera fixeades himself on a tank and covers it with a sequence of several hits.";
        skillFourImage.sprite = Resources.Load("Dot_Debuff", typeof(Sprite)) as Sprite;
        skillFour.text = "Valeera poisoned a party member.This debuff can be dispelled.";
        skillFiveImage.sprite = Resources.Load("Heal_Debuff", typeof(Sprite)) as Sprite;
        skillFive.text = "Valeera shakes a party member and prevents all healing he takes.";
    }

    /// <summary>
    /// Loads images and texts for level 10 in the description.
    /// </summary>
    public void LevelTen()
    {
        name.text = "Al'akir";
        description.text = "Al'akir is a powerful elemental lord. He was corrupted and now tried to bring the elements into an imbalance.";
        skillOneImage.sprite = Resources.Load("ElementalAttack", typeof(Sprite)) as Sprite;
        skillOne.text = "Al'akir uses the elements and deals damage to individual partymembers on a regular basis.";
        skillTwoImage.sprite = Resources.Load("AlakirPhasen", typeof(Sprite)) as Sprite;
        skillTwo.text = "Over the course of the fight Al'akir will zwitch between different elemental forms.";
        skillThreeImage.sprite = Resources.Load("Feuer_Debuff", typeof(Sprite)) as Sprite;
        skillThree.text = "Fireform: Al'akir burns partymembers, which will suffer damage on a regular basis.";
        skillFourImage.sprite = Resources.Load("Luft", typeof(Sprite)) as Sprite;
        skillFour.text = "Airform: Al'akir slowly reduces the healing obtained by partymembers.";
        skillFiveImage.sprite = Resources.Load("Blizzard", typeof(Sprite)) as Sprite;
        skillFive.text = "Waterform: Al'akir summons a blizzard that removes burns and causes damage";
    }

    /// <summary>
    /// Loads images and texts for level 11 in the description.
    /// </summary>
    public void LevelEleven()
    {
        name.text = "Ultraxion";
        description.text = "Ultraxion is a powerful dark dragon. He forces his will onto other dragons. Heal these dragons to help you fight.";
        skillOneImage.sprite = Resources.Load("Claw", typeof(Sprite)) as Sprite;
        skillOne.text = "Ultraxions can hurt both tanks simultaneously. From time to time he will also fixate one tank.";
        skillTwoImage.sprite = Resources.Load("ShadowAura", typeof(Sprite)) as Sprite;
        skillTwo.text = "Ultraxion is surrounded by a powerful aura, which deals damage regulary.";
        skillThreeImage.sprite = Resources.Load("red", typeof(Sprite)) as Sprite;
        skillThree.text = "The Red Dragon doubles your healing as soon as he is healed.";
        skillFourImage.sprite = Resources.Load("blue", typeof(Sprite)) as Sprite;
        skillFour.text = "The Blue Dragon regulary restores some mana as soon as he heals.";
        skillFiveImage.sprite = Resources.Load("green", typeof(Sprite)) as Sprite;
        skillFive.text = "The Green Dragon duplicates your healing and distributes it evenly to your party.";
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
        skillTwo.text = "DOTLEICHT";
        skillThreeImage.sprite = Resources.Load("DOTMITTEL", typeof(Sprite)) as Sprite;
        skillThree.text = "DOTMITTEL";
        skillFourImage.sprite = Resources.Load("DOTSCHWER", typeof(Sprite)) as Sprite;
        skillFour.text = "DOTSCHWER";
    }
}
