using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpCardButton : MonoBehaviour
{
    public HelpCardManager helpCardManager;

    void Start()
    {
        helpCardManager = GetComponent<HelpCardManager>();
    }

    void Update()
    {
        
    }

    public void yesButtonClicked()
    {
        helpCardManager.isYesNoButton = "Ya";
    }

    public void noButtonClicked()
    {
        helpCardManager.isYesNoButton = "Tidak";
    }
}
