using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumorCardManagerScript : MonoBehaviour
{
    public Texture[] rumorCardTextures;
    GameObject[] rumorCards;

    public bool cardTaken = false;

    void Start()
    {
        rumorCards = GameObject.FindGameObjectsWithTag("Rumor Card");
        if (rumorCards.Length != 0)
        {
            RandomizeRumorCard();
        }
        else
        {
            Debug.Log("Rumor Card Object tidak terdeteksi");
        }
    }

    void RandomizeRumorCard()
    {
        foreach (GameObject card in rumorCards)
        {
            // memilih texture
            int randomIndex = Random.Range(0, rumorCardTextures.Length);
            Texture selectedTexture = rumorCardTextures[randomIndex];

            // mengambil komponen material dari kartu
            Renderer cardRenderer = card.GetComponent<Renderer>();
            if (cardRenderer != null)
            {
                Material newMaterial = new Material(cardRenderer.material);
                newMaterial.mainTexture = selectedTexture;
                cardRenderer.material = newMaterial;
            }
        }
    }

    void Update()
    {
        if (cardTaken)
        {
            //transparantBg2nd.SetActive(true);
            //actionButtons.SetActive(true);
        }
        else
        {
            //transparantBg2nd.SetActive(false);
            //actionButtons.SetActive(false);
        }
    }
}
