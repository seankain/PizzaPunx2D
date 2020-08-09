using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumingSocket : PlacementSocket
{
    public UnityPizzaEvent OnPizzaArrive;

    public override void ContentArrive(Holdable thingArriving)
    {
        if (OnContentArrive != null) OnContentArrive.Invoke();

        if (thingArriving != null)
        {
            var p = thingArriving.GetComponent<Pizza>();
            if (p != null)
            {
                OnPizzaArrive.Invoke(p);
            }
        }
    }
}