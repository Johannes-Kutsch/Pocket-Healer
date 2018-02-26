using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the calls to update the shader of every TalenteButton.
/// </summary>
public class TalentManager : MonoBehaviour {
    TalenteButton[] talenteButtons;

    /// <summary>
    /// Initialize the talenteButtons array.
    /// </summary>
    private void Start()
    {
        talenteButtons = GetComponentsInChildren<TalenteButton>();
    }

    /// <summary>
    /// Calls the UpdateShader() Method in every child
    /// </summary>
    public void UpdateShaders()
    {
        foreach(TalenteButton button in talenteButtons)
        {
            if(button != null) //if not all talents are unlocked some scripts are destroyed after initialising the questionmark picture
            {
                button.UpdateShader();
            }
        }
    }
}
