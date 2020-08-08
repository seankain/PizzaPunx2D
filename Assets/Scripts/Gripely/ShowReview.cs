using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowReview : MonoBehaviour
{
    public enum ReviewStatus { revealing, showing, closing, closed };
    public enum ReviewGrade { correct, incorrect, missed };

    public string InitialText;

    public Text textToAnimate;
    public float animationDuration = 1f;
    public float closedHeight = 0.5f;
    public float openHeight = 116f;
    public float closedPosition = 2.9f;
    public float openPosition = 0f;

    public float basePizzaFee = 100;
    public float perToppingFee = 10;
    public float penaltyForMissedReview = 0.5f;
    public float penaltyForWrongButton = 0.75f;
    public float rewardForRightButton = 1.1f;
    public float rewardForGoodReview = 1.05f;

    ReviewStatus currentStatus;
    ReviewData currentReview;
    ReviewData nextReview;

    public void Thank()
    {
        switch (currentStatus)
        {
            // One is still coming in, do nothing
            case ReviewStatus.closed:
            case ReviewStatus.closing:
            case ReviewStatus.revealing:
                break;

            // Player missed it. Close up
            case ReviewStatus.showing:
                CloseReview(currentReview.isGoodReview, true);
                break;
        }
    }

    public void Placate()
    {
        switch (currentStatus)
        {
            // One is still coming in, do nothing
            case ReviewStatus.closed:
            case ReviewStatus.closing:
            case ReviewStatus.revealing:
                break;

            // Player missed it. Close up
            case ReviewStatus.showing:
                CloseReview(currentReview.isGoodReview, false);
                break;
        }
    }

    private void Awake()
    {
        currentStatus = ReviewStatus.closed;
    }

    private void Start()
    {
        DisplayReview(-1, InitialText, true);
    }

    public void DisplayNewReview()
    {
        DisplayReview(0, DebuggingReviewCorpus.GenerateReview(), true);
    }

    public void DisplayReview(int id, string text, bool isGood)
    {
        switch (currentStatus)
        {
            // Display the review
            case ReviewStatus.closed:
            case ReviewStatus.closing:
                nextReview = new ReviewData() { isGoodReview = isGood, reviewId = id, reviewText = text };
                break;

            // One is still coming in, do nothing
            case ReviewStatus.revealing:
                break;

            // Player missed it. Close up
            case ReviewStatus.showing:
                CloseReview(currentReview.isGoodReview, null);
                nextReview = new ReviewData() { isGoodReview = isGood, reviewId = id, reviewText = text };
                break;
        }
    }

    private void AnimateOpening()
    {
        if (currentStatus == ReviewStatus.closed)
        {
            currentReview = nextReview;
            nextReview = null;
            StartCoroutine(AnimateOpeningCo(currentReview.reviewText));
        }
    }

    private void AnimateClosing()
    {
        if (currentStatus == ReviewStatus.showing)
        {
            StartCoroutine(AnimateClosingCo());
        }
    }

    IEnumerator AnimateOpeningCo(string content)
    {
        currentStatus = ReviewStatus.revealing;
        var elapsed = 0f;
        var rt = textToAnimate.GetComponent<RectTransform>();
        textToAnimate.text = content;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Lerp(closedHeight, openHeight, elapsed / animationDuration));
            rt.position = new Vector3(rt.position.x, Mathf.Lerp(closedPosition, openPosition, elapsed / animationDuration));
            
            yield return null;
        }
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, openHeight);

        currentStatus = ReviewStatus.showing;
    }

    IEnumerator AnimateClosingCo()
    {
        currentStatus = ReviewStatus.closing;

        var elapsed = 0f;
        var rt = textToAnimate.GetComponent<RectTransform>();

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Lerp(openHeight, closedHeight, elapsed / animationDuration));
            rt.position = new Vector3(rt.position.x, Mathf.Lerp(openPosition, closedPosition, elapsed / animationDuration));

            yield return null;
        }
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, closedHeight);

        currentStatus = ReviewStatus.closed;
    }

    public ReviewGrade CloseReview(bool groundTruth, bool? grade)
    {
        AnimateClosing();

        if (grade.HasValue == false) return ReviewGrade.missed;

        if (groundTruth == grade.Value) return ReviewGrade.correct;

        return ReviewGrade.incorrect;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStatus == ReviewStatus.closed && nextReview != null )
        {
            AnimateOpening();
        }
    }
}
