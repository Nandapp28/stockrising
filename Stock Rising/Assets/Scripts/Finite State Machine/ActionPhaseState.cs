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

    // referensi
    GameObject player;
    ActionCardManager actionCardManagerScript;

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
                // mencari player urutan pertama 
                player = semester.players[semester.CheckPlayerOrder(1)];
                PlayerScript playerScript = player.GetComponent<PlayerScript>();

                // jika kartu disimpan
                if (semester.actionCardisSaved == true)
                {
                    // simpan kartu ke data player sesuai warna
                    SaveActionCard(playerScript);

                    // kembalikan actionCardisSaved ke false
                    semester.actionCardisSaved = false;
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
