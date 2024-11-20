using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardManager : MonoBehaviour
{
    public Texture[] actionCardTextures;
    GameObject[] actionCards;
    
    void Start()
    {
        actionCards = GameObject.FindGameObjectsWithTag("Action Card");
    }

    void RandomizeActionCard()
    {
        foreach (GameObject card in actionCards)
        {
            Material cardMat = card.GetComponent<Material>();
            int textureIndex = Random.Range(0, actionCardTextures.Length);
            cardMat.mainTexture = actionCardTextures[textureIndex];
        }
    }

    void Update()
    {
        
    }
}
