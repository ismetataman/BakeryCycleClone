using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacklist : MonoBehaviour
{
    public static Stacklist instance;
    public List <GameObject> stack = new List<GameObject>();
    void Awake()
    {
        instance = this;
    }
}
