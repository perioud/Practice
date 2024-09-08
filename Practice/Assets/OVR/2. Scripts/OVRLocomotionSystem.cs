//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OVRLocomotionSystem : MonoBehaviour
//{
//    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;
//    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;
//    public float speed = 2.0f;
//    private Rigidbody rigid;
//    public Transform player;
//    public float rotationSpeed = 45.0f;

//    void Start()
//    {
//        rigid = GetComponent<Rigidbody>();
//    }

//    void Update()
//    {
//        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, leftController);

//        Vector3 movement = (player.forward * thumbstick.y + player.right * thumbstick.x) * speed;

//        Move(movement);
//        HandleRotation();
//    }

//    void Move(Vector3 movement)
//    {
//        rigid.velocity = new Vector3(movement.x, rigid.velocity.y, movement.z);
//    }

//    void HandleRotation()
//    {
//        float thumbstickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, rightController).x;

//        if (Mathf.Abs(thumbstickX) > 0.1f)
//        {
//            float rotationAmount = thumbstickX * rotationSpeed * Time.deltaTime;
//            player.Rotate(0, rotationAmount, 0);
//        }
//    }
//}
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
    public LayerMask teleportLayerMask; // �ڷ���Ʈ ������ ���̾� ����
    public float teleportDistance = 10.0f; // �ڷ���Ʈ �ִ� �Ÿ�

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 leftThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, leftController);

        Vector3 movement = (player.forward * leftThumbstick.y + player.right * leftThumbstick.x) * speed;

        Move(movement);
        HandleRotation();
        HandleTeleport();
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

    void HandleTeleport()
    {
        float thumbstickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, rightController).y;

        if (thumbstickY > 0.8f)
        {
            Debug.Log("�ڷ���Ʈ �غ�");
            RaycastHit hit;

            Vector3 teleportDirection = player.forward;
            if (Physics.Raycast(player.position, teleportDirection, out hit, teleportDistance, teleportLayerMask))
            {
                player.position = hit.point;
            }
        }
    }
}
