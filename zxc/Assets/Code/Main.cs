using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Main Start.");
        Debug.Log("===========");
        Debug.Log(c(5,1));
        Debug.Log("===========");
        Debug.Log(c(5,2));
        Debug.Log("===========");
        Debug.Log(c(5,3));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private int c(int n,int k)
    {
        if (n == 1)
        {
            Debug.Log(0);
            return 0;
        }
        int rs = (c(n - 1, k) + k) % n;
        Debug.Log(rs);
        return rs;
    }
}
