using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    public Leaderboard leaderboard;
    public TMP_InputField playerNameInputfield;
    public TextMeshProUGUI NameSet;
    public GameObject panel;
    public GameObject panelFirstTry;
    public GameObject panelGoalSetter;
    public GameObject panelLeaderboard;
    public GameObject blackoutScreen;
    public GameObject blackoutSquare;
    public GameObject panelStartTry;
    public GameObject MovePanel;
    public GameObject ReturnPanel;
    public GameObject TrainPanel;
    public GameObject MovePanelTwo;
    public GameObject ReturnPanelTwo;
    
    // Start is called before the first frame update
    void Start()
    {
        blackoutSquare.SetActive(false);
        StartCoroutine(SetupRoutine());
        panel.SetActive(true);
        panelFirstTry.SetActive(false);
        panelGoalSetter.SetActive(false);
        panelLeaderboard.SetActive(false);
        panelStartTry.SetActive(false);
        MovePanel.SetActive(false);
        ReturnPanel.SetActive(false);
        MovePanelTwo.SetActive(false);
        ReturnPanelTwo.SetActive(false);
    }

    public void PanelSetNameDisappears()
    {
        panel.SetActive(false);
        blackoutScreen.SetActive(true);
        panelStartTry.SetActive(true);
    }

    public void CancelTrainPanel()
    {
        TrainPanel.SetActive(false);
        panelGoalSetter.SetActive(true);
    }

    public void StartTryDisappears()
    {
        panelStartTry.SetActive(false);
    }

    public void Retry()
    {
        panelLeaderboard.SetActive(false);
        panelGoalSetter.SetActive(true);
    }

    public void CancelGoalPanel()
    {        
        panelGoalSetter.SetActive(false);
    }

    public void SetPlayerName()
    {             
        LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
            if(response.success)
            {               
                Debug.Log("Succesfully set player name");                
                NameSet.text = "welcome " + playerNameInputfield.text;               
                Invoke("PanelSetNameDisappears", 3);
            }
            else
            {
                Debug.Log("Could not set player name"+response.Error);
            }
        });        
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if(response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
