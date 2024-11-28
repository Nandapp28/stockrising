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

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI orderNumberText;

    public List<CardOwned> cardsOwned = new List<CardOwned>();

    void Awake()
    {
        Transform tmpNameTransform = transform.Find("Name");
        if (tmpNameTransform != null )
        {
            nameText = tmpNameTransform.GetComponent<TextMeshProUGUI>();
        }

        Transform tmpOrderNumTransform = transform.Find("Player Order Number");
        if (tmpOrderNumTransform != null )
        {
            orderNumberText = tmpOrderNumTransform.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        PrintOrderNumber(playerOrder);
    }

    void PrintOrderNumber(int iPlayerOrder)
    {
        orderNumberText.text = iPlayerOrder.ToString();
    }

    public class CardOwned
    {
        public string cardSectorColor { get; set; }
        public GameObject actionCardObj { get; set; }
    }

    public void AddCard(string sectorColor, GameObject cardObj)
    {
        // Buat objek kartu baru dan tambahkan ke daftar
        CardOwned newCard = new CardOwned { cardSectorColor = sectorColor, actionCardObj = cardObj };
        cardsOwned.Add(newCard);
    }

    // fungsi untuk hitung kartu dengan warna tertentu
    public int CountCardsByColor(string color)
    {
        // Menggunakan LINQ
        return cardsOwned.Count(card => card.cardSectorColor == color);

        // Menggunakan Loop Sederhana
        //int count = 0;
        //foreach (var card in cardsOwned)
        //{
        //    if (card.cardSectorColor == color)
        //    {
        //        count++;
        //    }
        //}
        //return count;
    }
}
