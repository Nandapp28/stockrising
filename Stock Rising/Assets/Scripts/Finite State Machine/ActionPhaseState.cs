using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionPhaseState : SemesterBaseState
{
    int setInitIndex = 0;
    float timeEnterCD = 2.0f;
    string currentFx = "null"; int indexFx = 0;

    // main camera
    float moveSpeed = 2.0f;
    float rotateSpeed = 2.0f;
    bool isMoving = false;

    // referensi
    GameObject player;
    ActionCardManager actionCardManagerScript;
    Animator actionCardAnim;
    Renderer actionCardTakenRenderer;

    string savedActionCardTakenTexture;

    // insider trade fx needed
    int rumorCardIsTakedi = 0;

    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseName = "Fase Aksi";
        semester.phaseTitleParent.gameObject.SetActive(true);

        // referensi ke script ActionCardManager
        actionCardManagerScript = semester.actionCardManagerObj.GetComponent<ActionCardManager>();
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("currentFx = " + currentFx);
        Debug.Log(savedActionCardTakenTexture);
        if (setInitIndex == 0)
        {
            SetInitialize(semester);
        }
        switch (semester.playerState)
        {
            case GameState.Player1Turn:
                Debug.Log("Player 1's Turn");
                player = semester.players[semester.CheckPlayerOrder(1)]; // mencari player urutan pertama 
                PlayerScript playerScript = player.GetComponent<PlayerScript>();

                if (currentFx == "Insider Trade")
                {
                    InsiderTrade(semester, savedActionCardTakenTexture);
                } 

                // jika kartu disimpan ==========================================================================================
                if (semester.actionCardisSaved == true)
                {
                    SaveActionCard(playerScript); // simpan kartu ke data player sesuai warna
                    semester.actionCardisSaved = false; // kembalikan actionCardisSaved ke false
                    actionCardAnim = actionCardManagerScript.cardTakenObj.GetComponent<Animator>(); // jalankan animasi menyimpan kartu
                    if (actionCardAnim != null)
                    {
                        actionCardAnim.SetTrigger("TrSaveCard");
                    }
                    // cek currentFx ============================================================================================
                    if (currentFx == "Flashbuy")
                    {
                        // cek apakah sudah simpan kartu 2 kali?
                        if (indexFx != 1) { indexFx += 1; } else
                        {
                            currentFx = "null"; // set kembali ke null
                            Button activateButton = GameObject.Find("Button Aktifkan Kartu").GetComponent<Button>(); // referensi button
                            activateButton.enabled = true; // enable active button
                            semester.playerState = (GameState)1;
                        }
                    } else
                    {
                        semester.playerState = (GameState)1; // ganti state pemain
                    }
                }

                // jika kartu diaktifkan ========================================================================================
                if (semester.actionCardisActivated == true) // ini sifatnya sekali klik
                {
                    //if (savedActionCardTakenTexture != null)
                    //{

                    //}
                    semester.actionCardisActivated = false; // kembalikan actionCardisActivated ke false
                    actionCardAnim = actionCardManagerScript.cardTakenObj.GetComponent<Animator>(); // jalankan animasi menyimpan kartu
                    if (actionCardAnim != null)
                    {
                        savedActionCardTakenTexture = actionCardManagerScript.cardTakenObj.GetComponent<Renderer>().material.mainTexture.name;
                        actionCardAnim.SetTrigger("TrSaveCard");
                    }
                    //ActionCardDB(playerScript, semester); // akses Dictionary kartu aksi, dan jalankan fungsi yang sesuai
                    //semester.playerState = (GameState)2; // ganti state pemain
                }

                if (semester.actionCardisActivated == false && actionCardManagerScript.cardTakenIsDestroy == true)
                {
                    actionCardManagerScript.cardTakenIsDestroy = false;
                    ActionCardDB(playerScript, semester); // akses Dictionary kartu aksi, dan jalankan fungsi yang sesuai
                }

                break;
            case GameState.Player2Turn:
                Debug.Log("Player 2's Turn");
                break;
            case GameState.Player3Turn:
                Debug.Log("Player 3's Turn");
                break;
        }
    }

    void SetInitialize(SemesterStateManager semester)
    {
        if (timeEnterCD >= 0)
        {
            timeEnterCD -= Time.deltaTime;
        }
        else
        {
            semester.phaseTitleParent.gameObject.SetActive(false);

            // objek Action Phase
            isMoving = true;
            MoveCamera(semester.mainCamera, semester.cameraPost2);
            semester.transparantBgObj.SetActive(true);
            if (isMoving == false)
            {
                semester.actionCardsObj.SetActive(true);
                semester.actionCardManagerObj.SetActive(true);
                setInitIndex = 1;
            }
        }
    }

    void MoveCamera(Camera mainCam, GameObject camPost2)
    {
        if (isMoving == true && camPost2 != null)
        {
            // interpolasi posisi kamera
            mainCam.transform.position = Vector3.Lerp(
                mainCam.transform.position,
                camPost2.transform.position,
                moveSpeed * Time.deltaTime
                );

            // interpolasi rotasi kamera
            mainCam.transform.rotation = Quaternion.Lerp(
                mainCam.transform.rotation,
                camPost2.transform.rotation,
                rotateSpeed * Time.deltaTime
                );

            // periksa jika posisi dan rotasi sudah mendekati target
            if (Vector3.Distance(mainCam.transform.position, camPost2.transform.position) < 0.1f &&
                Quaternion.Angle(mainCam.transform.rotation, camPost2.transform.rotation) < 0.5f)
            {
                isMoving = false;
            }
        }

    }

    private void SaveActionCard(PlayerScript playerScript)
    {
        // ambil obj renderer
        Renderer actionCardObjRenderer = actionCardManagerScript.cardTakenObj.GetComponent<Renderer>();
        // simpan texture name melalui komponen renderer obj
        string textureName = actionCardObjRenderer.material.mainTexture.name;
        if (textureName.StartsWith("uv_M"))
        {
            playerScript.AddActionCard("Merah", actionCardManagerScript.cardTakenObj);
        }
        else if (textureName.StartsWith("uv_O"))
        {
            playerScript.AddActionCard("Oranye", actionCardManagerScript.cardTakenObj);
        }
        else if (textureName.StartsWith("uv_B"))
        {
            playerScript.AddActionCard("Biru", actionCardManagerScript.cardTakenObj);
        }
        else if (textureName.StartsWith("uv_H"))
        {
            playerScript.AddActionCard("Hijau", actionCardManagerScript.cardTakenObj);
        }
    }

    private void ActionCardDB(PlayerScript playerScript, SemesterStateManager semester)
    {
        // ambil obj renderer
        //Renderer actionCardObjRenderer = actionCardManagerScript.cardTakenObj.GetComponent<Renderer>();
        // simpan texture name melalui komponen renderer obj
        //string textureName = actionCardObjRenderer.material.mainTexture.name;
        string textureName = savedActionCardTakenTexture;

        // membuat dynamic dictionary
        Dictionary<Func<string, bool>, Action> dynamicActions = new Dictionary<Func<string, bool>, Action>
        {
            { name => name.EndsWith("FB"), () => {
                FlashbuyFx();
            } },
            { name => name.EndsWith("IT"), () => {
                InsiderTradeFx();
            } },
            { name => name.EndsWith("SS"), () => {
                Debug.Log("Stock Split Card is Activated");
            } },
            { name => name.EndsWith("TF"), () => {
                Debug.Log("Trade Free Card is Activated"); 
            } },
            { name => name.EndsWith("TO"), () => {
                Debug.Log("Tender Offer Card is Activated");
            } },
        };
        
        // Cari pola yang cocok
        foreach (var entry in dynamicActions)
        {
            if (entry.Key(textureName)) // Panggil fungsi kondisi
            {
                entry.Value(); // Jalankan aksi
                return;
            }
        }
    }

    private void FlashbuyFx()
    {
        //Debug.Log("Pilih 2 kartu untuk disimpan"); // show title
        Button activateButton = GameObject.Find("Button Aktifkan Kartu").GetComponent<Button>(); // referensi button
        activateButton.enabled = false; // disable activate button
        currentFx = "Flashbuy";
        actionCardManagerScript.cardTaken = false;
    }

    // INSIDER TRADE EFFECT SECTION ==============================================================================================

    private void InsiderTradeFx()
    {
        // ambil obj renderer
        //savedActionCardTakenTexture = actionCardManagerScript.cardTakenObj.GetComponent<Renderer>().material.mainTexture.name;
        //actionCardTakenRenderer = savedActionCardTaken.GetComponent<Renderer>();
        //actionCardTakenRenderer = actionCardManagerScript.cardTakenObj.GetComponent<Renderer>();
        currentFx = "Insider Trade";
    }

    private void InsiderTrade(SemesterStateManager semester, string textureName)
    {
        //// simpan texture name melalui komponen renderer obj
        //textureName = actionCardTakenRenderer.material.mainTexture.name;

        // camera movement ke board
        actionCardManagerScript.cardTaken = false;
        isMoving = true;
        MoveCamera(semester.mainCamera, semester.cameraPost1);

        // deactivate bacgkground dan hide kartu aksi
        semester.transparantBgObj.SetActive(false);
        semester.actionCardsObj.SetActive(false);

        // munculkan tombol "selesai"
        semester.rumorCardButton.SetActive(true);

        if (textureName.StartsWith("uv_M"))
        {
            ITFindBoard("Red Board", semester);
        }
        else if (textureName.StartsWith("uv_O"))
        {
            ITFindBoard("Orange Board", semester);
        }
        else if (textureName.StartsWith("uv_B"))
        {
            ITFindBoard("Blue Board", semester);
        }
        else if (textureName.StartsWith("uv_H"))
        {
            ITFindBoard("Green Board", semester);
        }
        else
        {
            Debug.Log("tidak terdeteksi uv_apa");
        }
    }

    private void ITFindBoard(string boardName, SemesterStateManager semester)
    {
        GameObject rumorCardi = null;
        GameObject board = GameObject.Find(boardName);
        Transform rumorCards = board.transform.Find("Rumor Cards");
        int childCount = rumorCards.transform.childCount;
        if (childCount == 4)
        {
            GameObject rumorCard = rumorCards.transform.Find("Rumor Card 1").transform.Find("Card").gameObject;
            if (rumorCardIsTakedi == 0)
            {
                rumorCard.GetComponent<RumorCardScript>().isTaked = true; // jalankan animasi untuk melihat kartu rumor
                rumorCardIsTakedi = 1;
            }
            semester.rumorCardButton.transform.Find("Button Selesai Kartu").GetComponent<RumorCardButtonScript>().rumorCardTaken = rumorCard; // beri referensi di script button "selesai"
            rumorCardi = rumorCard;
        }

        // tunggu sampai di klik user dan kartu kembali ke papan
        if (rumorCardi != null)
        {
            // jika user sudah klik button "selesai" dan isMoving menjadi false
            if (rumorCardi.GetComponent<RumorCardScript>().isTaked == false)
            {
                Debug.Log("Insider Trade Selesai");
                semester.rumorCardButton.SetActive(false);
                // kamera kembali
                isMoving = true;
                MoveCamera(semester.mainCamera, semester.cameraPost2);
                // deactivate bacgkground dan hide kartu aksi
                semester.transparantBgObj.SetActive(true);
                semester.actionCardsObj.SetActive(true);
            }
        }
    }

    private void StockSplitFx()
    {

    }

    private void TradeFeeFx()
    {

    }

    private void TenderOfferFx()
    {

    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
