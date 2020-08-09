using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public Text targetTextbox;
    int currentlyDisplayedAmount = 0;
    int targetAmount = 0;

    public void addToFunds(int newAmount)
    {
        targetAmount += newAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyDisplayedAmount < targetAmount)
        {
            currentlyDisplayedAmount++;
            targetTextbox.text = $"${currentlyDisplayedAmount}";
        }

        if (currentlyDisplayedAmount > targetAmount)
        {
            currentlyDisplayedAmount--;
            targetTextbox.text = $"${currentlyDisplayedAmount}";
        }
    }
}
