using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Settings{
    public int level;

    public float autoAttackSwingTimer;
    public float autoAttackDmg;
    public float autoAttackChangeTargetTimer;
    public float autoAttackMultiplier;

    public Settings(int level)
    {
        this.level = level;

        switch (level)
        {
            case 0:
                LoadLevelZero();
                Debug.Log("WARNING LEVEL INDES IS ZERO");
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

    private void LoadLevelZero() {
        //Tutorial
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 15f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelOne() {
        //Scene one
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 15f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelTwo() {
        //Scene two
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelThree()
    {
        //Scene three
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelFour()
    {
        //Scene four
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelFive()
    {
        //Scene five
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 10f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelSix()
    {
        //Scene six
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        //autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelSeven()
    {
        //Scene seven
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 10f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelEight()
    {
        //Scene eight
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 10f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelNine()
    {
        //Scene nine
        //AutoAttack
        autoAttackSwingTimer = 2f;
        autoAttackDmg = 20f;
        //autoAttackChangeTargetTimer = 15f;
        autoAttackMultiplier = 0.1f;
    }

    private void LoadLevelTen()
    {
        //Scene ten
        //AutoAttack
        autoAttackSwingTimer = 2.5f;
        autoAttackDmg = 20f;
        autoAttackChangeTargetTimer = 20f;
        autoAttackMultiplier = 0.1f;
    }

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
