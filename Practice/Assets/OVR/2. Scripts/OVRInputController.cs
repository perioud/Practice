using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputController : MonoBehaviour
{
    public OVRInput.Controller controller;

    public Transform player;

    private Transform ControllerTransform = null;
    private Rigidbody ControllerRigidbody = null;

    //�浹ü�� Rigidbody�� �����ϴ� ����
    private Rigidbody attachedObject = null;

    //�浹�� �浹ü���� Rigidbody�� �����ϴ� ����
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

        //�߷� ��Ȱ��ȭ
        attachedObject.useGravity = false;

        //����ȿ�� ��Ȱ��ȭ
        attachedObject.isKinematic = true;

        attachedObject.transform.parent = ControllerTransform; //�浹�� ������Ʈ�� �θ� ������Ʈ�� ��Ʈ�ѷ��� ����
        attachedObject.transform.position = ControllerTransform.position;
        attachedObject.transform.rotation = ControllerTransform.rotation;
    }

    public void ObjectDrop()
    {
        if (attachedObject == null)
            return;

        //�߷� Ȱ��ȭ
        attachedObject.useGravity = true;

        //����ȿ�� Ȱ��ȭ
        attachedObject.isKinematic = false;

        attachedObject.transform.parent = null; //�浹�� ������Ʈ�� �θ� ������Ʈ�� ����� ����

        //������Ʈ�� ���� �������� �����ִ� ���
        attachedObject.velocity += player.rotation * OVRInput.GetLocalControllerVelocity(controller);
        attachedObject.angularVelocity += player.rotation * OVRInput.GetLocalControllerAngularVelocity(controller); //ȸ����

        attachedObject = null;
    }

    //��Ʈ�ѷ��� �浹�� ������Ʈ �� ���� ����� �Ÿ��� �ִ� ������Ʈ�� Rigidbody�� �����ϴ� ���
    private Rigidbody GetNearestRigidbody()
    {
        Rigidbody nearestRigidbody = null;

        //�浹ü ������ �ּ� �Ÿ����� �����ϴ� ����
        float minDistance = float.MaxValue;

        //���� �Ÿ����� �����ϴ� ����
        float distance = 0;

        foreach(Rigidbody rigidbody in contactRigidbodies)
        {
            //List�� ����� Rigidbody �� ����� Rigidbody�� ���
            //Rigidbody �浹ü�� ��ġ�� ��Ʈ�ѷ� ������ �Ÿ��� ���
            distance = Vector3.Distance(rigidbody.transform.position, ControllerTransform.position);

            //�Ÿ��� ��
            if(distance < minDistance)
            {
                //�ּ� �Ÿ��� ����
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