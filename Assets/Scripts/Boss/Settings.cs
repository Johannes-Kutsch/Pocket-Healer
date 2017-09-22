using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains some Settings for Boss Damage Values and Timers
/// </summary>
public class Settings{
    public int level;

    public float autoAttackSwingTimer;
    public float autoAttackDmg;
    public float autoAttackChangeTargetTimer;
    public float autoAttackMultiplier;

    public float rangeAutoAttackSwingTimer;
    public float rangeAutoAttackDmg;
    public float rangeAutoAttackMultiplier;
    public float rangeAutoAttackSwingTimerStart = 0f;

    public float throwRockSwingTimer;
    public float throwRockAttackDmg;
    public int throwRockNumberTargets;

    public float timedAOESwingTimer;
    public float timedAOEAttackDmg;

    public float PulsingAOEswingTimer;
    public float PulsingAOEdmgRock;
    public int PulsingAOEnumberTargetsRock;
    public int PulsingAOErocksBetweenAoe;
    public float PulsingAOEdmgAoe;
    public int PulsingAOEticksAoe;

    /// <summary>
    /// Initializes a new instance of the <see cref="Settings"/> class.
    /// </summary>
    /// <param name="levelIndex">The levelIndex</param>
    public Settings(int levelIndex)
    {
        this.level = levelIndex;

        switch (levelIndex)
        {
            case 0:
                LoadLevelZero();
                Debug.Log("WARNING LEVEL INDES IS ZERO (DEFAULT/TUTORIAL)");
                break;
            case 1:
                LoadLevelOne();
                break;
            case 2:
                LoadLevelTwo();
                break;
            case 3:
                LoadLevelThree();
                break;
            case 4:
                LoadLevelFour();
                break;
            case 5:
                LoadLevelFive();
                break;
            case 6:
                LoadLevelSix();
                break;
            case 7:
                LoadLevelSeven();
                break;
            case 8:
                LoadLevelEight();
                break;
            case 9:
                LoadLevelNine();
                break;
            case 10:
                LoadLevelTen();
                break;
            case 11:
                LoadLevelEleven();
                break;
        }
    }

    /// <summary>
    /// Loads the level zero (Tutorail.
    /// </summary>
    private void LoadLevelZero() {
        //Tutorial
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 15f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 4f;
        rangeAutoAttackDmg = 15f;
        rangeAutoAttackMultiplier = 0.1f;
    }

    /// <summary>
    /// Loads the level one.
    /// </summary>
    private void LoadLevelOne()
    {
        //Scene one
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 15f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 3f;
        rangeAutoAttackDmg = 10f;
        rangeAutoAttackMultiplier = 0f;

        //ThrowRock
        throwRockSwingTimer = 8;
        throwRockAttackDmg = 40;
        throwRockNumberTargets = 2;
    }

    /// <summary>
    /// Loads the level two.
    /// </summary>
    private void LoadLevelTwo()
    {
        //Scene two
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 3f;
        rangeAutoAttackDmg = 20f;
        rangeAutoAttackMultiplier = 0.1f;

        //TimedAOE
        timedAOESwingTimer = 10f;
        timedAOEAttackDmg = 45f;
    }

    /// <summary>
    /// Loads the level three.
    /// </summary>
    private void LoadLevelThree()
    {
        //Scene three
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 2.5f;
        rangeAutoAttackDmg = 10f;
        rangeAutoAttackMultiplier = 0.1f;

        //PulsingAOE
        PulsingAOEswingTimer = 5f;
        PulsingAOEdmgRock = 35f;
        PulsingAOEnumberTargetsRock = 2;
        PulsingAOErocksBetweenAoe = 2;
        PulsingAOEdmgAoe = 10f;
        PulsingAOEticksAoe = 7;
    }

    /// <summary>
    /// Loads the level four.
    /// </summary>
    private void LoadLevelFour()
    {
        //Scene four
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 3.5f;
        rangeAutoAttackDmg = 15f;
        rangeAutoAttackMultiplier = 0.1f;

        //PulsingAOE
        PulsingAOEswingTimer = 6f;
        PulsingAOEdmgRock = 35f;
        PulsingAOEnumberTargetsRock = 3;
        PulsingAOErocksBetweenAoe = 0;
        PulsingAOEdmgAoe = 10f;
        PulsingAOEticksAoe = 0;
    }

    /// <summary>
    /// Loads the level five.
    /// </summary>
    private void LoadLevelFive()
    {
        //Scene five
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 10f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 3f;
        rangeAutoAttackDmg = 20f;
        rangeAutoAttackMultiplier = 0.1f;
    }

    /// <summary>
    /// Loads the level six.
    /// </summary>
    private void LoadLevelSix()
    {
        //Scene six
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        //autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 2f;
        rangeAutoAttackDmg = 20f;
        rangeAutoAttackMultiplier = 0.1f;
        rangeAutoAttackSwingTimerStart = 1f;
    }

    /// <summary>
    /// Loads the level seven.
    /// </summary>
    private void LoadLevelSeven()
    {
        //Scene seven
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 10f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 2f;
        rangeAutoAttackDmg = 10f;
        rangeAutoAttackMultiplier = 0f;
        rangeAutoAttackSwingTimerStart = 1f;
    }

    /// <summary>
    /// Loads the level eight.
    /// </summary>
    private void LoadLevelEight()
    {
        //Scene eight
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 10f;
        autoAttackMultiplier = 0.1f;
    }

    /// <summary>
    /// Loads the level nine.
    /// </summary>
    private void LoadLevelNine()
    {
        //Scene nine
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        //autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    /// <summary>
    /// Loads the level ten.
    /// </summary>
    private void LoadLevelTen()
    {
        //Scene ten
        //AutoAttack
        autoAttackSwingTimer = 2.5f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 20f;
        autoAttackMultiplier = 0.1f;
    }

    /// <summary>
    /// Loads the level eleven.
    /// </summary>
    private void LoadLevelEleven()
    {
        //Scene eleven
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        //autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 1f;
    }


}
