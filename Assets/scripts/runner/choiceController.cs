using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class choiceController : MonoBehaviour
{
    [SerializeField] private string loadLevel;
    //private GameObject player = GameObject.FindWithTag("Player");
    public static Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            lastPosition = hit.gameObject.transform.position;
            Debug.Log("from choice controller "+lastPosition);
            SceneManager.LoadScene(loadLevel);
        }
    }



}
