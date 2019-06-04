using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceSpawner : MonoBehaviour
{
    public PieceType type;
    private piece currentPiece;
    // Start is called before the first frame update

    public void Spawn() 
    {
        int amtObj = 0;
        switch(type){
            case PieceType.jump:
                amtObj = levelManager.Instance.jumps.Count;
                break;
            case PieceType.slide:
                amtObj = levelManager.Instance.slides.Count;
                break;
            case PieceType.longBlock:
                amtObj = levelManager.Instance.longBlocks.Count;
                break;
            case PieceType.ramp:
                amtObj = levelManager.Instance.ramps.Count;
                break;
        }
        currentPiece = levelManager.Instance.GetPiece(type, Random.Range(0, amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }
    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }

   
}
