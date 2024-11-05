using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BiddingPhaseState;

public class BPPlayersStopStateDebug : MonoBehaviour
{
    public List<float> originalOrderNumbers;
    public List<float> checkedOrderNumbers;
    public List<float> sortedOrderNumbers;

    public List<float> checkedOrderNumbers2;

    //void Update()
    //{
    //    checkedOrderNumbers2 = originalOrderNumbers;
    //    for (int i = 1; i < checkedOrderNumbers2.Count; i++)
    //    {
    //        if (checkedOrderNumbers2[i] == (float)Mathf.CeilToInt(checkedOrderNumbers2[i - 1]))
    //        {
    //            checkedOrderNumbers2[i] -= 0.5f;
    //            if (checkedOrderNumbers2[i] == checkedOrderNumbers2[i - 1])
    //            {
    //                checkedOrderNumbers2[i] -= 0.5f;
    //            }
    //        }
    //    }
    //}
}
