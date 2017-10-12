using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for a Spell.
/// </summary>
public interface ISpell { 
    void StartGcd();
    void RemoveSpellFromButton();
}
