using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardScript : MonoBehaviour
{
    //public Texture[] actionCardTextures;
    public Texture actionCardTexture;
    public Material cardMaterial;

    public bool isActive = false;

    void Start()
    {
        //int randomIndex = Random.Range(0, actionCardTextures.Length);
        //cardMaterial.mainTexture = actionCardTextures[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
