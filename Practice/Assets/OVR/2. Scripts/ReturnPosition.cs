using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPosition : MonoBehaviour
{
    private Vector3 Position;
    private Quaternion Rotation;
    private bool OutPosition = false;
    private float timeOutPosition = 0.0f;
    public float returnDelay = 10.0f;
    private Rigidbody rigid;

    void Start()
    {
        Position = transform.position;
        Rotation = transform.rotation;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position != Position)
        {
            if (!OutPosition)
            {
                OutPosition = true;
                timeOutPosition = 0.0f;
            }
            else
            {
                timeOutPosition += Time.deltaTime;

                if (timeOutPosition >= returnDelay)
                {
                    StartCoroutine(ReturnPositionCoroutine());
                }
            }
        }
        else
        {
            OutPosition = false;
            timeOutPosition = 0.0f;
        }
    }

    IEnumerator ReturnPositionCoroutine()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        transform.position = Position;
        transform.rotation = Rotation;

        OutPosition = false;
        timeOutPosition = 0.0f;

        yield return null;
    }
}