using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardManager : MonoBehaviour
{
    public Texture[] actionCardTextures;
    GameObject[] actionCards;

    public GameObject transparantBg2nd;
    public GameObject actionButtons;

    public bool cardTaken = false;
    public GameObject cardTakenObj;
    
    void Start()
    {
        actionCards = GameObject.FindGameObjectsWithTag("Action Card");
        if (actionCards.Length != 0)
        {
            RandomizeActionCard();
        } else
        {
            Debug.Log("Action Card Object tidak terdeteksi");
        }
    }

    void RandomizeActionCard()
    {
        foreach (GameObject card in actionCards)
        {
            // memilih texture
            int randomIndex = Random.Range(0, actionCardTextures.Length);
            Texture selectedTexture = actionCardTextures[randomIndex];

            // mengambil komponen material dari kartu
            Renderer cardRenderer = card.GetComponent<Renderer>();
            if (cardRenderer != null)
            {
                cardRenderer.material.mainTexture = selectedTexture;
            }
        }
    }

    void Update()
    {
        if (cardTaken)
        {
            transparantBg2nd.SetActive(true);
            actionButtons.SetActive(true);
        } else
        {
            transparantBg2nd.SetActive(false);
            actionButtons.SetActive(false);
        }
    }
}
