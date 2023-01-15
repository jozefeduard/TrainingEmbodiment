using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextRealBody : MonoBehaviour
{
    public Transform placeholder;    
    public Transform viewpoint;
    public TextMeshProUGUI distanceText;
    private float BodyDistance; 
    private float AvatarDistance;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;    
    public GameObject panelGoalSetter;
    public GameObject PlayerObject;
    public GameObject mainCamera;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        panelGoalSetter.SetActive(false);        
    }

    public float Calculus()
    {
        return Vector3.Distance(mainCamera.transform.position, PlayerObject.transform.position);
    }

    public void CalculateDistance()
    {
        float distance = Calculus();
        
        float AvatarDistance = 100 * (distance - 1);
        int AvatarD = Mathf.RoundToInt(AvatarDistance);
        
        float BodyDistance = 100 * (2 - distance);
        int BodyD = Mathf.RoundToInt(BodyDistance);

        Debug.Log("Calculating");       
        distanceText.text = "Your perceived embodiment is " + BodyD + "% biological and " + AvatarD + "% avatar.";
    }

    public void DelayedCalculation()
    {
        Invoke ("CalculateDistance", 22f);
    }

    // Update is called once per frame
    void Update()    
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        PlayerObject.transform.Translate(0, translation, 0);

        PlayerObject.transform.Rotate(0, 0, -rotation);   
    }
}
