using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrderInLayer : MonoBehaviour
{
    public int orderAdjust = 0;
    SpriteRenderer ren;

    // Update is called once per frame
    void Update()
    {
        if (ren != null)
        {
            ren.sortingOrder = (int)(transform.position.y * -10f) + orderAdjust;
        }
        else
        {
            ren = GetComponent<SpriteRenderer>();
        }
    }
}
