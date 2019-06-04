using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class segment : MonoBehaviour
{
    public int segID { set; get; }
    public bool transition;

    public int length;
    public int beginY1, beginY2, beginY3;
    public int endY1, endY2, endY3;

    private pieceSpawner[] pieces;

    private void Start()
    {
        //$$


    }
    private void Awake()
    {
        pieces = gameObject.GetComponentsInChildren<pieceSpawner>();
        for (int i = 0; i < pieces.Length; i++)
            foreach (MeshRenderer mr in pieces[i].GetComponentsInChildren<MeshRenderer>())
                mr.enabled = levelManager.Instance.SHOW_COLLIDER;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < pieces.Length; i++)
            pieces[i].Spawn();
        
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < pieces.Length; i++)
            pieces[i].Despawn();
    }

}
