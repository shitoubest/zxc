using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GCHandleTest : MonoBehaviour {
    public class TestClass
    {
        public int testInt;
    }

    public TestClass testObj;
    public GCHandle testObjHandle;
    public TestClass testObj2;
    public GCHandle testObjHandle2;
    // Use this for initialization
    void Start () {
        testObj = new TestClass();
        testObjHandle = GCHandle.Alloc(testObj,GCHandleType.Weak);
        testObj2 = new TestClass();
        testObjHandle2 = GCHandle.Alloc(testObj2, GCHandleType.Pinned);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(testObjHandle.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle));
            Debug.Log("#############");
            Debug.Log(testObjHandle2.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle2));
            testObj = null;
            testObj2 = null;
            GC.Collect();
            Debug.Log(testObjHandle.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle));
            Debug.Log("#############");
            Debug.Log(testObjHandle2.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle2));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(testObjHandle.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle));
            Debug.Log("#############");
            Debug.Log(testObjHandle2.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle2));
            testObjHandle.Free();
            testObjHandle2.Free();
            GC.Collect();
            //Debug.Log(testObjHandle.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle));
            Debug.Log("#############");
            //Debug.Log(testObjHandle2.Target);
            Debug.Log(GCHandle.ToIntPtr(testObjHandle2));
        }
    }
}
