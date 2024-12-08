using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerScript;

public class BoardScript : MonoBehaviour
{
    // referensi objek objek untuk Stock Indicators
    public List<Coordinates> stockCoordinates = new List<Coordinates>();
    public GameObject stockMarkerTokenObj; // marker token untuk saham
    public GameObject stockIndicators; // penampung empty object Stock Indicators 
    public int currentStockCoordinate = 0;
    public float currentStockValue;
    public int stockMultiplied = 1;

    // referensi objek objek untuk CP Indicators
    public List<Coordinates> cpCoordinates = new List<Coordinates>();
    public GameObject cpMarkerTokenObj; // marker token untuk kinerja perusahaan (CP)
    public GameObject cpIndicators; // penampung empty object CP Indicators 
    public int currentCPCoordinate = 0;
    public float currentCPValue;

    // stock values data setiap board
    public float[] redBoardStockValues = { 1, 2, 3, 5, 6, 7, 8 };
    public float[] orangeBoardStockValues = { 1, 3, 4, 5, 6, 7, 9 };
    public float[] blueBoardStockValues = { 1, 2, 4, 5, 6, 8, 9 };
    public float[] greenBoardStockValues = { 2, 4, 5, 7, 9 };

    // CP values data setiap board
    public float[] boardCPValues = { 0, 1, 1, 1, 2, 3, 3 };

    [System.Serializable]
    public class Coordinates
    {
        public int index { get; set; }
        public GameObject coordinateObj { get; set; }
        public float value { get; set; }
    }

    void Awake()
    {
        SetReferencesForStockObj();
        SetReferencesForCPObj();
    }

    void SetReferencesForStockObj() // fungsi untuk referensi ke indicator saham dan objek token saham
    {
        stockIndicators = transform.Find("Stock Indicators").gameObject;
        stockMarkerTokenObj = stockIndicators.transform.Find("Marker Token").gameObject;
        for (int i = -3; i <= 3; i++)
        {
            // cari objek coordinate
            if (stockIndicators.transform.Find(i.ToString()) != null)
            {
                // masukan objek coordinate
                GameObject findCoordinate = stockIndicators.transform.Find(i.ToString()).gameObject;
                // masukan value coordinate
                float coordinateValue = 0;
                coordinateValue = AssignBoardValues(i, coordinateValue);

                // buat sebuah list untuk nampung objek coordinate
                Coordinates newCoordinate = new Coordinates { index = i, coordinateObj = findCoordinate, value = coordinateValue };
                // masukan coordinate ke dalam list stockCoordinates
                stockCoordinates.Add(newCoordinate);
            }
        }
    }

    private float AssignBoardValues(int i, float coordinateValue)
    {
        if (transform.name == "Red Board")
        {
            coordinateValue = redBoardStockValues[i + 3];
        } else if (transform.name == "Orange Board")
        {
            coordinateValue = orangeBoardStockValues[i + 3];
        } else if (transform.name == "Blue Board")
        {
            coordinateValue = blueBoardStockValues[i + 3];
        } else if (transform.name == "Green Board")
        {
            coordinateValue = greenBoardStockValues[i + 2];
        }

        return coordinateValue;
    }

    void SetReferencesForCPObj()  // fungsi untuk referensi ke indicator CP dan objek token CP
    {
        cpIndicators = transform.Find("CP Indicators").gameObject;
        cpMarkerTokenObj = cpIndicators.transform.Find("Marker Token").gameObject;
        for (int i = -3; i <= 3; i++)
        {
            // cari objek coordinate
            if (cpIndicators.transform.Find(i.ToString()) != null)
            {
                GameObject findCoordinate = cpIndicators.transform.Find(i.ToString()).gameObject;
                float coordinateValue = boardCPValues[i+3];
                // buat sebuah list untuk nampung objek coordinate
                Coordinates newCoordinate = new Coordinates { index = i, coordinateObj = findCoordinate, value = coordinateValue };
                // masukan coordinate ke dalam list stockCoordinates
                cpCoordinates.Add(newCoordinate);
            }
        }
    }

    void Update()
    {
        // set Marker Token punya saham 
        SetStockMarkerTokenPost();

        // set Marker Token punya company performance
        SetCPMarkerToken();
    }

    void SetStockMarkerTokenPost() // set Marker Token punya saham
    {
        // cek objek marker token agar sesuai dengan currentStockCoordinate 
        foreach (Coordinates coordinate in stockCoordinates)
        {
            if (currentStockCoordinate == coordinate.index)
            {
                GameObject coordinateObj = coordinate.coordinateObj;
                stockMarkerTokenObj.transform.position = coordinateObj.transform.position;
                currentStockValue = coordinate.value;
            }
        }

        if (currentStockCoordinate < stockCoordinates[0].index) // jika stock crash ke bawah
        {
            currentStockCoordinate = 0;
        }
        else if (currentStockCoordinate > stockCoordinates[stockCoordinates.Count - 1].index) // jika stock crash ke atas, stockMultiplied naik 1
        {
            stockMultiplied += 1;
            currentStockCoordinate = 0;
        }
    }

    void SetCPMarkerToken() // set Marker Token punya company performance
    {
        foreach (Coordinates coordinate in cpCoordinates)
        {
            if (currentCPCoordinate == coordinate.index)
            {
                GameObject coordinateObj = coordinate.coordinateObj;
                cpMarkerTokenObj.transform.position = coordinateObj.transform.position;
                currentCPValue = coordinate.value;
            }
        }

        if (currentCPCoordinate < cpCoordinates[0].index) // jika CP crash ke bawah, Stock turun 1
        {
            currentStockCoordinate += -1;
            currentCPCoordinate = 0;
        } else if (currentCPCoordinate > cpCoordinates[cpCoordinates.Count - 1].index) // jika CP crash ke atas, Stock naik 1
        {
            currentStockCoordinate += 1;
            currentCPCoordinate = 0;
        }
    }
}
