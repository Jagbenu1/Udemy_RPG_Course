using UnityEngine;

public class Player : Entity
{

    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;

    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTimer;

    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("AttckInfo")]
    [SerializeField] private float comboTime = 1.3f;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;

    //float variable that cannot be seen from the inspector and cannot give access to another script
    private float xInput;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
        CheckInput();

        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;



        FlipController();
        AnimatorControllers();
    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

        if(comboCounter > 2)
            comboCounter = 0;

       
    }

    

    private void CheckInput()
    {
        //used for snappy immediate input
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAttackEvent()
    {
        if (!isGrounded)
            return;

        if (comboTimeWindow < 0)
            comboCounter = 0;

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTimer = dashDuration;
        }
    }

    private void Movement()
    {
        if(isAttacking) 
        {
            rb.velocity = new Vector2(0, 0);
        }
         else if (dashTimer > 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTimer > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 & facingRight)
            Flip();
    }

    
}
