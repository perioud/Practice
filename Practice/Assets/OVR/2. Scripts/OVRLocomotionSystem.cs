using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRLocomotionSystem : MonoBehaviour
{
    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;
    public float speed = 2.0f;
    private Rigidbody rigid;
    public Transform player;
    public float rotationSpeed = 45.0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, leftController);

        Vector3 movement = new Vector3(thumbstick.x, 0, thumbstick.y) * speed;

        Move(movement);
        HandleRotation();
    }

    void Move(Vector3 movement)
    {
        rigid.velocity = new Vector3(movement.x, rigid.velocity.y, movement.z);
    }

    void HandleRotation()
    {
        float thumbstickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, rightController).x;

        if (Mathf.Abs(thumbstickX) > 0.1f)
        {
            float rotationAmount = thumbstickX * rotationSpeed * Time.deltaTime;
            player.Rotate(0, rotationAmount, 0);
        }
    }
}
