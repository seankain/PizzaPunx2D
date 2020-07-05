using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    public GameObject FarBackground;
    public GameObject Background;
    public GameObject Foreground;

    public float FarBackgroundSpeedCoef = 0.1f;
    public float BackgroundSpeedCoef = 0.4f;
    public float ForegroundSpeedCoef = 1.0f;

    public float MaxPanDistance = 3f;
    public float MinPanDistance = -3f;

    public float Speed = 0.1f;

    private Vector3 Direction = Vector3.right;
    private float PanTravel = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PanTravel += Direction.x * (Speed * ForegroundSpeedCoef) * Time.deltaTime;
        if (PanTravel >= MaxPanDistance) { PanTravel = 0; Direction = Vector3.left; }
        else if(PanTravel <= MinPanDistance) { PanTravel = 0; Direction = Vector3.right; }
        Foreground.transform.position = Foreground.transform.position + (Direction * (Speed * ForegroundSpeedCoef)) * Time.deltaTime;
        Background.transform.position = Background.transform.position + (Direction * (Speed * BackgroundSpeedCoef)) * Time.deltaTime;
        FarBackground.transform.position = FarBackground.transform.position + (Direction * (Speed * FarBackgroundSpeedCoef)) * Time.deltaTime;
    }
}
