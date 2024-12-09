using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static BiddingPhaseState;

public class PlayerScript : MonoBehaviour
{
    public string playerName;
    public int playerOrder = 0;
    //public int playerOldOrder = 0;

    // referensi UI 
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI orderNumberText;

    // jumlah point 
    [Header("My Point")]
    public int investmentPoint = 15;

    // referensi UI point
    private TextMeshProUGUI investmentPointText;

    // jumlah kartu 
    [Header("My Cards")]
    public int actionCMerah = 0;
    public int actionCOranye = 0;
    public int actionCBiru = 0;
    public int actionCHijau = 0;

    // referensi UI kartu 
    private TextMeshProUGUI actionCMerahText;
    private TextMeshProUGUI actionCOranyeText;
    private TextMeshProUGUI actionCBiruText;
    private TextMeshProUGUI actionCHijauText;


    public List<ActionCardOwned> actionCardsOwned = new List<ActionCardOwned>();

    void Awake()
    {
        LoadReferences();
    }

    private void LoadReferences()
    {
        // load name
        Transform tmpNameTransform = transform.Find("Player Name");
        if (tmpNameTransform != null)
        {
            nameText = tmpNameTransform.GetComponent<TextMeshProUGUI>();
        }

        // load order number
        Transform tmpOrderNumTransform = transform.Find("Player Order Number");
        if (tmpOrderNumTransform != null)
        {
            orderNumberText = tmpOrderNumTransform.GetComponent<TextMeshProUGUI>();
        }

        // load investment points
        Transform tmpIPTransform = transform.Find("Player IP");
        if (tmpOrderNumTransform != null)
        {
            investmentPointText = tmpIPTransform.GetComponent<TextMeshProUGUI>();
        }

            // load total action cards
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
        SetInitialize();
        CountActionCards();
        //if (actionCardsOwned.Count != 0)
        //{
        //    Debug.Log("Ini adalah objek kartu aksi yang disimpan = " + actionCardsOwned[0].textureName);
        //}
    }

    void SetInitialize()
    {
        orderNumberText.text = playerOrder.ToString();
        investmentPointText.text = "x" + investmentPoint.ToString();
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
        public GameObject actionCardObj { get; set; } // sepertinya di disable tak masalah

        public string textureName { get; set; }
    }

    public void AddActionCard(string sectorColor, string textureName)
    {
        // Buat objek kartu baru dan tambahkan ke daftar
        //ActionCardOwned newCard = new ActionCardOwned { cardSectorColor = sectorColor, actionCardObj = cardObj };
        ActionCardOwned newCard = new ActionCardOwned { cardSectorColor = sectorColor, textureName = textureName  };
        actionCardsOwned.Add(newCard);
    }

    // fungsi untuk hitung kartu dengan warna tertentu
    public int CountActionCardsByColor(string color)
    {
        // Menggunakan LINQ
        return actionCardsOwned.Count(card => card.cardSectorColor == color);
    }
}
