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
    
    bool flashbuyIsDone = false;
    bool insiderTradeIsDone = false;
    bool stockSplitIsDone = false;

    // insider trade fx needed
    int rumorCardIsTakedi = 0;

    // stock split fx needed
    bool isStockSplitCardSaved = false;

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
        Debug.Log("savedActionCardTakenTexture = " + savedActionCardTakenTexture);
        if (setInitIndex == 0)
        {
            SetInitialize(semester);
        }

        // player state disini
        switch (semester.playerState)
        {
            case GameState.Player1Turn:
                player = semester.CheckPlayerOrder(1); // mencari player urutan pertama
                Debug.Log("Player 1's Turn" + "Sekarang adalah giliran " + player.name);
                CoreActionPhase(semester);
                break;

            case GameState.Player2Turn:
                player = semester.CheckPlayerOrder(2); // mencari player urutan kedua
                Debug.Log("Player 2's Turn" + "Sekarang adalah giliran " + player.name);
                CoreActionPhase(semester);
                break;

            case GameState.Player3Turn:
                player = semester.CheckPlayerOrder(3); // mencari player urutan kedua
                Debug.Log("Player 3's Turn" + "Sekarang adalah giliran " + player.name);
                CoreActionPhase(semester);
                break;

            case GameState.PlayersStop:
                Debug.Log("Player Stop... it's time to change the phase");
                break;
        }
    }

    private void CoreActionPhase(SemesterStateManager semester)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();

        // jika sedang dalam efek tertentu ====================================================================
        if (currentFx == "Flashbuy")
        {
            GameObject actionPhaseButton = GameObject.Find("Button Fase Aksi");
            if (actionPhaseButton != null)
            {
                actionPhaseButton.transform.Find("Button Aktifkan Kartu").gameObject.SetActive(false);
                actionPhaseButton.transform.Find("Button Simpan Kartu").localPosition = new Vector3(0, 0, 0);
            }
        }
        else if (currentFx == "Insider Trade")
        {
            InsiderTrade(semester, savedActionCardTakenTexture);
        }
        else if (currentFx == "Stock Split")
        {
            StockSplit(semester, savedActionCardTakenTexture, playerScript);
        }
        else if (currentFx == "null")
        {
            if (flashbuyIsDone == true || insiderTradeIsDone == true || stockSplitIsDone == true)
            {
                semester.playerState = (GameState)1;
                savedActionCardTakenTexture = null; // cuman ini // masalahnya disini
            }
        }

        // jika kartu disimpan ==========================================================================================
        if (semester.actionCardisSaved == true)
        {
            Debug.Log("Kartu Disimpan");
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
                if (indexFx != 1)
                {
                    indexFx += 1;
                }
                else
                {
                    currentFx = "null"; // set kembali ke null
                    GameObject actionPhaseButton = GameObject.Find("Button Fase Aksi");
                    if (actionPhaseButton != null)
                    {
                        actionPhaseButton.transform.Find("Button Aktifkan Kartu").gameObject.SetActive(true);
                        actionPhaseButton.transform.Find("Button Simpan Kartu").localPosition = new Vector3(128, 0, 0);
                    }
                    semester.playerState = (GameState)1;
                    flashbuyIsDone = true;
                }
            }
            else
            {
                semester.playerState = (GameState)1; // ganti state pemain
            }
        }

        // jika kartu diaktifkan ========================================================================================
        if (semester.actionCardisActivated == true) // ini sifatnya sekali klik
        {
            Debug.Log("Kartu Diaktifkan!!!");
            semester.actionCardisActivated = false; // kembalikan actionCardisActivated ke false
            actionCardAnim = actionCardManagerScript.cardTakenObj.GetComponent<Animator>(); // jalankan animasi menyimpan kartu
            if (actionCardAnim != null)
            {
                savedActionCardTakenTexture = actionCardManagerScript.cardTakenObj.GetComponent<Renderer>().material.mainTexture.name;
                actionCardAnim.SetTrigger("TrSaveCard");
            }
        }

        // jika kartu diaktifkan dan objek kartu yang diaktifkan telah dihancurkan
        if (semester.actionCardisActivated == false && actionCardManagerScript.cardTakenIsDestroy == true) // sekali main
        {
            actionCardManagerScript.cardTakenIsDestroy = false;
            ActionCardDB(playerScript, semester); // akses Dictionary kartu aksi, dan jalankan fungsi yang sesuai
        }
    }

    void SetInitialize(SemesterStateManager semester)
    {
        Debug.Log("Update from SetInitialize() ActionPhaseState");
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
                timeEnterCD = 2.0f;
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
            //playerScript.AddActionCard("Merah", actionCardManagerScript.cardTakenObj);
            playerScript.AddActionCard("Merah", textureName.ToString());
        }
        else if (textureName.StartsWith("uv_O"))
        {
            playerScript.AddActionCard("Oranye", textureName.ToString());
        }
        else if (textureName.StartsWith("uv_B"))
        {
            playerScript.AddActionCard("Biru", textureName.ToString());
        }
        else if (textureName.StartsWith("uv_H"))
        {
            playerScript.AddActionCard("Hijau", textureName.ToString());
        }
    }

    private void ActionCardDB(PlayerScript playerScript, SemesterStateManager semester) // sekali main
    {
        // ambil nilai nama texture yang disimpan di variabel savedActionCardTakenTexture
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
                StockSplitFx();
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

    // FLASHBUY EFFECT SECTION ==============================================================================================
    private void FlashbuyFx() // sekali main
    {
        currentFx = "Flashbuy";
        actionCardManagerScript.cardTaken = false;
    }


    // INSIDER TRADE EFFECT SECTION ==============================================================================================

    private void InsiderTradeFx() // sekali main
    {
        currentFx = "Insider Trade";
        actionCardManagerScript.cardTaken = false;
    }

    private void InsiderTrade(SemesterStateManager semester, string textureName) // main di update()
    {
        if (!insiderTradeIsDone)
        {
            // camera movement ke board
            isMoving = true;
            MoveCamera(semester.mainCamera, semester.cameraPost1);

            // deactivate bacgkground dan hide kartu aksi
            semester.transparantBgObj.SetActive(false);
            semester.actionCardsObj.SetActive(false);

            // munculkan tombol "selesai"
            semester.rumorCardButton.SetActive(true);
        } else
        {
            isMoving = true;
            MoveCamera(semester.mainCamera, semester.cameraPost2);
            // deactivate bacgkground dan hide kartu aksi
            semester.transparantBgObj.SetActive(true);
            semester.actionCardsObj.SetActive(true);
            semester.rumorCardButton.SetActive(false);
            if (isMoving == false)
            {
                currentFx = "null";
            }
        }

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

    private void ITFindBoard(string boardName, SemesterStateManager semester) // main di update()
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
                insiderTradeIsDone = true;
            }
        }
    }


    // STOCK SPLIT EFFECT SECTION ==============================================================================================
    private void StockSplitFx() // sekali main
    {
        currentFx = "Stock Split";
        actionCardManagerScript.cardTaken = false;
    }
    
    private void StockSplit(SemesterStateManager semester, string textureName, PlayerScript playerScript) // main di update()
    {
        if (!stockSplitIsDone) // jika stock split effect masih berjalan
        {
            // camera movement ke board
            isMoving = true;
            MoveCamera(semester.mainCamera, semester.cameraPost1);

            // deactivate bacgkground dan hide kartu aksi
            semester.transparantBgObj.SetActive(false);
            semester.actionCardsObj.SetActive(false);
        }
        else // jika stock split effect sudah selesai
        {
            isMoving = true;
            MoveCamera(semester.mainCamera, semester.cameraPost2);
            // deactivate bacgkground dan hide kartu aksi
            semester.transparantBgObj.SetActive(true);
            semester.actionCardsObj.SetActive(true);
            if (isMoving == false)
            {
                currentFx = "null";
            }
        }

        string sectorColor = null;

        if (textureName.StartsWith("uv_M"))
        {
            sectorColor = "Merah";
            SSFindBoard("Red Board", semester);
        }
        else if (textureName.StartsWith("uv_O"))
        {
            sectorColor = "Oranye";
            SSFindBoard("Orange Board", semester);
        }
        else if (textureName.StartsWith("uv_B"))
        {
            sectorColor = "Biru";
            SSFindBoard("Blue Board", semester);
        }
        else if (textureName.StartsWith("uv_H"))
        {
            sectorColor = "Hijau";
            SSFindBoard("Green Board", semester);
        }
        else
        {
            Debug.Log("tidak terdeteksi uv_apa");
        }


        // simpan kartu jika player memiliki minimal 2 kartu aksi
        if (sectorColor != null && isStockSplitCardSaved == false)
        {
            if (playerScript.actionCardsOwned.Count >= 2)
            {
                playerScript.AddActionCard(sectorColor, savedActionCardTakenTexture);
            }
            isStockSplitCardSaved = true;
        }
    }

    private void SSFindBoard(string boardName, SemesterStateManager semester)
    {
        // ambil referensi board script terkait dan tarik value saham nya
        GameObject board = GameObject.Find(boardName); // cari board
        BoardScript boardScript = board.GetComponent<BoardScript>();
        float boardStockValue = boardScript.currentStockValue;

        // kalkulasi saham yang dipecah
        float breakBoardStockValue = Mathf.Ceil(boardStockValue / 2);
        Debug.Log("Stock Splite nilia : " + breakBoardStockValue);

        // pengecekan value saham board terakait
        if (stockSplitIsDone != true)
        {
            bool isCrash = false;
            // cek apakah ada crash kebawah atau keatas
            if (breakBoardStockValue < boardScript.stockCoordinates[0].value)
            {
                boardScript.currentStockCoordinate = boardScript.stockCoordinates[0].index - 1;
                isCrash = true;
                stockSplitIsDone = true;
            } else if (breakBoardStockValue > boardScript.stockCoordinates[boardScript.stockCoordinates.Count - 1].value)
            {
                boardScript.currentStockCoordinate = boardScript.stockCoordinates[boardScript.stockCoordinates.Count - 1].index + 1;
                isCrash = true;
                stockSplitIsDone = true;
            }

            if (!isCrash)
            {
                // cek apakah nilai ada dan sesuai dengan value board
                foreach (var coordinate in boardScript.stockCoordinates)
                {
                    // jika ada, ubah currentStockCoordinate board terkait
                    if (breakBoardStockValue == coordinate.value)
                    {
                        boardScript.currentStockCoordinate = coordinate.index;
                        stockSplitIsDone = true;
                        break;
                    }
                }

                // jika masih lolos, tambahkan 1 pada nilai value
                if (!stockSplitIsDone)
                {
                    breakBoardStockValue += 1;
                    foreach (var coordinate in boardScript.stockCoordinates)
                    {
                        // jika ada, ubah currentStockCoordinate board terkait
                        if (breakBoardStockValue == coordinate.value)
                        {
                            boardScript.currentStockCoordinate = coordinate.index;
                            stockSplitIsDone = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    // TRADE FEE EFFECT SECTION ==============================================================================================

    private void TradeFeeFx()
    {

    }

    // TENDER OFFER EFFECT SECTION ==============================================================================================

    private void TenderOfferFx()
    {

    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
