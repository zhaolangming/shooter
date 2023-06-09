using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSppeed;
    private Rigidbody2D myRigidbody;
    private Animator myAim;
    private BoxCollider2D myFeet;
    private bool isGround;

    public static object PlayeController { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
        Jump();
      
        CheckGrounded();
        SwitchAnimation();
      
    }
    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0;
        if (playerHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x>0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        } 
    }
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log(isGround); 
    }



    void Run()
    { float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;//对于人物要定义一个速度
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0;
        myAim.SetBool("Run", playerHasXAxisSpeed); 
 }
    void Jump()
    {
        if (Input.GetButtonDown("Jump")){
            if (isGround)
            {
                myAim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSppeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
            
        }
    }
  

    void SwitchAnimation()
    {
        myAim.SetBool("idle", false);
        if (myAim.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAim.SetBool("Jump", false);
                myAim.SetBool("fall", true);
            }
        }
        else if (isGround)
        {
            myAim.SetBool("fall", false);
            myAim.SetBool("idle",true );
           
        }

    }


    

}
