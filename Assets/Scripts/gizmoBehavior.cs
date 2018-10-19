using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoBehavior : MonoBehaviour {

    public Transform parent;
	
	void Update () {
        this.transform.rotation = parent.rotation;
	}
}
