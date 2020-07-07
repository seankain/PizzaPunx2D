using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GripelyReview : MonoBehaviour
{ 
    [SerializeField]
    private Text ReviewTextRegion;
    [SerializeField]
    private Button ThankButton;
    [SerializeField]
    private Button PlacateButton;
    [SerializeField]
    private RectTransform VisibleLocation;
    [SerializeField]
    private RectTransform InvisibleLocation;
    [SerializeField]
    private float SlideSpeed;
    [SerializeField]
    private float ReviewsPerMinute;
    private RectTransform selfRect;
    private float slideDirection = -1;
    private bool isSliding = false;
    private float elapsed = 0;

    public void GenerateReview()
    {
        ReviewTextRegion.text = DebuggingReviewCorpus.GenerateReview();
    }

    public void Hide()
    {
        isSliding = true;
        slideDirection = -1;
    }
    public void Show() {
        GenerateReview();
        isSliding = true;
        slideDirection = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        selfRect = GetComponent<RectTransform>();
        ThankButton.onClick.AddListener(()=> { Debug.Log("thank clicked"); Hide(); });
        PlacateButton.onClick.AddListener(()=> { Debug.Log("placate clicked"); Hide(); });
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (!isSliding)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 60 / ReviewsPerMinute)
            {
                Show();
                elapsed = 0;
            }
            return;
        }
        if (slideDirection == -1)
        {
            if (selfRect.anchoredPosition.x > InvisibleLocation.anchoredPosition.x)
            {
                selfRect.anchoredPosition = new Vector2(selfRect.anchoredPosition.x - (SlideSpeed * Time.deltaTime), selfRect.anchoredPosition.y);
            }
            else
            {
                isSliding = false;
            }

        }
        else if (slideDirection == 1)
        {
            if (selfRect.anchoredPosition.x < VisibleLocation.anchoredPosition.x)
            {
                selfRect.anchoredPosition = new Vector2(selfRect.anchoredPosition.x + (SlideSpeed * Time.deltaTime), selfRect.anchoredPosition.y);
            }
            else
            {
                isSliding = false;
            }

        }
    }


}
