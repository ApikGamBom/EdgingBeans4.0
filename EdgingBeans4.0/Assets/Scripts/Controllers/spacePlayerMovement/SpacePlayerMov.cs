using UnityEngine;

public class SpacePlayerMov : MonoBehaviour
{

    #region Variables

    [Header("Movement")]
    public Transform playerPosition;
    public CharacterController controller;
    public float speed = 4f;
    public float sprint = 1.5f;
    //public float jumpTime = 1;

    [Header("Dashing")]
    public Camera Camera;
    public float w_time = 0;
    public float a_time = 0;
    public float s_time = 0;
    public float d_time = 0;

    //==Removed Crouching==
    //[Header("Crouching")]
    //public Transform defaultState;
    //public Transform ctrlState;
    //public Camera ctrlCam;
    //public Transform Player;
    //public float playerHeigth;
    //public float halfPlayerHeigth;

    [Header("Gravity")]
    public float gravity = -3f * 2f;
    public float jumpHeight = 1.5f;
    public bool continousJump = true;

    [Header("GroundChecks")]
    public Transform groundCheck;
    public Transform roofCheck;
    public float groundDistance = 0.2f;

    //==This is for Crouching==
    //public float roofDistance = 0.2f;
    //public bool hasRoof;

    public bool canStand;
    public LayerMask groundMask;
    public LayerMask obstacleMask;
    public LayerMask interactableMask;
    public LayerMask enemyMask;
    public bool isGrounded;

    #endregion
    Vector3 velocity;

    void Start()
    {

        //==This is for Crouching==
        //scale = Player.localScale;
        //halfPlayerHeigth = playerHeigth / 2;
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

        if (Input.GetKey(KeyCode.LeftShift)) //detects for shift key pressed
        {
            if (isGrounded)
            {
                controller.Move(move * speed * Time.deltaTime * sprint); // <* jumpTime> if shift pressed this happens (player moves 1.4 as fast)
                //Debug.Log("Sprinting!");
            }
        } else {
            if (isGrounded)
            {
                controller.Move(move * speed * Time.deltaTime); // <* jumpTime> if shift not is pressed this happens (plyer moves at default speed wich is in the <speed> variable)
                //Debug.Log("Walking normal!")
            }
        }

        #endregion

        //Dashing
        if(Input.GetKeyDown(KeyCode.V))
        {
            controller.Move(move * 20 * Time.deltaTime);
            //velocity.x = 20f;
            //controller.Move(move * 1.3f);
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

        //#region Crouching

        //hasRoof = Physics.CheckSphere(roofCheck.position, roofDistance, obstacleMask);
        //if (hasRoof)
        //    canStand = false;
        //else
        //    canStand = true;

        //if (Input.GetKey(KeyCode.C))
        //{

        //    ctrlCam.transform.position = ctrlState.position;
        //    hasSlowness = true;
        //    Player.localScale = new Vector3(Player.localScale.x, halfPlayerHeigth, Player.localScale.z);
        //}
        //else if (canStand)
        //{
        //    ctrlCam.transform.position = defaultState.position;
        //    hasSlowness = false;
        //    Player.localScale = new Vector3(Player.localScale.x, playerHeigth, Player.localScale.z);
        //}

        //#endregion
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDistance);
    }
}
