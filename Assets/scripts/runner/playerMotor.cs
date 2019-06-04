using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float jumpForce = 6.0f;

    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private int desiredLane = 1;

    public static float speed = 5.0f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;
    private float originalSpeed = 7.0f;


    private float animationDuration = 3.0f;

    private const float LANE_DISTANCE = 2.5f;
    private const float TURN_SPEED = 0.05f;

    private bool isRunning = false;

    //animator
    private Animator anim;

    void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isRunning)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray socialRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Casts the ray and get the first game object hit
            Physics.Raycast(socialRay, out RaycastHit click);
            if (click.collider.tag == "social")
            {
                score.socialLevel++;
                Destroy(click.collider.gameObject);
            }
            else
            {
                return;
            }
        }

        if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;

        }

     
        // gather the input on which lane we should be
        if (mobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
            moveLane(false);
        if (mobileInput.Instance.SwipeRight || Input.GetKeyDown(KeyCode.RightArrow))
            moveLane(true);
      
        //calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        // calculate move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        bool isGroundedBool = isGrounded();
        anim.SetBool("Grounded", isGroundedBool);

        //calculate Y
        if (isGrounded())
        {
            verticalVelocity = -0.1f;

            if (mobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }
            else if (mobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
            {
                //slide
                startSliding();
                Invoke("stopSliding", 1.0f);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;

            //fast falling mechanic
            if (mobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //move the character
        controller.Move(moveVector * Time.deltaTime);

        //rotate the character to where he is going
        Vector3 dir = controller.velocity;
       
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);



    }
    private void startSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);

    }
    private void stopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);

    }

    private void moveLane(bool goingRight)
    {
        //left
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    public void setSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

  

    private bool isGrounded()
    {
        Ray groundRay = new Ray(
        new Vector3(
            controller.bounds.center.x,
            (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
            controller.bounds.center.z),
        Vector3.down);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);


    }

    public void startRunning()
    {
        isRunning = true;
        anim.SetTrigger("startRunning");
    }

    private void crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        score.agePoints -= 10;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //hit.point.z > transform.position.z + 0.01f &&
        switch (hit.gameObject.tag)
        {
            case "coin":
                score.coinScore++;
                Debug.Log(score.coinScore);
                break;
            case "obstacle":
                crash();
                if (score.currentMood < 0)
                    return;
                score.currentMood -= 5;
                break;
            case "goodFood":
                //hit.gameObject.GetComponent<Animator>().Play("Collected");
                Destroy(hit.gameObject);
                score.agePoints++;
                break;
            case "social":
                Destroy(hit.gameObject);
                score.socialLevel--;
                break;
            case "talent":
                Destroy(hit.gameObject);
                switch (hit.gameObject.name)
                {
                    case "skill1":
                        score.skill1_points++;
                        break;
                    case "skill2":
                        score.skill2_points++;
                        break;
                    case "skill3":
                        score.skill3_points++;
                        break;
                }

                break;
            case "healthDamage":
                Destroy(hit.gameObject);
                score.currentHealth--;
                break;
            case "love":
                Destroy(hit.gameObject);
                score.loveStatus++;
                break;
            default:
                return;
        }

    }

}
