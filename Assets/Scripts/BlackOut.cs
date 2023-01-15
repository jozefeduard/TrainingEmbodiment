using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackOut : MonoBehaviour
{
    public GameObject blackOutScreen;
    public GameObject MovePanel;
    public GameObject ReturnPanel; 
    public GameObject GoalSetterPanel;

    void Start()
    {
        ReturnPanel.SetActive(false);
        MovePanel.SetActive(false);
    }

    public void GoToBlack()
    {
        StartCoroutine(FadeBlackOutSquare());        
    }

    public void Move()
    {
        MovePanel.SetActive(true);
    }

    public void CancelMove()
    {
        MovePanel.SetActive(false);
    }

    public void Return()
    {
        ReturnPanel.SetActive(true);
    }

    public void CancelReturn()
    {
        ReturnPanel.SetActive(false);
    }

    public void GoalSetting()
    {
        GoalSetterPanel.SetActive(true);
    }
                
    public void DelayedStart()
    {
        Invoke("GoToBlack", 5f);
        Invoke("Move", 7f);
        Invoke("CancelMove", 10f);
        Invoke("Return", 11f);
        Invoke("CancelReturn", 21f);
        Invoke("GoalSetting", 22f);
    }   

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, float fadeSpeed = 1f )
    {
        Color objectColor = blackOutScreen.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutScreen.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutScreen.GetComponent<Image>().color = objectColor;
                yield return null;               
            }
        }
        else
        {
            while (blackOutScreen.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutScreen.GetComponent<Image>().color = objectColor;
                yield return null;               
            }
        }        
    }
}