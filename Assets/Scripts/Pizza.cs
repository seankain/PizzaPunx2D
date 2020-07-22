using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public Sprite RawDoughSprite;
    public Sprite SaucedSprite;
    public Sprite ToppedCheeseSprite;
    public Sprite ToppedVeggieSprite;
    public Sprite ToppedPepperoniSprite;
    public Sprite BakedCheeseSprite;
    public Sprite BakedVeggieSprite;
    public Sprite BakedPepperoniSprite;
    public Sprite BurnedSprite;

    private SpriteRenderer spriteRenderer;

    public void ProgressStage(int prepZoneStage)
    {
        if(prepZoneStage != stage)
        {
            //TODO: need a non burned ruined type pizza sprite for when player skips stages 
            stage = 4;
            spriteRenderer.sprite = BurnedSprite;
        }
        if (stage + 1 < 5)
        {
            stage += 1;
            switch (stage)
            {
                case 1:
                    Debug.Log("Sauced");
                    spriteRenderer.sprite = SaucedSprite;
                    break;
                case 2:
                    Debug.Log("Toppinged");
                    SetToppingSprite();
                    break;
                case 3:
                    Debug.Log("Baked");
                    //TODO: modify spritesheet to have more obvious baked sprites and animate steam separately
                    SetBakedSprite();
                    break;
                case 4:
                    Debug.Log("Burnt");
                    spriteRenderer.sprite = BurnedSprite;
                    break;
                default: break;
            }
        }
    }

    private void SetToppingSprite()
    {
        switch (pizzaType)
        {
            case 0:
                spriteRenderer.sprite = ToppedCheeseSprite;
                break;
            case 1:
                spriteRenderer.sprite = ToppedVeggieSprite;
                break;
            case 2:
                spriteRenderer.sprite = ToppedPepperoniSprite;
                break;
            default:
                break;

        }

    }

    private void SetBakedSprite() {

        switch (pizzaType)
        {
            case 0:
                spriteRenderer.sprite = BakedCheeseSprite;
                break;
            case 1:
                spriteRenderer.sprite = BakedVeggieSprite;
                break;
            case 2:
                spriteRenderer.sprite = BakedPepperoniSprite;
                break;
            default:
                break;

        }
    }

    /*
    RawDough=0,
    Sauced=1,
    Topped=2,
    Baked=3,
    Burnt=4   
    */
    private uint stage = 0;
    /*
     Cheese = 0,
     Veggie = 1,
     Pepperoni = 2
    */
    private uint pizzaType = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}

