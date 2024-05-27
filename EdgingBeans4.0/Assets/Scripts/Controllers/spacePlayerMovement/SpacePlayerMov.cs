using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System.Threading;
using Unity.VisualScripting;

public class SpacePlayerMov : MonoBehaviour
{

    #region Variables

    [Header("Movement")]
    public Transform playerPosition;
    public CharacterController controller;
    public float speed = 4f;
    public float sprint = 1.5f;
    public float slowness = 0.6f;
    public bool hasSlowness;
    //public float jumpTime = 1;

    [Header("Dashing")]
    public Camera Camera;
    public float w_time = 0;
    public float a_time = 0;
    public float s_time = 0;
    public float d_time = 0;

    [Header("Crouching")]
    public Transform defaultState;
    public Transform ctrlState;
    public Camera ctrlCam;
    public Transform Player;
    public float playerHeigth;
    public float halfPlayerHeigth;

    [Header("Gravity")]
    public float gravity = -3f * 2f;
    public float jumpHeight = 1.5f;
    public bool continousJump = true;
    public float inAirResistance = 0.6f;

    [Header("GroundChecks")]
    public Transform groundCheck;
    public Transform roofCheck;
    public float groundDistance = 0.2f;
    public float roofDistance = 0.2f;
    public bool hasRoof;
    public bool canStand;
    public LayerMask groundMask;
    public LayerMask obstacleMask;
    public LayerMask interactableMask;
    public LayerMask enemyMask;
    bool isGrounded;

    #endregion
    Vector3 scale;
    Vector3 velocity;

    void Start()
    {
        scale = Player.localScale;
        halfPlayerHeigth = playerHeigth / 2;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask | obstacleMask | interactableMask | enemyMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        #region Full Movement

        if (Input.GetKey(KeyCode.LeftShift) && !hasSlowness) //detects for shift key pressed
        {
            if (isGrounded)
            {
                controller.Move(move * speed * Time.deltaTime * sprint); // <* jumpTime> if shift pressed this happens (player moves 1.4 as fast)
                //Debug.Log("Sprinting!");
            } else
            {
                controller.Move(move * speed * inAirResistance * Time.deltaTime * sprint); // <* jumpTime> if shift pressed this happens (player moves 1.4 as fast)
                //Debug.Log("Sprinting in air!");
            }
        } else if (!hasSlowness){
            if (isGrounded)
            {
                controller.Move(move * speed * Time.deltaTime); // <* jumpTime> if shift not is pressed this happens (plyer moves at default speed wich is in the <speed> variable)
                //Debug.Log("Walking normal!")
            } else
            {
                controller.Move(move * speed * inAirResistance * Time.deltaTime); // <* jumpTime> if shift not is pressed this happens (plyer moves at default speed wich is in the <speed> variable)
                //Debug.Log("Walking in air!")
            }
        } else if (hasSlowness)
        {
            if (isGrounded)
            {
                controller.Move(move * speed * slowness * Time.deltaTime); // <* jumpTime> if shift not is pressed this happens (plyer moves at default speed wich is in the <speed> variable)
                //Debug.Log("Walking normal!")
            }
            else
            {
                controller.Move(move * speed * inAirResistance * slowness * Time.deltaTime); // <* jumpTime> if shift not is pressed this happens (plyer moves at default speed wich is in the <speed> variable)
                //Debug.Log("Walking in air!")
            }
        }

        #endregion

        #region wasd input timer

        if (Input.GetKey(KeyCode.W))
        {
            w_time += 1;
        } else
        {
            w_time = 0;
        }
        
        if(Input.GetKey(KeyCode.A))
        {
            a_time += 1;
        } else
        {
            a_time = 0;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            s_time += 1;
        } else
        {
            s_time = 0;
        }
        
        if(Input.GetKey(KeyCode.D))
        {
            d_time += 1;
        } else
        {
            d_time = 0;
        }
        //if (Input.GetKey(KeyCode.Space))
        //{
            //jumpTime += .001f;
            //Debug.Log("Jump "+jumpTime);
            //Thread.Sleep(10); //Setter koden på pause i ".Sleep(x)" milisekunder OBS: må ha øverst i scriptet "using System.Threading;" NB: Stopper også frames, så det blir færre frames i sekundet
        //}else{
            //jumpTime = 1;
        //}

        #endregion

        //Dashing
        if(Input.GetKey(KeyCode.V))
        {
            controller.Move(move * 1.3f);
            //Debug.Log("Dashed!");
            //Thread.Sleep(100);
        }

        #region Jumping

        if (continousJump)
        {
            if (Input.GetKey(KeyCode.Space) && isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                //Debug.Log("Jumping continous!");
        } else {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                //Debug.Log("NOT Jumping continous!");
            }
        }

        #endregion

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        #region Crouching

        hasRoof = Physics.CheckSphere(roofCheck.position, roofDistance, obstacleMask);
        if (hasRoof)
            canStand = false;
        else
            canStand = true;

        if (Input.GetKey(KeyCode.C))
        {

            ctrlCam.transform.position = ctrlState.position;
            hasSlowness = true;
            Player.localScale = new Vector3(Player.localScale.x, halfPlayerHeigth, Player.localScale.z);
        }
        else if (canStand)
        {
            ctrlCam.transform.position = defaultState.position;
            hasSlowness = false;
            Player.localScale = new Vector3(Player.localScale.x, playerHeigth, Player.localScale.z);
        }

        #endregion
    }
}
