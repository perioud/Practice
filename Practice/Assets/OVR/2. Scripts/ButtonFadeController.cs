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
    private bool isFading = false; // 페이드 동작 중인지 확인하는 플래그

    void Update()
    {
        // OVRInput 버튼을 눌렀을 때 페이드 인/아웃을 시작
        if (OVRInput.GetDown(OVRInput.Button.Two, controller) && !isFading)
        {
            if (!canvas.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(true);
            }
            StartCoroutine(FadeInOut());
        }
    }

    // 페이드 인 후 페이드 아웃하는 코루틴
    IEnumerator FadeInOut()
    {
        isFading = true; // 페이드 시작 플래그 설정
        Color color = image.color;

        // 페이드 인 (알파 0.0에서 1.0으로 증가)
        if (color.a <= 0.01f) // 알파 값이 거의 0일 때
        {
            for (float i = 0.0f; i <= 1.0f; i += 0.01f)
            {
                color.a = i;
                image.color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }

        // 페이드 아웃 (알파 1.0에서 0.0으로 감소)
        else if (color.a >= 0.99f) // 알파 값이 거의 1일 때
        {
            for (float i = 1.0f; i >= 0.0f; i -= 0.01f)
            {
                color.a = i;
                image.color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }

        isFading = false; // 페이드 종료 플래그 설정
    }
}
