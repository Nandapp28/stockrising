using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSalesButtonScript : MonoBehaviour
{
    SemesterStateManager state;
    public SectorSalesScript sectorSalesScript;

    void Awake()
    {
        state = GameObject.FindGameObjectWithTag("State Manager").GetComponent<SemesterStateManager>();
        sectorSalesScript = GetComponentInParent<SectorSalesScript>();
    }

    public void SkipButtonClicked()
    {
        state.isSalesPhaseSkip = true;
    }

    public void PlusButtonClicked()
    {
        if (sectorSalesScript.soldCardsTotal < sectorSalesScript.ownCardsTotal)
        {
            sectorSalesScript.soldCardsTotal += 1;
        }
    }
    public void MinusButtonClicked()
    {
        if (sectorSalesScript.soldCardsTotal != 0)
        {
            sectorSalesScript.soldCardsTotal -= 1;
        }
    }
}
