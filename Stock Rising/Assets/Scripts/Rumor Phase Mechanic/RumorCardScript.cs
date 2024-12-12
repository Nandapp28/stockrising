using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RumorCardScript : MonoBehaviour
{
    public Material cardMaterial;
    public Transform camMiddlePoint;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public RumorCardManagerScript rumorCardManagerScript;

    [SerializeField] public bool isMoving = false;
    [SerializeField] public bool isTaked;
    [SerializeField] public bool isSaved;
    //[SerializeField] public bool isFliped = false;
    float moveSpeed = 8.0f;
    float rotateSpeed = 8.0f;

    //int flipIndex = 0;

    void Start()
    {
        rumorCardManagerScript = GameObject.Find("RumorCardManager").GetComponent<RumorCardManagerScript>();
        camMiddlePoint = Camera.main.transform.Find("Middle Point").gameObject.transform;
        initialPosition = transform.position; initialRotation = transform.rotation;
        isTaked = false;
    }

    void Update()
    {
        // animasi take & put down card
        if (isTaked == true)
        {
            //Debug.Log("isTaked adalah " + isTaked);
            isMoving = true;
            if (isMoving == true)
            {
                TakeCardAnim(camMiddlePoint.position, camMiddlePoint.rotation);
            }
        }
        else
        {
            //Debug.Log("isTaked adalah " + isTaked);
            isMoving = true;
            if (isMoving == true)
            {
                TakeCardAnim(initialPosition, initialRotation);
            }
        }

        //if (isFliped == true)
        //{
        //    isMoving = true;
        //    if (isMoving == true)
        //    {
        //        FlipCardAnim();
        //    }
        //}

        // animasi save card
        if (isSaved == true)
        {
            //actionCardManager.cardTaken = false;
            Destroy(gameObject);
        }
    }

    void TakeCardAnim(Vector3 targetPosition, Quaternion targetRotation)
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

    void FlipCardAnim()
    {
        // inisialisasi 
        Vector3 indexPosition = new Vector3(0, 0.25f, 0);
        Quaternion indexRotation = Quaternion.Euler(0,0,0);

        transform.position = indexPosition; transform.rotation = indexRotation;

        //if (flipIndex == 0)
        //{
        //    transform.position = Vector3.Lerp(
        //    transform.position,
        //    indexPosition,
        //    moveSpeed * Time.deltaTime
        //    );
        //    transform.rotation = Quaternion.Lerp(
        //        transform.rotation,
        //        indexRotation,
        //        rotateSpeed * Time.deltaTime
        //        );
        //    if (Vector3.Distance(transform.position, indexPosition) < 0.1f &&
        //        Quaternion.Angle(transform.rotation, indexRotation) < 0.5f)
        //    {
        //        flipIndex = 1;
        //    }
        //} else
        //{
        //    transform.position = Vector3.Lerp(
        //    transform.position,
        //    initialPosition,
        //    moveSpeed * Time.deltaTime
        //    );
        //    if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        //    {
        //        isMoving = false;
        //        flipIndex = 0;
        //    }
        //}
        
    }
}
