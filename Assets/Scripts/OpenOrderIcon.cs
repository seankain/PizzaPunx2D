using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOrderIcon : MonoBehaviour
{
    public SpriteRenderer pizzaSprite;
    public List<SpriteRenderer> ingredientSprites;
    public float xForce = -2f;
    PizzaOrder myOrder;
    GameManager gameManager;
    Rigidbody2D rb;

        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Missing Rigidbody2d");
    }

    GameManager myGameManager()
    {
        if (gameManager != null) return gameManager;

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null) Debug.LogError("Open order can't find game manager");

        return gameManager;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(xForce, 0, 0));
    }

    public void SetOrder(PizzaOrder order)
    {
        myOrder = order;
        pizzaSprite.sprite = myGameManager().pizzaSpriteManager.GetPizzaSprite(myOrder.ingredients, true);
        foreach (var s in ingredientSprites)
        {
            s.sprite = null;
        }

        var spriteIndex = 0;
        foreach (var i in myOrder.ingredients)
        {
            if (i != PizzaIngredient.PizzaInredientType.Cheese && i != PizzaIngredient.PizzaInredientType.Sauce)
            {
                ingredientSprites[spriteIndex].sprite = gameManager.ingredientSpriteManager.GetSprite(i);
                spriteIndex++;
            }
        }
    }
}
