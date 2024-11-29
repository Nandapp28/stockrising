using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonScript : MonoBehaviour
{
    public SemesterStateManager state;

    public void ButtonClicked()
    {
        // jika berada di fase aksi
        if (state.phaseName == "Fase Aksi")
        {
            state.actionCardisSaved = true;
        }
    }
}
