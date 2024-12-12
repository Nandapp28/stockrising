using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RumorPhaseState : SemesterBaseState
{
    int setInitIndex = 0;
    float timeEnterCD = 2.0f;

    // main camera
    float moveSpeed = 2.0f;
    float rotateSpeed = 2.0f;
    bool isMoving = false;

    float timeOpenRumorCard = 5.0f;

    GameObject board;
    BoardScript boardScript;

    string rumorCardTextureName;
    bool isTextureNameSaved = false;

    bool isCoreRumorPhaseDone = false;

    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseName = "Fase Rumor";
        semester.phaseTitleParent.gameObject.SetActive(true);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Update from Rumor Phase State");
        if (setInitIndex == 0)
        {
            SetInitialize(semester);
        } else
        {
            switch (semester.playerState)
            {
                case GameState.Player1Turn:
                    Debug.Log("Player 1's Turn " + "Sekarang adalah giliran ");
                    // referensi ke boardscript 
                    board = GameObject.Find("Red Board");
                    boardScript = board.GetComponent<BoardScript>();
                    CoreRumorPhase(semester, boardScript);

                    break;

                case GameState.Player2Turn:
                    Debug.Log("Player 2's Turn " + "Sekarang adalah giliran ");
                    board = GameObject.Find("Orange Board");
                    boardScript = board.GetComponent<BoardScript>();
                    CoreRumorPhase(semester, boardScript);
                    break;

                case GameState.Player3Turn:
                    Debug.Log("Player 3's Turn " + "Sekarang adalah giliran ");
                    board = GameObject.Find("Blue Board");
                    boardScript = board.GetComponent<BoardScript>();
                    CoreRumorPhase(semester, boardScript);
                    break;

                case GameState.PlayersStop:
                    Debug.Log("PlayerStop's turn");
                    board = GameObject.Find("Green Board");
                    boardScript = board.GetComponent<BoardScript>();
                    CoreRumorPhase(semester, boardScript);
                    break;
            }
        }
    }

    private void CoreRumorPhase(SemesterStateManager semester, BoardScript boardScript)
    {
        // move kamera
        GameObject camPost = board.transform.Find("Camera Post").gameObject;
        isMoving = true;
        if (isMoving == true)
        {
            MoveCamera(semester.mainCamera, camPost);
            isMoving = false;
        }

        DelaySession1();

        // referensi ke kartu rumor paling atas
        Transform rumorCards = board.transform.Find("Rumor Cards");
        int rumorCardsTotal = rumorCards.childCount;
        GameObject rumorCard = rumorCards.Find("Rumor Card " + rumorCardsTotal).gameObject;
        GameObject card = rumorCard.transform.Find("Card").gameObject;
        RumorCardScript rumorCardScript = card.GetComponent<RumorCardScript>();
        if (delaySession1Done == true)
        {
            // angkat kartu selama 10 detik
            if (timeOpenRumorCard >= 0)
            {
                timeOpenRumorCard -= Time.deltaTime;
                rumorCardScript.isTaked = true;
            }
            else
            {
                if (isTextureNameSaved == false)
                {
                    rumorCardTextureName = card.GetComponent<Renderer>().material.mainTexture.name;
                    isTextureNameSaved = true;
                    rumorCardScript.isTaked = false;
                    rumorCard.SetActive(false);
                }
                DelaySession2();
            }
        }

        if (delaySession2Done == true && isTextureNameSaved == true)
        {
            // terapkan efek
            if (isCoreRumorPhaseDone == false)
            {
                Debug.Log(rumorCardTextureName);
                RumorCardDB(boardScript, semester);
                isCoreRumorPhaseDone = true;
            }
            DelaySession3();
        }

        if (delaySession3Done == true)
        {
            Debug.Log("Ganti state selanjutnya");

            isTextureNameSaved = false;

            timeOpenRumorCard = 5.0f;
            delaySession1Done = false;
            timeDelaySession1 = 1.5f;
            delaySession2Done = false;
            timeDelaySession2 = 1.0f;
            delaySession3Done = false;
            timeDelaySession3 = 2.5f;
            isCoreRumorPhaseDone = false;

            semester.DestroyObject(rumorCard);

            if (semester.playerState == (GameState)3)
            {
                Debug.Log("Ganti Fase Resolusi");
                semester.SwitchState(semester.resolutionPhase);
                semester.SwitchPlayerState();
            } else
            {
                semester.SwitchPlayerState();
            }
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
            setInitIndex = 1;
            timeEnterCD = 2.0f;
        }
    }

    private void RumorCardDB(BoardScript boardScript, SemesterStateManager semester) // sekali main
    {
        // ambil nilai nama texture yang disimpan di variabel savedActionCardTakenTexture
        string textureName = rumorCardTextureName;

        // membuat dynamic dictionary
        Dictionary<Func<string, bool>, Action> dynamicActions = new Dictionary<Func<string, bool>, Action>
        {
            { name => name == "uv_KR1", () => { 
                AppliedRumor(boardScript, 2);
            } },
            { name => name == "uv_KR2", () => {
                AppliedRumor(boardScript, 2);;
            } },
            { name => name == "uv_KR3", () => {
                AppliedRumor(boardScript, -1);;
            } },
            { name => name == "uv_KR4", () => {
                AppliedRumor(boardScript, -1);;
            } },
            { name => name == "uv_KR5", () => {
                AppliedRumor(boardScript, 3);
            } },
            { name => name == "uv_KR6", () => {
                AppliedRumor(boardScript, 3);
            } },
            { name => name == "uv_KR7", () => {
                AppliedRumor(boardScript, -3);
            } },
            { name => name == "uv_KR8", () => {
                AppliedRumor(boardScript, -3);
            } },
            { name => name == "uv_KR9", () => {
                AppliedRumor(boardScript, -2);
            } },
            { name => name == "uv_KR10", () => {
                AppliedRumor(boardScript, -2);
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

    private static void AppliedRumor(BoardScript boardScript, int i)
    {
        int stockCoordinate = i;
        boardScript.currentStockCoordinate += stockCoordinate;
    }

    float timeDelaySession1 = 1.5f;
    bool delaySession1Done = false;
    bool DelaySession1()
    {
        if (timeDelaySession1 >= 0)
        {
            timeDelaySession1 -= Time.deltaTime;
        } else
        {
            return delaySession1Done = true;
        }

        return delaySession1Done = false;
    }

    float timeDelaySession2 = 1.0f;
    bool delaySession2Done = false;
    bool DelaySession2()
    {
        if (timeDelaySession2 >= 0)
        {
            timeDelaySession2 -= Time.deltaTime;
        }
        else
        {
            return delaySession2Done = true;
        }

        return delaySession2Done = false;
    }

    float timeDelaySession3 = 2.5f;
    bool delaySession3Done = false;
    bool DelaySession3()
    {
        if (timeDelaySession3 >= 0)
        {
            timeDelaySession3 -= Time.deltaTime;
        }
        else
        {
            return delaySession3Done = true;
        }

        return delaySession3Done = false;
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

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
