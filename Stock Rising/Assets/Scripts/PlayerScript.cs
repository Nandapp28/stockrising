using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerOrder = 0;
    public int playerOldOrder = 0;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI orderNumberText;

    void Awake()
    {
        Transform tmpNameTransform = transform.Find("Name");
        if (tmpNameTransform != null )
        {
            nameText = tmpNameTransform.GetComponent<TextMeshProUGUI>();
        }

        Transform tmpOrderNumTransform = transform.Find("Order Number");
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
}
