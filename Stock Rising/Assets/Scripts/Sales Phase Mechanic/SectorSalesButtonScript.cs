using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlayerScript;

public class SectorSalesButtonScript : MonoBehaviour
{
    SemesterStateManager state;
    public SectorSalesScript sectorSalesScript;

    public SectorSalesScript consumerSSC;
    public SectorSalesScript infrastructureSSC;
    public SectorSalesScript financialSSC;
    public SectorSalesScript miningSSC;

    void Awake()
    {
        state = GameObject.FindGameObjectWithTag("State Manager").GetComponent<SemesterStateManager>();
        sectorSalesScript = GetComponentInParent<SectorSalesScript>();
    }

    public void SkipButtonClicked()
    {
        state.isSalesPhaseSkip = true;
    }

    public void SellButtonClicked()
    {
        if (consumerSSC.profit + infrastructureSSC.profit + financialSSC.profit + miningSSC.profit != 0)
        {
            // sektor konsumer 
            SellAction(consumerSSC);
            // sektor infrastruktur
            SellAction(infrastructureSSC);
            // sektor keuangan
            SellAction(financialSSC);
            // sektor tambang
            SellAction(miningSSC);

            state.isSalesPhaseSell = true;
        }
    }

    private void SellAction(SectorSalesScript ssc)
    {
        // berikan investment point ke player
        int profit = (int)ssc.profit;
        ssc.playerScript.investmentPoint += profit;
        // hapus kartu yang sudah dijual
        int numberOfCardsSold = ssc.soldCardsTotal;
        string sectorColor = ssc.warnaSektor;
        var cards = ssc.playerScript.actionCardsOwned.Where(card => card.cardSectorColor == sectorColor).Take(numberOfCardsSold).ToList();
        foreach (var card in cards)
        {
            ssc.playerScript.actionCardsOwned.Remove(card);
        }
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
