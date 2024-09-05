using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputManager : MonoBehaviour
{
    Vector2 leftThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
    Vector2 RightThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

    void Start()
    {

    }


    void Update()
    {
        ControllerInput();
    }

    private void ControllerInput()
    {
        //Get(), GetDown(), GetUp()
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("오른쪽 트리거 버튼");
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("오른쪽 그립 버튼");
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            Debug.Log("오른쪽 B 버튼");
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            Debug.Log("오른쪽 A 버튼");
        }

        if (leftThumbstick != Vector2.zero)
        {
            Debug.Log("오른쪽 조이패드");
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            Debug.Log("왼쪽 트리거 버튼");
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            Debug.Log("왼쪽 그립 버튼");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            Debug.Log("왼쪽 Y 버튼");
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            Debug.Log("왼쪽 X 버튼");
        }

        if (leftThumbstick != Vector2.zero)
        {
            Debug.Log("왼쪽 조이패드");
        }

    }
}
