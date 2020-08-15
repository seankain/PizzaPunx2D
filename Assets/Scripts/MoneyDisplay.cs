using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public Text targetTextbox;
    public string prefix = "";
    public int changeSpeed = 1;
    int currentlyDisplayedAmount = 0;
    int targetAmount = 0;

    public void SetMoneyTarget(int newAmount)
    {
        targetAmount = newAmount;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust the speed based on the amount things need to change
        var delta = Mathf.Min(changeSpeed, Mathf.Abs(targetAmount - currentlyDisplayedAmount));

        if (currentlyDisplayedAmount < targetAmount)
        {
            currentlyDisplayedAmount += delta;
            targetTextbox.text = $"${currentlyDisplayedAmount}";
        }

        if (currentlyDisplayedAmount > targetAmount)
        {
            currentlyDisplayedAmount -= delta;
            targetTextbox.text = $"{prefix}${currentlyDisplayedAmount}";
        }
    }
}
