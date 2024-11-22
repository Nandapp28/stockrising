using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionPhaseState : SemesterBaseState
{
    float timeEnterCD = 2.0f;

    // main camera
    float moveSpeed = 2.0f;
    float rotateSpeed = 2.0f;
    bool isMoving = false;

    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseTitleParent.gameObject.SetActive(true);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        SetInitialize(semester);
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

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
