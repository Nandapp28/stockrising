using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SectorSalesScript : MonoBehaviour
{

    [Header("Control")]
    public int ownCardsTotal;
    public float stockSellValue;
    public int soldCardsTotal = 0;
    public float profit;
    public string warnaSektor;

    [Header("References")]
    public TextMeshProUGUI ownCardsTotalText;
    public TextMeshProUGUI stockSellValueText;
    public TextMeshProUGUI soldCardsTotalText;
    public TextMeshProUGUI profitText;
    public TextMeshProUGUI sectorName;
    // ======================================
    public SemesterStateManager state;
    public PlayerScript playerScript;
    public BoardScript boardScript;

    private void Awake()
    {
        state = GameObject.FindGameObjectWithTag("State Manager").GetComponent<SemesterStateManager>();

        ownCardsTotalText = transform.Find("Number of Own Stocks").transform.Find("Shares").gameObject.GetComponent<TextMeshProUGUI>();
        stockSellValueText = transform.Find("Stock Sell Value").transform.Find("Shares").gameObject.GetComponent<TextMeshProUGUI>();
        soldCardsTotalText = transform.Find("Number of Stocks Sold").transform.Find("Total").gameObject.GetComponent<TextMeshProUGUI>();
        profitText = transform.Find("Profit").transform.Find("Value").gameObject.GetComponent<TextMeshProUGUI>();
        sectorName = transform.Find("Sector Name").gameObject.GetComponent<TextMeshProUGUI>();

        sectorName = transform.Find("Sector Name").GetComponent<TextMeshProUGUI>();
        if (sectorName.text == "Sektor Konsumer")
        {
            boardScript = GameObject.FindGameObjectWithTag("Red Board").GetComponent<BoardScript>();
            warnaSektor = "Merah";
        }
        else if (sectorName.text == "Sektor Infrastruktur")
        {
            boardScript = GameObject.FindGameObjectWithTag("Orange Board").GetComponent<BoardScript>();
            warnaSektor = "Oranye";
        }
        else if (sectorName.text == "Sektor Keuangan")
        {
            boardScript = GameObject.FindGameObjectWithTag("Blue Board").GetComponent<BoardScript>();
            warnaSektor = "Biru";
        }
        else if (sectorName.text == "Sektor Tambang")
        {
            boardScript = GameObject.FindGameObjectWithTag("Green Board").GetComponent<BoardScript>();
            warnaSektor = "Hijau";
        }
    }

    private void OnEnable()
    {
        if (state.phaseName == "Fase Penjualan")
        {
            // referensikan playerScript saat ini, cek playerState
            if (state.playerState == (GameState)0)
            {
                playerScript = state.CheckPlayerOrder(1).GetComponent<PlayerScript>();
            }
            else if (state.playerState == (GameState)1)
            {
                playerScript = state.CheckPlayerOrder(2).GetComponent<PlayerScript>();
            }
            else if (state.playerState == (GameState)2)
            {
                playerScript = state.CheckPlayerOrder(3).GetComponent<PlayerScript>();
            }
            else
            {
                playerScript = null;
            }
        }
        soldCardsTotal = 0;
    }

    private void OnDisable()
    {
        playerScript = null;
    }

    private void Update()
    {
        // tampilkan jumlah kartu saham sektor tertentu yang dimiliki player
        if (playerScript != null)
        {
            ownCardsTotal = playerScript.CountActionCardsByColor(warnaSektor);
            ownCardsTotalText.text = ownCardsTotal.ToString();
        }

        // tampilkan harga saham sesuai dengan sektor tertentu
        if (boardScript != null)
        {
            stockSellValue = boardScript.currentStockValue;
            stockSellValueText.text = stockSellValue.ToString();
        }

        // tampilkan jumlah kartu yang akan dijual
        soldCardsTotalText.text = soldCardsTotal.ToString();

        // tampilkan profit yang didapat
        profit = stockSellValue * soldCardsTotal;
        profitText.text = profit.ToString();
    }

}
