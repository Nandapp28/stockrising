using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardManager : MonoBehaviour
{
    public Texture[] actionCardTextures;
    GameObject[] actionCards;
    public GameObject actionCardPrefab;
    public Transform spawnParent;

    public SemesterStateManager state;

    public GameObject transparantBg2nd;
    public GameObject actionButtons;

    public bool cardTaken = false;
    public GameObject cardTakenObj;

    void OnEnable()
    {
        // fungsi untuk menampilkan kartu sesuai jumlah player
        SpawnCards();

        // cari kartu yang sudah ter-spawn, kemudian di randomize texturenya
        actionCards = GameObject.FindGameObjectsWithTag("Action Card");
        if (actionCards.Length != 0)
        {
            RandomizeActionCard();
        }
        else
        {
            Debug.Log("Action Card Object tidak terdeteksi");
        }
    }

    private void SpawnCards()
    {
        // hitung total pemain kemudian di kali 2 utk mendapatkan jumlah kartu
        int totalPlayers = state.players.Length;
        int totalActionCards = totalPlayers * 2;
        if (totalPlayers == 3)
        {
            for (int i = 0; i < totalActionCards; i++)
            {
                float xPosition = -i;
                Vector3 spawnPosition = new Vector3(xPosition, 0, 0);
                GameObject card = Instantiate(actionCardPrefab, spawnPosition, Quaternion.identity);

                // spawn kartu di parent tertentu
                if (spawnParent != null)
                {
                    card.transform.SetParent(spawnParent, false);
                }
            }
        }
        else if (totalPlayers == 4)
        {

        }
        else if (totalPlayers == 5)
        {

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
                //cardRenderer.material.mainTexture = selectedTexture;
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
            transparantBg2nd.SetActive(true);
            actionButtons.SetActive(true);
        } else
        {
            transparantBg2nd.SetActive(false);
            actionButtons.SetActive(false);
        }
    }
}
