using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeCycle : MonoBehaviour
{
    public Text fadeText;
    public float MinAlpha = 0f;
    public float MaxAlpha = 1f;
    public float Speed = 0.0001f;

    private bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var nextColor = fadeText.color;
        if (fading)
        {
            nextColor.a -= Speed * Time.deltaTime;
           
            if(nextColor.a <= MinAlpha) { fading = false; }
        }
        else
        {
            nextColor.a += Speed * Time.deltaTime;
            if (nextColor.a >= MaxAlpha) { fading = true; }
        }
        fadeText.color = nextColor;
    }
}
