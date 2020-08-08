using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    public int penaltyForMissedReview;
    public int penaltyForWrongButton;

    public void ShowNewReview()
    {
        Debug.Log("Time to show a new review");

        // If an existing review is up, and ungraded big penalty

    }

    public void ThankPressed()
    {
        Debug.Log("Thank pressed, grade the review");
    }

    public void PlacatePressed()
    {
        Debug.LogError("Placate pressed, grade the review");
    }
}
