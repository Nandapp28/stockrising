using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ActionCardScript : MonoBehaviour
{
    //public Texture[] actionCardTextures;
    public Texture actionCardTexture;
    public Material cardMaterial;
    GameObject cameraMiddlePoint;
    Transform cardPlaced;
    bool isTaked;

    public bool isActive = false;

    void Start()
    {
        cameraMiddlePoint = Camera.main.transform.Find("Middle Point").gameObject;
        cardPlaced = gameObject.transform;
    }

    void OnMouseDown()
    {
        Debug.Log("Card Clicked");
        if (cameraMiddlePoint != null)
        {
            Debug.Log("Camera Middle Point Detected");
            Transform camMiddlePoint = cameraMiddlePoint.transform;
            transform.position = camMiddlePoint.position;
            transform.rotation = camMiddlePoint.rotation;
        } else
        {
            Debug.Log("Camera Middle Point Not Detected");
        }
    }
}
