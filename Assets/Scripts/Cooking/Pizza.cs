using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public Sprite RawDoughSprite;

    public Sprite SaucedSprite;
    public Sprite BurnedSprite;
    public Sprite ToppedCheeseSprite;
    public Sprite ToppedVeggieSprite;
    public Sprite ToppedPepperoniSprite;
    public Sprite ToppedComboSprite;
    public Sprite ToppedWrongSprite;

    public Sprite BakedDoughSprite;
    public Sprite BakedSauceSprite;
    public Sprite BakedCheeseSprite;
    public Sprite BakedVeggieSprite;
    public Sprite BakedPepperoniSprite;
    public Sprite BakedComboSprite;
    public Sprite BakedWrongSprite;

    public ParticleSystem Cookedparticles;
    public ParticleSystem BurntParticles;

    private SpriteRenderer spriteRenderer;

    private List<PizzaIngredient.PizzaInredientType> AddedIngredients = new List<PizzaIngredient.PizzaInredientType>();

    public enum PizzaStage { dough, sauced, toppinged, baked, burnt }
    public enum PizzaType { dough, sauced, cheese, veggie, meat, combo, wrong }

    float cookTime = 0f;
    float neededCookTime = 10f;
    float burnCookTime = 14f;

    public PizzaStage stage = PizzaStage.dough;

    public int GetNumToppings()
    {
        return AddedIngredients.Count - 2;
    }

    protected PizzaType getPizzaType()
    {
        bool hasCheese = false;
        bool hasVeggies = false;
        bool hasMeat = false;
        bool hasSauce = false;

        if (AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Broccoli)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.ChiliPepper)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Mushroom)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Onion)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Pepper)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Tomato))
        {
            hasVeggies = true;
        }

        if (AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Sauce))
        {
            hasSauce = true;
        }

        if (AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Cheese))
        {
            hasCheese = true;
        }

        if (AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Pepperoni)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Sardine)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.Shrimp)
            || AddedIngredients.Contains(PizzaIngredient.PizzaInredientType.SpecialMeat)
            )
        {
            hasMeat = true;
        }

        if (AddedIngredients.Count == 0) return PizzaType.dough;
        else
        {
            if (hasSauce == true)
            {
                if (hasVeggies && hasMeat) return PizzaType.combo;
                if (hasVeggies) return PizzaType.veggie;
                if (hasMeat) return PizzaType.meat;
                if (hasCheese) return PizzaType.cheese;
                return PizzaType.sauced;
            }
            else
            {
                return PizzaType.wrong;
            }
        }
    }

    public void AddCookTime(float t)
    {
        cookTime += t;
        if (cookTime > neededCookTime) SetBaked();
        if (cookTime > burnCookTime) SetBurnt();
    }

    void SetToppinged()
    {
        stage = PizzaStage.toppinged;
        SetToppingSprite();
    }

    void SetBaked()
    {
        stage = PizzaStage.baked;
        SetBakedSprite();
        var e = Cookedparticles.emission;
        e.enabled = true;
    }

    void SetBurnt()
    {
        stage = PizzaStage.burnt;
        spriteRenderer.sprite = BurnedSprite;

        // turn off cooked particles
        var e = Cookedparticles.emission;
        e.enabled = false;

        // enable burnt particles
        var e2 = BurntParticles.emission;
        e2.enabled = true;
    }

    public void AddIngredient(PizzaIngredient.PizzaInredientType newIngredient)
    {
        if (getPizzaType() != PizzaType.wrong)
        {
            AddedIngredients.Add(newIngredient);
            SetToppinged();
        }
    }

    private void SetToppingSprite()
    {
        switch (getPizzaType())
        {
            case PizzaType.dough:
                spriteRenderer.sprite = RawDoughSprite;
                break;

            case PizzaType.sauced:
                spriteRenderer.sprite = SaucedSprite;
                break;

            case PizzaType.cheese:
                spriteRenderer.sprite = ToppedCheeseSprite;
                break;

            case PizzaType.veggie:
                spriteRenderer.sprite = ToppedVeggieSprite;
                break;

            case PizzaType.meat:
                spriteRenderer.sprite = ToppedPepperoniSprite;
                break;

            case PizzaType.combo:
                spriteRenderer.sprite = ToppedComboSprite;
                break;

            case PizzaType.wrong:
                spriteRenderer.sprite = ToppedWrongSprite;
                break;
        }
        
    }

    private void SetBakedSprite() {

        switch (getPizzaType())
        {
            case PizzaType.dough:
                spriteRenderer.sprite = BakedDoughSprite;
                break;

            case PizzaType.sauced:
                spriteRenderer.sprite = BakedSauceSprite;
                break;

            case PizzaType.cheese:
                spriteRenderer.sprite = BakedCheeseSprite;
                break;

            case PizzaType.veggie:
                spriteRenderer.sprite = BakedVeggieSprite;
                break;

            case PizzaType.meat:
                spriteRenderer.sprite = BakedPepperoniSprite;
                break;

            case PizzaType.combo:
                spriteRenderer.sprite = BakedComboSprite;
                break;

            case PizzaType.wrong:
                spriteRenderer.sprite = BakedWrongSprite;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}

