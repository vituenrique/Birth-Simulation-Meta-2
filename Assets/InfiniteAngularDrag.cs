using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAngularDrag : MonoBehaviour
{
    Rigidbody rig;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void  LateUpdate ()
    {
        rig.angularVelocity = Vector3.zero;	
	}
}
