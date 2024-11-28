using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardScript : MonoBehaviour
{
    public Material cardMaterial;
    Transform camMiddlePoint;
    Vector3 initialPosition;
    Quaternion initialRotation;
    public ActionCardManager actionCardManager;

    [SerializeField] bool isMoving = false;
    [SerializeField] bool isTaked;
    float moveSpeed = 8.0f;
    float rotateSpeed = 8.0f;

    public bool isActive = false;

    void Start()
    {
        camMiddlePoint = Camera.main.transform.Find("Middle Point").gameObject.transform;
        initialPosition = transform.position; initialRotation = transform.rotation;
        isTaked = false;
    }

    void OnMouseDown()
    {
        Debug.Log("Card Clicked");
        if (camMiddlePoint != null)
        {
            Debug.Log("Camera Middle Point Detected");
            isTaked = !isTaked;
            actionCardManager.cardTaken = !actionCardManager.cardTaken;
            actionCardManager.cardTakenObj = gameObject;

        } else
        {
            Debug.Log("Camera Middle Point Not Detected");
        }
    }

    void Update()
    {
        if (isTaked == true)
        {
            Debug.Log("isTaked adalah " + isTaked);
            isMoving = true;
            if (isMoving == true)
            {
                MoveCard(camMiddlePoint.position, camMiddlePoint.rotation);
            }
        }
        else
        {
            Debug.Log("isTaked adalah " + isTaked);
            isMoving = true;
            if (isMoving == true)
            {
                MoveCard(initialPosition, initialRotation);
            }
        }
    }

    void MoveCard(Vector3 targetPosition, Quaternion targetRotation)
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
            );
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
            );
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f &&
            Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            isMoving = false;
        }
    }
}
