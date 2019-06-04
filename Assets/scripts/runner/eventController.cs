using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eventController : MonoBehaviour
{
    public Text msg;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator toggleEventWindow(string content)
    {
        gameObject.SetActive(true);
        msg.text = content;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    

}
