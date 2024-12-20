using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //float variable that can be seen in the inspector and can give access to another script
    //public float xInput;

    //this variable can only store info about rigidbodies
    private Rigidbody2D rb;
    private Animator anim;

    
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;


    //float variable that cannot be seen from the inspector and cannot give access to another script
    private float xInput;
    [SerializeField] private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //used for snappy immediate input
        xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
    }
}
