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
    private Holdable currentHolding;
    [SerializeField]
    private Transform holdLocation;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moveKeyDown = false;
        interacting = Input.GetKeyDown(KeyCode.Space);
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
            speed = targetSpeed;
        }
        else
        {
            anim.SetInteger("Direction", -1); // Goin' nowhere
            speed = 0;
        }

        anim.SetFloat("Speed", speed);
        this.transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
        if (interacting) { TestInteract(); }
        if(currentHolding != null)
        {
            var holdPosition = holdLocation.position;
            holdPosition.x += direction.x * 0.2f;
            holdPosition.y += direction.y * 0.2f;
            currentHolding.transform.position = holdPosition; 
        }
    }

    private void TestInteract()
    {
        if (currentHolding != null)
        {
            //interactable that is holdable is released
            currentHolding.Drop();
            currentHolding = null;
            return;
        }
        var interactables = FindObjectsOfType<Interactable>();

        Interactable closestItem = null;
        var closestDist = float.MaxValue;

        foreach (var interactable in interactables)
        {
            var dist = Vector3.Distance(interactable.transform.position, gameObject.transform.position);
            if (dist < interactDistance && dist < closestDist)
            {
                closestItem = interactable;
                closestDist = dist;
            }
        }

        if (closestItem != null)
        {
            //Can it be held?
            var holdable = closestItem.GetComponent<Holdable>();
            if (holdable != null)
            {
                currentHolding = holdable;
                currentHolding.Pickup();
            }

            //Todo otherwise interact
        }
    }
}
