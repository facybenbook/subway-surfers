using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public BallController target;
    private float offset; // Keep initial distance between cam and ball

    private void Awake()
    {
        offset = transform.position.y - target.transform.position.y;
    }

    void Update () {
        // Move camera smoothly to target height (yTargetPos)
        Vector3 curPos = transform.position;
        curPos.y = target.transform.position.y + offset;
        transform.position = curPos;
    }
}
