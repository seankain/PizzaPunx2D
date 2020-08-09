using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDeactivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var ren = GetComponentInChildren<SpriteRenderer>();
        if (ren) ren.enabled = false;
    }
}
