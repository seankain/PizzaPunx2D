using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static ShowReview;

public class OrderManager : MonoBehaviour
{
    public float basePizzaFee = 100;
    public float perToppingFee = 10;
    public float penaltyForMissedReview = 0.5f;
    public float penaltyForWrongButton = 0.75f;
    public float rewardForRightButton = 1.1f;
    public float rewardForGoodReview = 1.05f;

    public float timeBetweenOrders = 8f;
    public float orderRandomness = 2f;
    public int maxOpenOrders = 4;
    public int maxToppings = 4; // does not include sauce and cheese

    float timeUntilNextOrder = 0f;

    public UnityIntEvent OnOrderComplete;
    public UnityIntEvent OnNewOrder;

    GameManager gameManager;

    List<PizzaOrder> allOrders;

    int pizzaOrderCounter = 0;

    private void Update()
    {
        timeUntilNextOrder -= Time.deltaTime;

        if (timeUntilNextOrder < 0 && gameManager.TakingNewOrders() && NumOpenOrders() < maxOpenOrders)
        {
            CreateOrder();
        }

        // Add time to each open order in parallel
        var t = Time.deltaTime; // Time.deltaTime can only be called from the main thread
        allOrders.Where(a => a.isOrderComplete == false).AsParallel().ForAll(a => a.AddWaitTime(t));
    }

    public PizzaIngredient.PizzaInredientType RandomTopping()
    {
        var values = System.Enum.GetValues(typeof(PizzaIngredient.PizzaInredientType));
        var topping = (PizzaIngredient.PizzaInredientType)values.GetValue(Random.Range(0, values.Length));

        while (topping == PizzaIngredient.PizzaInredientType.Cheese || topping == PizzaIngredient.PizzaInredientType.Sauce)
        {
            topping = (PizzaIngredient.PizzaInredientType)values.GetValue(Random.Range(0, values.Length));
        }

        return topping;
    }

    public void CreateOrder()
    {
        timeUntilNextOrder = timeBetweenOrders + Random.Range(-orderRandomness, orderRandomness);

        var latestOrder = new PizzaOrder() { orderId = pizzaOrderCounter, ingredients = new List<PizzaIngredient.PizzaInredientType>() { PizzaIngredient.PizzaInredientType.Cheese, PizzaIngredient.PizzaInredientType.Sauce } };
        var numToppings = Random.Range(0, gameManager.MaxToppingsThisStage() + 1); // max is exclusive
        for (var i = 0; i < numToppings; i++)
        {
            latestOrder.ingredients.Add(RandomTopping());
        }
        allOrders.Add(latestOrder);
        pizzaOrderCounter++;

        if (OnNewOrder != null) OnNewOrder.Invoke(latestOrder.orderId);
    }

    public void OrderComplete(Pizza p)
    {
        var ingredients = p.GetToppings();
        var oo = FindOpenOrder(ingredients);

        if (oo != null)
        {
            oo.isOrderComplete = true;
            if (OnOrderComplete != null) OnOrderComplete.Invoke(oo.orderId);
        }
        else
        {
            Debug.LogError("Couldn't find that open order!");
        }
    }

    public int NumOpenOrders()
    {
        return allOrders.Where(a => a.isOrderComplete == false).Count();
    }

    public PizzaOrder FindOpenOrder(List<PizzaIngredient.PizzaInredientType> ingredients)
    {
        return allOrders.Where(a => a.Match(ingredients) && a.isOrderComplete == false).FirstOrDefault();
    }

    public PizzaOrder FindOrderById(int id)
    {
        return allOrders.Where(a => a.orderId == id).FirstOrDefault();
    }

    public void PizzaReviewed(int orderId, bool wasGoodReview, ReviewGrade grade)
    {
        var money = basePizzaFee;

        switch (grade)
        {
            case ReviewGrade.correct:
                money *= rewardForRightButton;
                break;

            case ReviewGrade.incorrect:
                money *= penaltyForWrongButton;
                break;

            case ReviewGrade.missed:
                money *= penaltyForMissedReview;
                break;
        }
        if (wasGoodReview == true) money *= rewardForGoodReview;

        var o = FindOrderById(orderId);
        if (o != null)
        {
            o.moneyEarned = (int)money;
            gameManager.moneyDisplay.addToFunds(o.moneyEarned);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        allOrders = new List<PizzaOrder>();
    }
}
