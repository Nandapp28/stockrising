using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollButtonScript : MonoBehaviour
{
    public Dice1Script dice1Script;
    public Dice2Script dice2Script;
    public bool isClicked = false;

    public void ButtonClicked()
    {
        isClicked = true;
        dice1Script.isRolling = true;
        dice2Script.isRolling = true;
    }
}
