using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cc : MonoBehaviour {
    public CharacterController ccc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 d = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            d += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            d -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            d += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            d -= Vector3.right;
        }
        ccc.SimpleMove(d.normalized );
    }
}
