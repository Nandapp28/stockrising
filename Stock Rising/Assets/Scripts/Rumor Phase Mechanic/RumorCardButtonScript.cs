using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumorCardButtonScript : MonoBehaviour
{
    public GameObject rumorCardTaken;

    void Start()
    {
        
    }

    public void SelesaiButtonClicked()
    {
        rumorCardTaken.GetComponent<RumorCardScript>().isTaked = false;
    }
}
