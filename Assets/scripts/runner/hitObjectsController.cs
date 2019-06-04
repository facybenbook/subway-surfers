using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitObjectsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    // Casts the ray and get the first game object hit
        //    Physics.Raycast(ray, out RaycastHit click);
        //    if (click.collider.tag == "social")
        //    {
        //        score.socialLevel++;
        //        Destroy(click.collider.gameObject);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    //hit.point.z > transform.position.z + 0.01f &&
    //    switch (hit.gameObject.tag)
    //    {
    //        case "obstacle":
    //            playerMotor.crash();
    //            if (score.currentMood < 0)
    //                return;
    //            score.currentMood -= 5;
    //            break;
    //        case "goodFood":
    //            Destroy(hit.gameObject);
    //            score.agePoints++;
    //            break;
    //        case "social":
    //            Destroy(hit.gameObject);
    //            score.socialLevel--;
    //            break;
    //        case "talent":
    //            Destroy(hit.gameObject);
    //            switch (hit.gameObject.name)
    //            {
    //                case "music":
    //                    score.skill2_points++;
    //                    break;
    //                case "sport":
    //                    score.skill1_points++;
    //                    break;
    //                case "paint":
    //                    score.skill2_points++;
    //                    break;
    //                case "book":
    //                    score.skill3_points++;
    //                    break;
    //            }

    //            break;
    //        case "healthDamage":
    //            Destroy(hit.gameObject);
    //            score.currentHealth--;
    //            break;
    //        case "love":
    //            Destroy(hit.gameObject);
    //            score.loveStatus++;
    //            break;
    //        default:
    //            return;
    //    }

    //}

}
