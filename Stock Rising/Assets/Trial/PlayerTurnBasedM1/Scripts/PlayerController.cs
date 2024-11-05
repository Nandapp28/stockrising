using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float finishline = 5f;
    public bool isTurn = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();

        if (transform.position.z >= finishline)
        {
            Debug.Log("Pemain " + gameObject.name + " menang!");
        }
    }

    void MovePlayer()
    {
        if (isTurn && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 moveDirection = transform.forward * movementSpeed;
            rb.MovePosition(rb.position + moveDirection);
            isTurn = false;

            //Panggil fungsi untuk ganti giliran
            GameManager.instance.NextTurn();
        }
    }
}

