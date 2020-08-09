using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class IngredientMax
{
    public GameManager.GameStage stage;
    public int maxIngredients;
}

public class GameManager : MonoBehaviour
{
    public enum GameStage { early, mid, late }

    public OrderManager orderManager;
    public MoneyDisplay moneyDisplay;
    public PizzaSpriteManager pizzaSpriteManager;
    public IngredientSpriteManager ingredientSpriteManager;

    public List<PlacementSocket> PizzaSockets;
    public List<PlacementSocket> IngredientSockets;

    public float StageLengthSeconds = 360f;
    public float OrderCutoffTime = 330f;
    public float CurrentStageTime = 0f;

    public GameStage currentGameStage = GameStage.early;

    public List<IngredientMax> IngredientsPerStage;

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

    public int MaxToppingsThisStage()
    {
        return IngredientsPerStage.Where(a => a.stage == currentGameStage).First().maxIngredients;
    }
}
