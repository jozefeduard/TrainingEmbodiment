using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Leave : MonoBehaviour
{
    public GameObject PanelLeaderboard;
    public GameObject PanelNameSetter;
    public GameObject placeholder;
    public GameObject viewpoint;
    public GameObject blackoutScreen;
    public GameObject blackoutSquare;
    public TMP_InputField playerNameInputfield;
    public TextMeshProUGUI NameSet;

    public void ReturnToBeginning()
    {
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
        Color objectColor = blackoutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)      
        {
            while (blackoutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackoutSquare.GetComponent<Image>().color = objectColor;
                yield return null;

                PanelLeaderboard.SetActive(false);
                PanelNameSetter.SetActive(true);
                playerNameInputfield.text = "";
                NameSet.text = "";
                placeholder.SetActive(true);
                viewpoint.SetActive(true);
                blackoutScreen.SetActive(true);
                blackoutSquare.SetActive(false);
            }
        }
        
    }
}
