using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
    private Animator _anim;
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;

    private void Start()
    {
        _anim = GetComponent<Animator>();   
    }
    private void Update() 
    {
            if(Input.GetMouseButton(0))
            {
                _anim.SetBool("canRun",true);

                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            }
            else
                _anim.SetBool("canRun",false);
    }
}
