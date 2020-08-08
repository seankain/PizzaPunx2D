using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrderInLayerStatic : MonoBehaviour
{
    public int orderAdjust = 0;
    SpriteRenderer ren;

    // Update is called once per frame
    void Update()
    {

#if (UNITY_EDITOR)
        if (ren != null)
        {
            ren.sortingOrder = (int)(transform.position.y * -10f) + orderAdjust;
        }
        else
        {
            ren = GetComponent<SpriteRenderer>();
        }
#endif

    }
}
