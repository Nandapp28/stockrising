using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionPhaseState : SemesterBaseState
{
    int setInitIndex = 0;
    float timeEnterCD = 2.0f;

    // main camera
    float moveSpeed = 2.0f;
    float rotateSpeed = 2.0f;
    bool isMoving = false;

    GameObject board;
    BoardScript boardScript;
    string tokenTextureName;

    bool isCoreResolutionPhaseDone = false;
    bool isTextureNameSaved = false;

    bool isSetFinalization = false;
    bool isDividendDitributed = false;

    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseName = "Fase Resolusi";
        semester.phaseTitleParent.gameObject.SetActive(true);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Update from Resolution Phase State");
        if (setInitIndex == 0)
        {
            SetInitialize(semester);
        } else
        {
            if (isSetFinalization == true)
            {
                if (isDividendDitributed == false)
                {
                    DividendDistribution(semester);
                    isDividendDitributed = true;
                } else
                {
                    if (SetFinalization(semester))
                    {
                        setInitIndex = 0;
                        timeEnterCD = 2.0f;
                        isSetFinalization = false;
                        isDividendDitributed = false;
                        //semester.SwitchState(semester.);
                        semester.SwitchSemester();
                        semester.SwitchPlayerState();
                    }
                }
            } else
            {
                switch (semester.playerState)
                {
                    case GameState.Player1Turn:
                        Debug.Log("Player 1's Turn " + "Sekarang adalah giliran ");
                        CoreResolutionPhase(semester, "Red Board");
                        break;

                    case GameState.Player2Turn:
                        Debug.Log("Player 2's Turn " + "Sekarang adalah giliran ");
                        CoreResolutionPhase(semester, "Orange Board");
                        break;

                    case GameState.Player3Turn:
                        Debug.Log("Player 3's Turn " + "Sekarang adalah giliran ");
                        CoreResolutionPhase(semester, "Blue Board");
                        break;

                    case GameState.PlayersStop:
                        Debug.Log("PlayerStop's turn");
                        CoreResolutionPhase(semester, "Green Board");
                        break;
                }
            }
        }
    }

    private void DividendDistribution(SemesterStateManager semester)
    {
        // perulangan boards yang ada
        GameObject[] boards = semester.divinationTokenManagerScript.boards;
        foreach (var board in boards)
        {
            // ambil nilai CP 
            BoardScript boardScript = board.GetComponent<BoardScript>();
            int boardCPValue = (int)boardScript.currentCPValue;
            // terapkan pada player yang memiliki kartu saham sektor
            foreach (var player in semester.players)
            {
                PlayerScript playerScript = player.GetComponent<PlayerScript>();
                if (board.name == "Red Board")
                {
                    AddDividendDistribution(boardCPValue, playerScript, playerScript.actionCMerah);
                } else if (board.name == "Orange Board")
                {
                    AddDividendDistribution(boardCPValue, playerScript, playerScript.actionCOranye);
                } else if (board.name == "Blue Board")
                {
                    AddDividendDistribution(boardCPValue, playerScript, playerScript.actionCBiru);
                } else if (board.name == "Green Board")
                {
                    AddDividendDistribution(boardCPValue, playerScript, playerScript.actionCHijau);
                }
            }
        }
    }

    private static void AddDividendDistribution(int boardCPValue, PlayerScript playerScript, int countCard)
    {
        if (countCard != 0)
        {
            int addIP = boardCPValue * countCard;
            playerScript.investmentPoint += addIP;
        }
    }

    private void CoreResolutionPhase(SemesterStateManager semester, string boardName)
    {
        board = GameObject.Find(boardName);
        boardScript = board.GetComponent<BoardScript>();

        // move kamera
        GameObject camPost = board.transform.Find("Camera Post").gameObject;
        isMoving = true;
        if (isMoving == true)
        {
            MoveCamera(semester.mainCamera, camPost);
            isMoving = false;
        }

        DelaySession1();

        // referensi ke objek token yang akan diaktifkan
        Transform divTokens = board.transform.Find("Divination Tokens");
        int divTokensTotal = divTokens.childCount;
        GameObject divToken = divTokens.Find("Divination Token " + (5 - divTokensTotal)).gameObject;
        GameObject token = divToken.transform.Find("Token").gameObject;
        DivinationTokenScript divinationTokenScript = token.GetComponent<DivinationTokenScript>();
        if (delaySession1Done == true)
        {
            if (timeOpenToken >= 0)
            {
                timeOpenToken -= Time.deltaTime;
                divinationTokenScript.isTaked = true;
            } else
            {
                if (isTextureNameSaved == false)
                {
                    tokenTextureName = token.GetComponent<Renderer>().material.mainTexture.name;
                    isTextureNameSaved = true;
                    divinationTokenScript.isTaked = false;
                    divToken.SetActive(false);
                }
                DelaySession2();
            }

            if (delaySession2Done == true && isTextureNameSaved == true)
            {
                if (isCoreResolutionPhaseDone == false)
                {
                    Debug.Log(tokenTextureName);
                    DivTokenDB(boardScript, semester);
                    isCoreResolutionPhaseDone = true;
                }
                DelaySession3();
            }

            if (delaySession3Done == true)
            {
                Debug.Log("Ganti state selanjutnya");

                isTextureNameSaved = false;

                timeOpenToken = 2.5f;
                delaySession1Done = false;
                timeDelaySession1 = 1.5f;
                delaySession2Done = false;
                timeDelaySession2 = 1.0f;
                delaySession3Done = false;
                timeDelaySession3 = 2.5f;
                isCoreResolutionPhaseDone = false;

                semester.DestroyObject(divToken);

                if (semester.playerState == (GameState)3)
                {
                    Debug.Log("Ganti Fase Resolusi");
                    isSetFinalization = true;
                    //semester.SwitchState(semester.resolutionPhase);
                    //semester.SwitchPlayerState();
                }
                else
                {
                    semester.SwitchPlayerState();
                }
            }
        }

        // applied divination token
    }

    void DivTokenDB(BoardScript boardScript, SemesterStateManager semester)
    {
        string textureName = tokenTextureName;

        Dictionary<Func<string, bool>, Action> dynamicActions = new Dictionary<Func<string, bool>, Action>
        {
            { name => name == "up", () => {
                AppliedToken(boardScript, 1);
            } },
            { name => name == "down", () => {
                AppliedToken(boardScript, -1);;
            } },
            { name => name == "up-double", () => {
                AppliedToken(boardScript, 2);;
            } },
            { name => name == "down-double", () => {
                AppliedToken(boardScript, -2);;
            } },
        };

        foreach (var entry in dynamicActions)
        {
            if (entry.Key(textureName)) // Panggil fungsi kondisi
            {
                entry.Value(); // Jalankan aksi
                return;
            }
        }
    }

    private void AppliedToken(BoardScript boardScript, int i)
    {
        int stockCoordinate = i;
        boardScript.currentCPCoordinate += stockCoordinate;
    }

    float timeOpenToken = 2.5f;

    float timeDelaySession1 = 1.5f;
    bool delaySession1Done = false;
    bool DelaySession1()
    {
        if (timeDelaySession1 >= 0)
        {
            timeDelaySession1 -= Time.deltaTime;
        }
        else
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
        }
    }

    bool SetFinalization(SemesterStateManager semester)
    {
        Debug.Log("Update from SetFinalization() ActionPhaseState");
        isMoving = true;
        MoveCamera(semester.mainCamera, semester.cameraPost1);
        if (isMoving == false)
        {
            return true;
        }
        return false;
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
