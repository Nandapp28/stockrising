using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionCardManager : MonoBehaviour
{
    public Texture[] actionCardTextures;
    public GameObject[] actionCards;
    public bool actionCardsIsSpawned = false;
    public GameObject actionCardPrefab;
    public Transform spawnParent;

    public SemesterStateManager state;

    public GameObject transparantBg2nd;
    public GameObject actionButtons;

    public bool cardTaken = false;
    public bool cardTakenIsDestroy = false;
    public GameObject cardTakenObj;

    public PlayerScript currentPlayerScript;


    private void OnDisable()
    {
        actionCardsIsSpawned = false;
        foreach (Transform child in spawnParent)
        {
            Destroy(child.gameObject);
        }
    }

    void OnEnable()
    {
        if (state.phaseName == "Fase Aksi")
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
        } else if (state.phaseName == "Fase Penjualan")
        {
            ShowPlayerActionCard(currentPlayerScript);

            actionCards = GameObject.FindGameObjectsWithTag("Action Card");
            if (actionCards.Length == 0)
            {
                Debug.Log("actionCards kosong");
            }
            string[] textureCode = { "uv_M", "uv_O", "uv_B", "uv_H" };
            int index = 0;
            foreach (GameObject card in actionCards)
            {
                // pilih texture dan assign ke objek kartu
                Texture selectedTexture = actionCardTextures.FirstOrDefault(texture => texture.name.StartsWith(textureCode[index]));
                Debug.Log(selectedTexture);
                index += 1;

                Renderer cardRenderer = card.GetComponent<Renderer>();
                Material newMaterial = new Material(cardRenderer.material);
                newMaterial.mainTexture = selectedTexture;
                cardRenderer.material = newMaterial;
            }
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
        actionCardsIsSpawned = true;
    }

    void ShowPlayerActionCard(PlayerScript playerScript)
    {
        // cek player punya kartu berapa
        int numberOfCards = playerScript.actionCardsOwned.Count;
        if (numberOfCards == 0)
        {
            // jika tidak ada
            Debug.Log("Tidak memiliki kartu aksi");
        } else
        {
            // jika ada
            for (int i = 0; i < 4; i++)
            {
                float xPosition = -i-1;
                Vector3 spawnPosition = new Vector3(xPosition, 0, 0);
                Quaternion spawnRotation = new Quaternion(0, 0, 180, 0);
                GameObject card = Instantiate(actionCardPrefab, spawnPosition, spawnRotation);

                // spawn kartu di parent tertentu
                if (spawnParent != null)
                {
                    card.transform.SetParent(spawnParent, false);
                }
            }
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
            cardTakenIsDestroy = false;
        } else
        {
            transparantBg2nd.SetActive(false);
            actionButtons.SetActive(false);
        }
    }
}
