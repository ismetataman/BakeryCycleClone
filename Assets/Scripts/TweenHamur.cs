using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenHamur : MonoBehaviour
{
    public HamurStack hamurstack;
    public Transform destinationPos;
    public GameObject hamur;


    
    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(hamurstack.duration >1)
            {
                hamur.transform.DOMove(destinationPos.position,0.5f);
            }
        }    
    }
}
