using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJK : MonoBehaviour {
    public bool IsSum = false;
    public bool IsDiff = false;
    public List<Transform> v1List;
    public List<Transform> v2List;

    public List<Vector3> rs;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (v1List == null || v2List == null)
        {
            return;
        }
        if (IsSum)
        {
            rs = MSum(v1List, v2List);
        }
        if (IsDiff)
        {
            rs = MDiff(v1List, v2List);
        }
    }
    public static List<Vector3> TransformToVecList(List<Transform> ts)
    {
        var list = new List<Vector3>();
        foreach (var item in ts)
        {
            list.Add(item.position);
        }
        return list;
    }
    public static List<Vector3> MSum(List<Transform> v1, List<Transform> v2)
    {
        return MSum(TransformToVecList(v1), TransformToVecList(v2));
    }
    public static List<Vector3> MDiff(List<Transform> v1, List<Transform> v2)
    {
        return MDiff(TransformToVecList(v1), TransformToVecList(v2));
    }

    public static List<Vector3> MSum(List<Vector3> v1, List<Vector3> v2)
    {
        List<Vector3> rs = new List<Vector3>();
        foreach (var item1 in v1)
        {
            foreach (var item2 in v2)
            {
                rs.Add(item1 + item2);
            }
        }
        return rs;
    }
    public static List<Vector3> MDiff(List<Vector3> v1, List<Vector3> v2)
    {
        List<Vector3> rs = new List<Vector3>();
        foreach (var item1 in v1)
        {
            foreach (var item2 in v2)
            {
                rs.Add(item1 - item2);
            }
        }
        return rs;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (v1List != null)
        {
            foreach (var item in v1List)
            {
                Gizmos.DrawSphere(item.transform.position,0.1f);
            }
        }
        Gizmos.color = Color.blue;
        if (v2List != null)
        {
            foreach (var item in v2List)
            {
                Gizmos.DrawSphere(item.transform.position, 0.1f);
            }
        }
        Gizmos.color = Color.green;
        if (rs != null)
        {
            foreach (var item in rs)
            {
                Gizmos.DrawSphere(item, 0.1f);
            }
        }
    }

}
