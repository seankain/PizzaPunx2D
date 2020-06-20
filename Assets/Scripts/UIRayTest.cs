using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRayTest : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // Start is called before the first frame update
    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    public bool CursorOverReview()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "Review") { return true; }
            //Debug.Log("Hit " + result.gameObject.name);
        }
        return false;
    }
       
}
