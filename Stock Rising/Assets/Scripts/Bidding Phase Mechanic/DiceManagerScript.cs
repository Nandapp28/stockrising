using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceManagerScript : MonoBehaviour
{
    public Dice1Script dice1;
    public Dice2Script dice2;
    public RollButtonScript rollButtonScript;

    public int diceResult;
    public TextMeshProUGUI dice1IndexTMP;
    public TextMeshProUGUI dice2IndexTMP;

    private void Update()
    {
        //dice1IndexTMP.text = dice1.indexResult.ToString();
        //dice2IndexTMP.text = dice2.indexResult.ToString();
        //Debug.Log("Hasil dadu : " + CountDiceResult());
    }

    public int CountDiceResult()
    {
        if (rollButtonScript.isClicked == true)
        {
            if (dice1.CheckObjectHasStopped() == true && dice2.CheckObjectHasStopped() == true)
            {
                //diceResult = dice1.indexResult + dice2.indexResult;
                StartCoroutine(DelayAndCalculate());
                return diceResult;
            }
        }
        diceResult = 0;
        return diceResult;
    }

    IEnumerator DelayAndCalculate()
    {
        yield return new WaitForSeconds(2.5f);
        diceResult = dice1.indexResult + dice2.indexResult;
    }
}
