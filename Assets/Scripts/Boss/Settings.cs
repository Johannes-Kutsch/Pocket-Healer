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

    public float pulsingAOEswingTimer;
    public float pulsingAOEdmgRock;
    public int pulsingAOEnumberTargetsRock;
    public int pulsingAOErocksBetweenAoe;
    public float pulsingAOEdmgAoe;
    public int pulsingAOEticksAoe;

    public float applyKillDebuffTimer;
    public float applyKillDebuffTimerStart = 0f;

    public float applyMarkDebuffTimer;
    public float applyMarkDebuffTimerStart = 0f;

    public float fixateCleaveSwingTimer;
    public float fixateCleaveDmgCleave;
    public int fixateCleaveCleaveBetweenFixate;
    public int fixateCleaveHitsFixate;
    public float fixateCleaveDmgFixate;
    public float fixateCleaveMultiplierFixate;

    public float applyStoneDebuffTimer;
    public float applyStoneDebuffTimerStart = 0f;
    public float applyStoneDmg1;
    public float applyStoneDmg2;

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
        pulsingAOEswingTimer = 6f;
        pulsingAOEdmgRock = 40f;
        pulsingAOEnumberTargetsRock = 3;
        pulsingAOErocksBetweenAoe = 3;
        pulsingAOEdmgAoe = 10f;
        pulsingAOEticksAoe = 7;
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
        pulsingAOEswingTimer = 6f;
        pulsingAOEdmgRock = 35f;
        pulsingAOEnumberTargetsRock = 3;
        pulsingAOErocksBetweenAoe = 4;
        pulsingAOEdmgAoe = 10f;
        pulsingAOEticksAoe = 7;

        //kill debuff
        applyKillDebuffTimer = 13f;
        applyKillDebuffTimerStart = -2f;
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

        //MarkDebuff
        applyMarkDebuffTimer = 20f;
        applyMarkDebuffTimerStart = 17f;
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
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 2f;
        rangeAutoAttackDmg = 20f;
        rangeAutoAttackMultiplier = 0.1f;
        rangeAutoAttackSwingTimerStart = 1f;

        //FixateCleave
        fixateCleaveSwingTimer = 5f;
        fixateCleaveDmgCleave = 50f;
        fixateCleaveCleaveBetweenFixate = 2;
        fixateCleaveHitsFixate = 5;
        fixateCleaveDmgFixate = 38f;
        fixateCleaveMultiplierFixate = 1.2f;
    }

    /// <summary>
    /// Loads the level seven.
    /// </summary>
    private void LoadLevelSeven()
    {
        //Scene seven
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 25f;
        autoAttackChangeTargetTimer = 10f;
        autoAttackMultiplier = 0.1f;

        //RangeAutoAttack
        rangeAutoAttackSwingTimer = 2f;
        rangeAutoAttackDmg = 15f;
        rangeAutoAttackMultiplier = 0f;
        rangeAutoAttackSwingTimerStart = 1f;

        //StoneDebuff
        applyStoneDebuffTimer = 12f;
        applyStoneDebuffTimerStart = 9f;
        applyStoneDmg1 = 40f;
        applyStoneDmg2 = 50f;
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

        //PulsingAOE
        pulsingAOEswingTimer = 11f;
        pulsingAOEdmgRock = 0f;
        pulsingAOEnumberTargetsRock = 0;
        pulsingAOErocksBetweenAoe = 0;
        pulsingAOEdmgAoe = 15f;
        pulsingAOEticksAoe = 10;
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
        autoAttackMultiplier = 0.1f;

        //FixateCleave
        fixateCleaveSwingTimer = 5f;
        fixateCleaveDmgCleave = 50f;
        fixateCleaveCleaveBetweenFixate = 2;
        fixateCleaveHitsFixate = 5;
        fixateCleaveDmgFixate = 35f;
        fixateCleaveMultiplierFixate = 1.2f;
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
        autoAttackMultiplier = 1f;

        //FixateCleave
        fixateCleaveSwingTimer = 6f;
        fixateCleaveDmgCleave = 100f;
        fixateCleaveCleaveBetweenFixate = 3;
        fixateCleaveHitsFixate = 5;
        fixateCleaveDmgFixate = 50f;
        fixateCleaveMultiplierFixate = 1.2f;
    }


}
