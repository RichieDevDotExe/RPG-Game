using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 lookPos;
    private Camera mainCamera;
    private float speed;
    Vector3 lookDir;
    private float idleTimer;
    private Animator animator;
    private int layerMask = 1 << 10;
    private Rigidbody rb;
    private float dodgeTimer;
    private Vector3 moveDirection;
    private AudioClip dashSFX;

    [SerializeField]private float dashStrength;
    [SerializeField]private float dashCooldown;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        dashSFX = Player.instance.dashSFX;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //receive inputs from InputManager and apply them   to character controller
    public void ProcessMove(Vector2 input)
    {
        Debug.Log(input);
        if((input.x != 0) || (input.y != 0)) {
            idleTimer = 0f;
            //animator.SetBool("idleTimer", false);
            moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            speed = Player.instance.EntitySpeed;
            //var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            characterController.Move(moveDirection * speed * Time.deltaTime);

            if (moveDirection.magnitude > 1.0f)
            {
                moveDirection = moveDirection.normalized;
            }

            moveDirection = transform.InverseTransformDirection(moveDirection);

            /*
            animator.SetFloat("VelX",moveDirection.x*4);
            animator.SetFloat("VelZ", moveDirection.z*4);
            animator.SetBool("isRunning", true);
        }
        else if((input.x == 0) && (input.y == 0) && (idleTimer >= 15)){
            idleTimer = 0f;
            animator.SetBool("idleTimer", true);
        }
        else
        {
            idleTimer += Time.deltaTime;
            animator.SetFloat("VelX", 0);
            animator.SetFloat("VelZ", 0);
            animator.SetBool("isRunning", false);
            //Debug.Log(idleTimer);
            */
        }
    }


    private void dodgeMovement()
    {
        //rb.AddForce(transform.rotation*moveDirection * dashStrength,ForceMode.Impulse);
    }

    /*
    public IEnumerator activateDodge()
    {
        Debug.Log("Time " + (Time.time - dodgeTimer) + " cooldown " + dashCooldown);
        if ((Time.time - dodgeTimer) >= dashCooldown)
        {
            //dodgeMovement();
            Player.instance.Hitbox.enabled = false;
            SoundFXManager.instance.playSoundEffect(dashSFX, transform, 1f);
            //Player.instance.Hitbox.excludeLayers = 1<<6;
            rb.AddForce(transform.rotation * moveDirection * dashStrength, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);

            //Player.instance.Hitbox.excludeLayers = 0;
            Player.instance.Hitbox.enabled = true;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            dodgeTimer = Time.time;
        }
    }


    public float DodgeTimer
    {
        get { return dodgeTimer; }
        set { dodgeTimer = value; }
    }

    public float DashCooldown
    {
        get { return dashCooldown; }
        set { dashCooldown = value; }
    }

    */

    public Rigidbody RB
    {
        get { return rb; }
        set { rb = value; }
    }

    public void playerFaceTowards()
    {
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit,100, layerMask))
        {
            //Debug.Log(hit.collider);
            lookPos = hit.point;
        }
        lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(lookPos,1);
    }
}
