using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static BiddingPhaseState;

public class PlayerScript : MonoBehaviour
{
    public int playerOrder = 0;
    public int playerOldOrder = 0;

    // jumlah kartu aksi
    public int actionCMerah = 0;
    public int actionCOranye = 0;
    public int actionCBiru = 0;
    public int actionCHijau = 0;

    // referensi UI
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI orderNumberText;
    private TextMeshProUGUI actionCMerahText;
    private TextMeshProUGUI actionCOranyeText;
    private TextMeshProUGUI actionCBiruText;
    private TextMeshProUGUI actionCHijauText;


    public List<ActionCardOwned> actionCardsOwned = new List<ActionCardOwned>();

    void Awake()
    {
        Transform tmpNameTransform = transform.Find("Player Name");
        if (tmpNameTransform != null )
        {
            nameText = tmpNameTransform.GetComponent<TextMeshProUGUI>();
        }

        Transform tmpOrderNumTransform = transform.Find("Player Order Number");
        if (tmpOrderNumTransform != null )
        {
            orderNumberText = tmpOrderNumTransform.GetComponent<TextMeshProUGUI>();
        }

        Transform tmpActionCardsTransform = transform.Find("Player Action Cards");
        if (tmpOrderNumTransform != null)
        {
            orderNumberText = tmpOrderNumTransform.GetComponent<TextMeshProUGUI>();
            actionCMerahText = tmpActionCardsTransform.transform.Find("Red Card").GetComponent<TextMeshProUGUI>();
            actionCOranyeText = tmpActionCardsTransform.transform.Find("Orange Card").GetComponent<TextMeshProUGUI>();
            actionCBiruText = tmpActionCardsTransform.transform.Find("Blue Card").GetComponent<TextMeshProUGUI>();
            actionCHijauText = tmpActionCardsTransform.transform.Find("Green Card").GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        PrintOrderNumber(playerOrder);
        CountActionCards();
    }

    void PrintOrderNumber(int iPlayerOrder)
    {
        orderNumberText.text = iPlayerOrder.ToString();
    }

    void CountActionCards()
    {
        actionCMerah = CountActionCardsByColor("Merah");
        actionCMerahText.text = "x" + actionCMerah.ToString();
        actionCOranye = CountActionCardsByColor("Oranye");
        actionCOranyeText.text = "x" + actionCOranye.ToString();
        actionCBiru = CountActionCardsByColor("Biru");
        actionCBiruText.text = "x" + actionCBiru.ToString();
        actionCHijau = CountActionCardsByColor("Hijau");
        actionCHijauText.text = "x" + actionCHijau.ToString();
    }

    [System.Serializable]
    public class ActionCardOwned
    {
        public string cardSectorColor { get; set; }
        public GameObject actionCardObj { get; set; }
    }

    public void AddActionCard(string sectorColor, GameObject cardObj)
    {
        // Buat objek kartu baru dan tambahkan ke daftar
        ActionCardOwned newCard = new ActionCardOwned { cardSectorColor = sectorColor, actionCardObj = cardObj };
        actionCardsOwned.Add(newCard);
    }

    // fungsi untuk hitung kartu dengan warna tertentu
    public int CountActionCardsByColor(string color)
    {
        // Menggunakan LINQ
        return actionCardsOwned.Count(card => card.cardSectorColor == color);
    }
}
