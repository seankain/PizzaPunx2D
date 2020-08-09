using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ShowReview;

public class OrderManager : MonoBehaviour
{
    public GameManager gameManager;

    public float basePizzaFee = 100;
    public float perToppingFee = 10;
    public float penaltyForMissedReview = 0.5f;
    public float penaltyForWrongButton = 0.75f;
    public float rewardForRightButton = 1.1f;
    public float rewardForGoodReview = 1.05f;

    public UnityIntEvent OnOrderComplete;

    List<PizzaOrder> allOrders;

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

    public PizzaOrder FindOpenOrder(List<PizzaIngredient.PizzaInredientType> ingredients)
    {
        return allOrders.Where(a => a.Match(ingredients) && a.isOrderComplete == false).FirstOrDefault();
    }

    PizzaOrder FindOrderById(int id)
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
        allOrders = new List<PizzaOrder>();

        allOrders.Add(new PizzaOrder() { orderId = 0, ingredients = new List<PizzaIngredient.PizzaInredientType>() { PizzaIngredient.PizzaInredientType.Cheese, PizzaIngredient.PizzaInredientType.Sauce } });
    }
}
