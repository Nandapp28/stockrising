using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpCardManager : MonoBehaviour
{
    public SemesterStateManager state;
    public GameObject buttonHelpCard;

    public GameObject activateHelpCard;
    public GameObject buyHelpCard;
    public GameObject actBuyButton;
    public GameObject yesNoButton;

    public string isYesNoButton;

    // fase aksi
    List<bool> isPlayerSckipUsingHelpC = new List<bool>();


    void Start()
    {
        state = GameObject.FindGameObjectWithTag("State Manager").GetComponent<SemesterStateManager>();
        buttonHelpCard = gameObject;
        // referensi ke objek
        activateHelpCard = buttonHelpCard.transform.Find("Activate Help Card").gameObject;
        buyHelpCard = buttonHelpCard.transform.Find("Buy Help Card").gameObject;
        actBuyButton = buttonHelpCard.transform.Find("Button Aktifkan dan Beli").gameObject;
        yesNoButton = buttonHelpCard.transform.Find("Button Ya Tidak").gameObject;
    }

    void OnEnable()
    {
        List<bool> isPlayerSckipUsingHelpC = new List<bool>();
    }

    void Update()
    {
        bool coreActionPhaseIsDone = state.coreActionPhaseIsDone;
        bool helpCardIsDone = state.helpCardIsDone;

        if (state.phaseName == "Fase Aksi")
        {
            if (coreActionPhaseIsDone == true && helpCardIsDone == false)
            {
                activateHelpCard.SetActive(true);
                yesNoButton.SetActive(true);

                if (isYesNoButton == "Ya")
                {

                } else if (isYesNoButton == "Tidak")
                {
                    isPlayerSckipUsingHelpC.Add(false);
                    isYesNoButton = "";
                    state.SwitchPlayerState();
                }
            }

            if (isPlayerSckipUsingHelpC.Count == state.players.Length)
            {
                activateHelpCard.SetActive(false);
                yesNoButton.SetActive(false);
                this.gameObject.SetActive(false);
                state.helpCardIsDone = true;
            }
        }
    }

    public void yesButtonClicked()
    {
        isYesNoButton = "Ya";
    }

    public void noButtonClicked()
    {
        isYesNoButton = "Tidak";
    }
}


