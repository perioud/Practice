using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputController : MonoBehaviour
{
    public OVRInput.Controller controller;

    public Transform player;

    private Transform ControllerTransform = null;
    private Rigidbody ControllerRigidbody = null;

    //충돌체의 Rigidbody를 저장하는 변수
    private Rigidbody attachedObject = null;

    //충돌한 충돌체들의 Rigidbody를 저장하는 변수
    private List<Rigidbody> contactRigidbodies = new List<Rigidbody>();

    void Start()
    {
        ControllerTransform = GetComponent<Transform>();
        ControllerRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            ObjectPickup();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            ObjectDrop();
        }
    }

    public void ObjectPickup()
    {
        attachedObject = GetNearestRigidbody();

        if (attachedObject == null)
            return;

        //중력 비활성화
        attachedObject.useGravity = false;

        //물리효과 비활성화
        attachedObject.isKinematic = true;

        attachedObject.transform.parent = ControllerTransform; //충돌한 오브젝트의 부모 오브젝트를 컨트롤러로 변경
        attachedObject.transform.position = ControllerTransform.position;
        attachedObject.transform.rotation = ControllerTransform.rotation;
    }

    public void ObjectDrop()
    {
        if (attachedObject == null)
            return;

        //중력 활성화
        attachedObject.useGravity = true;

        //물리효과 활성화
        attachedObject.isKinematic = false;

        attachedObject.transform.parent = null; //충돌한 오브젝트의 부모 오브젝트를 윌드로 변경

        //오브젝트에 물리 에너지를 전해주는 기능
        attachedObject.velocity += player.rotation * OVRInput.GetLocalControllerVelocity(controller);
        attachedObject.angularVelocity += player.rotation * OVRInput.GetLocalControllerAngularVelocity(controller); //회전값

        attachedObject = null;
    }

    //컨트롤러에 충돌한 오브젝트 중 가장 가까운 거리에 있는 오브젝트의 Rigidbody를 저장하는 기능
    private Rigidbody GetNearestRigidbody()
    {
        Rigidbody nearestRigidbody = null;

        //충돌체 사이의 최소 거리값을 저장하는 변수
        float minDistance = float.MaxValue;

        //현재 거리값을 저장하는 변수
        float distance = 0;

        foreach(Rigidbody rigidbody in contactRigidbodies)
        {
            //List에 저장된 Rigidbody 중 가까운 Rigidbody를 계산
            //Rigidbody 충돌체의 위치와 컨트롤러 사이의 거리를 계산
            distance = Vector3.Distance(rigidbody.transform.position, ControllerTransform.position);

            //거리값 비교
            if(distance < minDistance)
            {
                //최소 거리값 갱신
                minDistance = distance;

                nearestRigidbody = rigidbody;
            }
        }
        return nearestRigidbody;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractionObject"))
        {
            Outline script = other.GetComponent<Outline>();
            script.enabled = true;
            contactRigidbodies.Add(other.gameObject.GetComponent<Rigidbody>());
        }

        //attachedObject = other.gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractionObject"))
        {
            Outline script = other.GetComponent<Outline>();
            script.enabled = false;
            contactRigidbodies.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}