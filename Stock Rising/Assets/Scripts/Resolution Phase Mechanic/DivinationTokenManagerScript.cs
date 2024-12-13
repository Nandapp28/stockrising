using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivinationTokenManagerScript : MonoBehaviour
{
    public Texture[] divTokenTextures;
    GameObject[] divTokens;
    public GameObject[] boards;

    SemesterStateManager state;

    public bool tokenTaken = false;

    void Awake()
    {
        state = GameObject.FindGameObjectWithTag("State Manager").GetComponent<SemesterStateManager>();
    }

    void Start()
    {
        divTokens = GameObject.FindGameObjectsWithTag("Divination Token");
        if (divTokens.Length != 0)
        {
            RandomizeDivToken();
            Debug.Log("Randomisasi Token Ramalan Telah Selesai");
        }
        else
        {
            Debug.Log("Object Token Ramalan tidak terdeteksi");
        }
    }

    void RandomizeDivToken()
    {
        foreach (GameObject card in divTokens)
        {
            // memilih texture
            int randomIndex = Random.Range(0, divTokenTextures.Length);
            Texture selectedTexture = divTokenTextures[randomIndex];

            // mengambil komponen material dari kartu
            Renderer tokenRenderer = card.GetComponent<Renderer>();
            if (tokenRenderer != null)
            {
                Material newMaterial = new Material(tokenRenderer.material);
                newMaterial.mainTexture = selectedTexture;
                tokenRenderer.material = newMaterial;
            }
        }
    }

    public void FlipDivToken(SemesterStateManager state)
    {
        Debug.Log("Hello from FlipDivToken() function");
        foreach (GameObject board in boards)
        {
            // referensikan token yang akan di flip
            Transform divinationTokens = board.transform.Find("Divination Tokens");
            Transform divinationToken = divinationTokens.Find("Divination Token " + state.semesterCount);
            GameObject token = divinationToken.Find("Token").gameObject;

            // flip token 
            token.transform.rotation = Quaternion.identity;
            //Debug.Log("Divination Token " + state.semesterCount);
        }
    }
}
