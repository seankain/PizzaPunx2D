using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplier : MonoBehaviour
{
    public float targetSpeed = 0.05f;
    public List<PlacementSocket> deliverySockets;
    public PlacementSocket sourceSocket;
    public float deliveryX = 0.75f;
    public float homeX = 3.25f;

    [SerializeField]
    private Animator anim;
    private float speed = 0;
    private Vector2 direction = Vector2.zero;
    [SerializeField]
    private Holdable currentHolding;
    [SerializeField]
    private Transform holdLocation;

    private enum MovementState { entering, waiting, returning, idle }

    MovementState currentState = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (sourceSocket == null) Debug.LogError("Source socked needs to be populated");
    }

    void Update()
    {
        var isMoving = false;
        direction.x = 0;

        switch (currentState)
        {
            case MovementState.entering:
                anim.SetInteger("Direction", 3);
                direction.x -= 1;
                isMoving = true;
                if (transform.position.x < deliveryX) currentState = MovementState.waiting;
                break;

            case MovementState.returning:
                anim.SetInteger("Direction", 1);
                direction.x += 1;
                isMoving = true;
                if (transform.position.x > homeX) currentState = MovementState.idle;
                break;

            case MovementState.waiting:
                if (isCloseSocketAvailable() == true)
                {
                    //interactable that is holdable is released
                    currentHolding.Drop();
                    currentHolding = null;
                    currentState = MovementState.returning;
                }
                break;

            case MovementState.idle:
                if (sourceSocket.OccupiedBy != null)
                {
                    var ingredient = sourceSocket.OccupiedBy;
                    var holdable = ingredient.GetComponent<Holdable>();
                    if (holdable != null)
                    {
                        currentHolding = holdable;
                        currentHolding.Pickup();
                    }
                    currentState = MovementState.entering;
                }
                break;
        }
        
        if (isMoving)
        {
            speed = targetSpeed;
        }
        else
        {
            anim.SetInteger("Direction", -1); // Goin' nowhere
            speed = 0;
        }

        anim.SetFloat("Speed", speed);
        this.transform.position += new Vector3(direction.x * speed, 0, 0);

        if (currentHolding != null)
        {
            var holdPosition = holdLocation.position;
            holdPosition.x += direction.x * 0.2f;
            holdPosition.y += direction.y * 0.2f;
            currentHolding.transform.position = holdPosition;
        }
    }

    public bool isCloseSocketAvailable()
    {
        foreach (var ds in deliverySockets)
        {
            if (ds.OccupiedBy == null) return true;
        }
        return false;
    }
}
