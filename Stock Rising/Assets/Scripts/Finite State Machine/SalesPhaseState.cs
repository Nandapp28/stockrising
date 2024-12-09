using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesPhaseState : SemesterBaseState
{
    int setInitIndex = 0;
    float timeEnterCD = 2.0f;

    // main camera
    float moveSpeed = 2.0f;
    float rotateSpeed = 2.0f;
    bool isMoving = false;

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
        }

        switch (semester.playerState)
        {
            case GameState.Player1Turn:
                Debug.Log("Player 1's Turn " + "Sekarang adalah giliran ");
                player = semester.CheckPlayerOrder(1); // mencari urutan player

                actionCardManagerScript.currentPlayerScript = player.GetComponent<PlayerScript>();
                semester.actionCardsObj.SetActive(true);
                semester.actionCardManagerObj.SetActive(true);

                break;

            case GameState.Player2Turn:
                Debug.Log("Player 2's Turn " + "Sekarang adalah giliran ");
                break;

            case GameState.Player3Turn:
                Debug.Log("Player 3's Turn " + "Sekarang adalah giliran ");
                break;

            case GameState.PlayersStop:
                break;
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

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
