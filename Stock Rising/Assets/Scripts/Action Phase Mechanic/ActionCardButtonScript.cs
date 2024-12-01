using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardButtonScript : MonoBehaviour
{
    SemesterStateManager state;
    ActionCardManager actionCardManagerScript;

    void Awake()
    {
        state = GameObject.FindGameObjectWithTag("State Manager").GetComponent<SemesterStateManager>();
        actionCardManagerScript = GameObject.FindGameObjectWithTag("Action Card Manager").GetComponent<ActionCardManager>();
    }

    public void SaveButtonClicked()
    {
        actionCardManagerScript.cardTaken = false;
        // jika berada di fase aksi
        if (state.phaseName == "Fase Aksi")
        {
            state.actionCardisSaved = true;
        }
    }

    public void ActivateButtonClicked()
    {
        actionCardManagerScript.cardTaken = false;
        // jika berada di fase aksi
        if (state.phaseName == "Fase Aksi")
        {
            state.actionCardisActivated = true;
        }
    }
}
