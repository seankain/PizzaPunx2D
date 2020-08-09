using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStage { early, mid, late }

    public OrderManager orderManager;
    public MoneyDisplay moneyDisplay;
    public PizzaSpriteManager pizzaSpriteManager;

    public List<PlacementSocket> PizzaSockets;
    public List<PlacementSocket> IngredientSockets;

    public float StageLengthSeconds = 360f;
    public float OrderCutoffTime = 330f;
    public float CurrentStageTime = 0f;

    private void Start()
    {
        if (orderManager == null) Debug.LogError("Game manager has no order manager!");
        if (moneyDisplay == null) Debug.LogError("Game mangaer has no money display!");
        if (pizzaSpriteManager == null) Debug.LogError("Game manager has no pizza sprite manager!");
    }

    private void Update()
    {
        CurrentStageTime += Time.deltaTime;
    }

    public bool TakingNewOrders()
    {
        return CurrentStageTime < OrderCutoffTime;
    }
}
