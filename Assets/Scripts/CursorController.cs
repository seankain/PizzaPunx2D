﻿using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    public Texture2D cursorTexture;
    [SerializeField]
    public Player player;
    [SerializeField]
    public UIRayTest rayTest;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rayTest.CursorOverReview()) { return; }
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //player.GoTo(new Vector3(pos.x,pos.y,0));
            
        }

    }
}
