using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LootLocker.Requests;

public class DistanceToViepoint : MonoBehaviour
{
    public Leaderboard leaderboard;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject viewpoint;
    public TextMeshProUGUI BodyDistance;
    float BDistance;
    int score;
    public GameObject PanelLeaderboard;
    public int leaderboardID;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    public GameObject PlayerObject;
    public GameObject mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }
    
    public float Calculus()
    {
        return Vector3.Distance(mainCamera.transform.position, PlayerObject.transform.position);
    }
    
    void CalculateDistance()
    {
        if(viewpoint)
        {
            float distance = Calculus();

            float BDistance = 100 * (2 - distance);                   
            int scoreToUpload = Mathf.RoundToInt(BDistance);
            
            Debug.Log(scoreToUpload);
            leaderboardID = 10105;
            StartCoroutine(SubmitScoreRoutine(scoreToUpload));
            StartCoroutine(FetchTopHighscoresRoutine());
        }
    }

    // Update is called once per frame
    void Update()
    
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, translation, 0);

        transform.Rotate(0, 0, -rotation);        
    }

    public void Calculating()
    {
        Debug.Log("Calculating");
        CalculateDistance();
        PanelLeaderboard.SetActive(true);
        Destroy(BodyDistance, 5);
    }

    public void DelayCalculating()
    {
        Invoke("Calculating", 22f);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if(response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        leaderboardID = 10105;
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboardID, 10, 0, (response) =>
        {
            if(response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if(members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
