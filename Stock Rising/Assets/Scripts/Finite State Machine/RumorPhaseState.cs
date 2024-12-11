using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumorPhaseState : SemesterBaseState
{
    int setInitIndex = 0;
    float timeEnterCD = 2.0f;

    // main camera
    float moveSpeed = 2.0f;
    float rotateSpeed = 2.0f;
    bool isMoving = false;

    GameObject board;
    BoardScript boardScript;

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

                    // move kamera
                    GameObject camPost = board.transform.Find("Camera Post").gameObject;
                    isMoving = true;
                    if (isMoving == true)
                    {
                        MoveCamera(semester.mainCamera, camPost);
                        isMoving = false;
                    }

                    // flip kartu
                    // angkat kartu
                    // terapkan efek
                    break;

                case GameState.Player2Turn:
                    Debug.Log("Player 2's Turn " + "Sekarang adalah giliran ");
                    break;

                case GameState.Player3Turn:
                    Debug.Log("Player 3's Turn " + "Sekarang adalah giliran ");
                    break;

                case GameState.PlayersStop:
                    Debug.Log("Ganti Fase Rumor");
                    break;
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
