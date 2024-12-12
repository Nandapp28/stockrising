using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesPhaseState : SemesterBaseState
{
    int setInitIndex = 0;
    float timeEnterCD = 2.0f;
    float timeCoreSalesPhaseCD = 30.0f;

    // main camera
    float moveSpeed = 2.0f;
    float rotateSpeed = 2.0f;
    bool isMoving = false;

    // button fase aksi needed
    

    GameObject player;
    ActionCardManager actionCardManagerScript;

    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseName = "Fase Penjualan";
        semester.phaseTitleParent.gameObject.SetActive(true);

        // referensi ke script ActionCardManager
        actionCardManagerScript = semester.actionCardManagerObj.GetComponent<ActionCardManager>();
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Update from Sales Phase State");
        if (setInitIndex == 0)
        {
            SetInitialize(semester);
        } else
        {
            semester.salesPhaseTimerText.text = ((int)timeCoreSalesPhaseCD).ToString();
            switch (semester.playerState)
            {
                case GameState.Player1Turn:
                    Debug.Log("Player 1's Turn " + "Sekarang adalah giliran ");
                    player = semester.CheckPlayerOrder(1); // mencari urutan player, output GameObject
                    if (timeCoreSalesPhaseCD >= 0)
                    {
                        timeCoreSalesPhaseCD -= Time.deltaTime;
                        CoreSalesPhase(semester);
                    } else
                    {
                        Debug.Log("Ganti Pemain");
                        semester.salesPhaseButton.SetActive(false);
                        timeCoreSalesPhaseCD = 30.0f;
                        semester.SwitchPlayerState();
                    }
                    break;

                case GameState.Player2Turn:
                    Debug.Log("Player 2's Turn " + "Sekarang adalah giliran ");
                    player = semester.CheckPlayerOrder(2); // mencari urutan player, output GameObject
                    if (timeCoreSalesPhaseCD >= 0)
                    {
                        timeCoreSalesPhaseCD -= Time.deltaTime;
                        CoreSalesPhase(semester);
                    }
                    else
                    {
                        Debug.Log("Ganti Pemain");
                        semester.salesPhaseButton.SetActive(false);
                        timeCoreSalesPhaseCD = 30.0f;
                        semester.SwitchPlayerState();
                    }
                    break;

                case GameState.Player3Turn:
                    Debug.Log("Player 3's Turn " + "Sekarang adalah giliran ");
                    player = semester.CheckPlayerOrder(3); // mencari urutan player, output GameObject
                    if (timeCoreSalesPhaseCD >= 0)
                    {
                        timeCoreSalesPhaseCD -= Time.deltaTime;
                        CoreSalesPhase(semester);
                    }
                    else
                    {
                        Debug.Log("Ganti Pemain");
                        semester.salesPhaseButton.SetActive(false);
                        timeCoreSalesPhaseCD = 30.0f;
                        semester.SwitchPlayerState();
                    }
                    break;

                case GameState.PlayersStop:
                    Debug.Log("Ganti Fase Rumor");
                    semester.SwitchState(semester.rumorPhase);
                    semester.SwitchPlayerState();
                    break;
            }
        }
    }

    private void CoreSalesPhase(SemesterStateManager semester)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();

        // cek apakah player punya kartu saham atau tidak
        if (playerScript.actionCMerah + playerScript.actionCOranye +
            playerScript.actionCBiru + playerScript.actionCHijau != 0)
        {
            semester.salesPhaseButton.SetActive(true);
            if (semester.isSalesPhaseSkip == true)
            {
                Debug.Log("Ganti Pemain");
                semester.isSalesPhaseSkip = false;
                semester.salesPhaseButton.SetActive(false);
                timeCoreSalesPhaseCD = 30.0f;
                semester.SwitchPlayerState();
            }
            else
            {
                Debug.Log("Proses menjual kartu");
            }

            if (semester.isSalesPhaseSell == true)
            {
                Debug.Log("Ganti Pemain");
                semester.isSalesPhaseSell = false;
                semester.salesPhaseButton.SetActive(false);
                timeCoreSalesPhaseCD = 30.0f;
                semester.SwitchPlayerState();
            }
        }
        else
        {
            Debug.Log("Ganti Pemain");
            semester.salesPhaseButton.SetActive(false);
            timeCoreSalesPhaseCD = 30.0f;
            semester.SwitchPlayerState();
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
