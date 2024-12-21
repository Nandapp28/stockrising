using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardScript : MonoBehaviour
{
    public GameObject blackPanel;

    private void OnEnable()
    {
        blackPanel.SetActive(true);
    }

    private void OnDisable()
    {
        blackPanel.SetActive(false);
    }
}
