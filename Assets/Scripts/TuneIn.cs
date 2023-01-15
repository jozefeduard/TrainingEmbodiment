using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TuneIn : MonoBehaviour
{
    public GameObject viewpoint;
    public GameObject PanelGoalSetter; 
    public GameObject blackoutScreen;
    public GameObject blackoutSquare; 
    
    public void TuningIn()
    {        
        blackoutSquare.SetActive(true);
        StartCoroutine(FadeBlackOutSquare());        
    }

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, float fadeSpeed = 50f )
    {
        Color objectColor = blackoutScreen.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)        
        {
            while (blackoutScreen.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackoutScreen.GetComponent<Image>().color = objectColor;
                yield return null;   
                
                blackoutScreen.SetActive(false);                
                PanelGoalSetter.SetActive(false);
                viewpoint.SetActive(false);
            }
        }        
    }
}
