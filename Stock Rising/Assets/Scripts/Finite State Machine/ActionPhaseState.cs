using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionPhaseState : SemesterBaseState
{
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
        SetInitialize(semester);
        switch (semester.playerState)
        {
            case GameState.Player1Turn:
                Debug.Log("Player 1's Turn");
                player = semester.players[semester.CheckPlayerOrder(1)]; // mencari player urutan pertama 
                PlayerScript playerScript = player.GetComponent<PlayerScript>();

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
                if (semester.actionCardisActivated == true) 
                {
                    ActionCardDB(playerScript); // akses Dictionary kartu aksi, dan jalankan fungsi yang sesuai
                    semester.actionCardisActivated = false; // kembalikan actionCardisActivated ke false
                    actionCardAnim = actionCardManagerScript.cardTakenObj.GetComponent<Animator>(); // jalankan animasi menyimpan kartu
                    if (actionCardAnim != null)
                    {
                        actionCardAnim.SetTrigger("TrSaveCard");
                    }
                    //semester.playerState = (GameState)2; // ganti state pemain

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

    private void ActionCardDB(PlayerScript playerScript)
    {
        // ambil obj renderer
        Renderer actionCardObjRenderer = actionCardManagerScript.cardTakenObj.GetComponent<Renderer>();
        // simpan texture name melalui komponen renderer obj
        string textureName = actionCardObjRenderer.material.mainTexture.name;

        // membuat dynamic dictionary
        Dictionary<Func<string, bool>, Action> dynamicActions = new Dictionary<Func<string, bool>, Action>
        {
            { name => name.EndsWith("FB"), () => {
                FlashbuyFx();
            } },
            { name => name.EndsWith("IT"), () => {
                Debug.Log("Insider Trade Card is Activated");
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
        Debug.Log("Pilih 2 kartu untuk disimpan"); // show title
        Button activateButton = GameObject.Find("Button Aktifkan Kartu").GetComponent<Button>(); // referensi button
        activateButton.enabled = false; // disable activate button
        currentFx = "Flashbuy";
        actionCardManagerScript.cardTaken = false;
    }

    private void InsiderTradeFx()
    {

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
