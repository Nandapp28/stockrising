using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseTitleScript : MonoBehaviour
{
    public Sprite[] phaseTitleSprites;
    public SemesterStateManager semesterStateManager;
    public Image phaseTitleImg;

    void OnEnable()
    {
        int currentPhase = semesterStateManager.phaseCount;
        phaseTitleImg.sprite = phaseTitleSprites[currentPhase - 1];
    }
}
