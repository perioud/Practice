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
    private bool isFading = false;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two, controller) && !isFading)
        {
            if (!canvas.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(true);
            }
            StartCoroutine(FadeInOut());
        }
    }

    IEnumerator FadeInOut()
    {
        isFading = true;
        Color color = image.color;

        if (color.a <= 0.01f)
        {
            for (float i = 0.0f; i <= 1.0f; i += 0.01f)
            {
                color.a = i;
                image.color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }

        else if (color.a >= 0.99f)
        {
            for (float i = 1.0f; i >= 0.0f; i -= 0.01f)
            {
                color.a = i;
                image.color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }

        isFading = false;
    }
}
