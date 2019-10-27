using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 1f;
    [SerializeField]
    private float acceleration = 0.01f;
    private float elapsed = 0;
    private Animator anim;
    private float speed = 0;
    private Vector2 direction = Vector2.zero;
    private KeyCode currentKey = KeyCode.None;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        destination = transform.position;
    }

    private void ProcessKeyDown(KeyCode keyCode, int direction) {
        if (keyCode != currentKey) {
            currentKey = keyCode;
            anim.SetInteger("Direction", direction);
        }
    }

    public void GoTo(Vector3 location)
    {
        destination = location;
    }

    // Update is called once per frame
    void Update()
    {
        //var moveKeyDown = false;
        if (Vector3.Distance(transform.position, destination) > 0.3f)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, destination, maxSpeed * Time.deltaTime);
            anim.SetFloat("Speed", maxSpeed);
        }
        else {
            anim.SetFloat("Speed", 0);
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    ProcessKeyDown(KeyCode.UpArrow, 2);
        //    //anim.SetInteger("Direction", 2);
        //    direction.x = 0;
        //    direction.y = 1;
        //    moveKeyDown = true;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    ProcessKeyDown(KeyCode.RightArrow, 1);
        //    //anim.SetInteger("Direction", 1);
        //    direction.x = 1;
        //    direction.y = 0;
        //    moveKeyDown = true;
            
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    ProcessKeyDown(KeyCode.LeftArrow, 3);
        //    //anim.SetInteger("Direction", 3);
        //    direction.x = -1;
        //    direction.y = 0;
        //    moveKeyDown = true;
            
        //}
        //if ( Input.GetKey(KeyCode.DownArrow))
        //{
        //    ProcessKeyDown(KeyCode.DownArrow, 0);
        //    //anim.SetInteger("Direction", 0);
        //    direction.x = 0;
        //    direction.y = -1;
        //    moveKeyDown = true;
        //}
        //if (moveKeyDown)
        //{
        //    if (speed < maxSpeed)
        //    {
        //        speed += acceleration;
        //    }
        //}
        //else
        //{
        //    if (speed > 0)
        //    {
        //        speed -= acceleration;
        //    }
        //    currentKey = KeyCode.None;
        //}

        //anim.SetBool("MoveKeyDown", moveKeyDown);
        //anim.SetFloat("Speed", speed);
        //this.transform.position += new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
    }
}
