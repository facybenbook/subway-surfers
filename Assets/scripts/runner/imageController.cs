using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageController : MonoBehaviour
{

    //skills' images
    public static GameObject skill1_image;
    private GameObject skill2_image;
    private GameObject skill3_image;

    // Start is called before the first frame update
    void Start()
    {
        //get current skill's image
        skill1_image = GameObject.Find("skill1_image");
        skill2_image = GameObject.Find("skill2_image");
        skill3_image = GameObject.Find("skill3_image");

        //disable the pictures
        skill1_image.SetActive(false);
        skill2_image.SetActive(false);
        skill3_image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
