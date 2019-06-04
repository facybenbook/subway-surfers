using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 offset = new Vector3(0, 2.5f, -1.0f);

    private void Start()
    {
        transform.position = lookAt.position + offset;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0;
        desiredPosition.y = 8;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * playerMotor.speed);


    }


    //private Transform lookAt;
    //private Vector3 startOffset;
    //private Vector3 moveVector;

    //private float transition = 0.0f;
    //private float animationDuration = 3.0f;
    //private Vector3 animationOffset = new Vector3(0, 5, 5);
    ////private Vector3 lastCamera = new Vector3(0, 0, choiceController.lastPosition.z);

    //// Start is called before the first frame update
    //void Start()
    //{
    //   lookAt = GameObject.FindGameObjectWithTag("Player").transform;
    //   startOffset = transform.position - lookAt.position;

    //    //transform.position = choiceController.lastPosition; 

    //    //Debug.Log(choiceController.lastPosition[2]);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //moveVector = lastCamera;
    //    moveVector = lookAt.position + startOffset;
    //    //x
    //    moveVector.x = 0;


    //    //y
    //    moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

    //    if (transition > 1.0f)
    //    {
    //        transform.position = moveVector;
    //    }
    //    else
    //    {
    //        transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
    //        transition += Time.deltaTime * 1 / animationDuration;
    //        transform.LookAt(lookAt.position + Vector3.up);
    //    }

    //}
}
