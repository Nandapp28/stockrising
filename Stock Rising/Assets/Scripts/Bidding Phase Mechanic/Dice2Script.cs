using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice2Script : MonoBehaviour
{
    public Transform[] faceDetectors;
    public Rigidbody rb;
    public bool isRolling = false; // trigger button
    public int indexResult = 0;
    public Vector3 initialPosition;
    public Quaternion initialRotation;

    //void Start()
    //{
    //    initialPosition = transform.position;
    //    initialRotation = transform.rotation;
    //}

    //void OnEnable()
    //{
    //    transform.position = initialPosition;
    //    transform.rotation = initialRotation;
    //}

    void Update()
    {
        if (isRolling == true)
        {
            transform.position = new Vector3(1.5f, 2.5f, 0);
            indexResult = 0;
            SetInitialState();

            isRolling = false;
        }

        if (CheckObjectHasStopped())
        {
            indexResult = FindFaceResult();
            indexResult += 1;
            //Debug.Log("Hasil DADU: " + indexResult);
        }
    }

    private void SetInitialState()
    {
        int x = Random.Range(0, 360);
        int y = Random.Range(0, 360);
        int z = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(x, y, z);

        x = Random.Range(0, 5);
        y = Random.Range(0, 5);
        z = Random.Range(0, 5);
        Vector3 force = new Vector3(0, -y, 0);

        x = Random.Range(0, 10);
        y = Random.Range(0, 10);
        z = Random.Range(0, 10);
        Vector3 torque = new Vector3(x, y, z);

        transform.rotation = rotation;
        rb.velocity = force;

        this.rb.maxAngularVelocity = 1000;
        rb.AddTorque(torque, ForceMode.VelocityChange);
    }
    public bool CheckObjectHasStopped()
    {
        if (rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero)
        {
            return true;
        }
        else return false;
    }
    public int FindFaceResult()
    {
        int maxIndex = 0;
        for (int i = 1; i < faceDetectors.Length; i++)
        {
            if (faceDetectors[maxIndex].transform.position.y < faceDetectors[i].transform.position.y)
            {
                maxIndex = i;
            }
        }
        return maxIndex;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Memutar suara saat bersentuhan dengan objek apa pun
        AudioManager.instance.DiceCollisionSFX();
    }
}
