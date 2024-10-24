using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollButtonScript : MonoBehaviour
{
    public Material red, black;
    bool redB;
    void OnMouseDown()
    {
        //Debug.Log("Tombol Roll Dice ditekan");
        redB = !redB;
        if (red)
        {
            this.GetComponent<Renderer>().material = black;
        } else
        {
            this.GetComponent<Renderer>().material = red;
        }
    }
}
