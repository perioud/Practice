//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ButtonFadeController : MonoBehaviour
//{
//    public OVRInput.Controller controller;
//    public Canvas canvas;
//    public Image image;

//    void Update()
//    {
//        if (OVRInput.GetDown(OVRInput.Button.Two, controller))
//        {
//            if (!canvas.gameObject.activeSelf)
//            {
//                canvas.gameObject.SetActive(true);
//            }
//            StartCoroutine(FadeIn());
//        }
//    }

//    IEnumerator FadeIn()
//    {
//        Color color = image.color;
//        if (color.a == 0.0f)
//        {
//            for (float i = 0.0f; i <= 1.0f; i += 0.01f)
//            {
//                color.a = i;
//                image.color = color;

//                yield return new WaitForSeconds(0.01f);
//            }
//        }
//        if (color.a == 1.0f)
//        {
//            for (float i = 1.0f; i >= 0.0f; i -= 0.01f)
//            {
//                color.a = i;
//                image.color = color;

//                yield return new WaitForSeconds(0.01f);
//            }
//        }
//    }
//}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFadeController : MonoBehaviour
{
    public OVRInput.Controller controller;
    public Canvas canvas;
    public Image image;
    private bool isFading = false; // ���̵� ���� ������ Ȯ���ϴ� �÷���

    void Update()
    {
        // OVRInput ��ư�� ������ �� ���̵� ��/�ƿ��� ����
        if (OVRInput.GetDown(OVRInput.Button.Two, controller) && !isFading)
        {
            if (!canvas.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(true);
            }
            StartCoroutine(FadeInOut());
        }
    }

    // ���̵� �� �� ���̵� �ƿ��ϴ� �ڷ�ƾ
    IEnumerator FadeInOut()
    {
        isFading = true; // ���̵� ���� �÷��� ����
        Color color = image.color;

        // ���̵� �� (���� 0.0���� 1.0���� ����)
        if (color.a <= 0.01f) // ���� ���� ���� 0�� ��
        {
            for (float i = 0.0f; i <= 1.0f; i += 0.01f)
            {
                color.a = i;
                image.color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }

        // ���̵� �ƿ� (���� 1.0���� 0.0���� ����)
        else if (color.a >= 0.99f) // ���� ���� ���� 1�� ��
        {
            for (float i = 1.0f; i >= 0.0f; i -= 0.01f)
            {
                color.a = i;
                image.color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }

        isFading = false; // ���̵� ���� �÷��� ����
    }
}
