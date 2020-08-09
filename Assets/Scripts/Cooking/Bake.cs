using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bake : MonoBehaviour
{
    PlacementSocket SocketToCook;

    bool isCooking = false;

    // Start is called before the first frame update
    void Start()
    {
        SocketToCook = GetComponent<PlacementSocket>();
        if (SocketToCook == null)
        {
            Debug.LogError("Socket missing?");
        }
    }

    public void StartCooking()
    {
        isCooking = true;
    }

    public void StopCooking()
    {
        isCooking = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isCooking)
        {
            if (SocketToCook != null && SocketToCook.OccupiedBy != null)
            {
                var ps = SocketToCook.OccupiedBy.GetComponent<Pizza>();
                ps.AddCookTime(Time.deltaTime);
            }
            else
            {
                Debug.LogError("Can't cook");
            }
                
        }

    }
}
