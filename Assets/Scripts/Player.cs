using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float targetSpeed = 0.05f;
    [SerializeField]
    private float maxSpeed = 0.2f;
    [SerializeField]
    private float acceleration = 0.01f;
    private float elapsed = 0;
    [SerializeField]
    private float inputRateHz = 5;
    private Animator anim;
    private float speed = 0;
    private Vector2 direction = Vector2.zero;
    [SerializeField]
    private float interactDistance = 0.4f;
    private bool interacting = false;
    private GameObject currentHolding;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moveKeyDown = false;
        interacting = Input.GetKey(KeyCode.Space);
        direction.x = 0;
        direction.y = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("Direction", 2);
            direction.y += 1;
            moveKeyDown = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetInteger("Direction", 1);
            direction.x += 1;
            moveKeyDown = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetInteger("Direction", 3);
            direction.x -= 1;
            moveKeyDown = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetInteger("Direction", 0);
            direction.y -= 1;
            moveKeyDown = true;
        }

        if (moveKeyDown)
        {
            //if (speed < maxSpeed)
            //{
            //    speed += acceleration;
            //}
            speed = targetSpeed;
        }
        else
        {
            //if (speed > 0)
            //{
            //    speed -= acceleration;
            //}
            speed = 0;
        }
        anim.SetFloat("Speed", speed);
        this.transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
        if (interacting) { TestInteract(); }
    }

    private void TestInteract()
    {
        if (currentHolding != null)
        {
            //interactable that is holdable is released
        }
        var interactables = FindObjectsOfType<Interactable>();
        foreach (var interactable in interactables)
        {
            if(Vector3.Distance(interactable.transform.position,gameObject.transform.position) < 1f)
            {
                Debug.Log(interactable.name);
            }
        }
    }
}
