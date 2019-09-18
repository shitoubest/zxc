using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShape : GJKShape
{
    public CircleShape(Vector3 center,float radius)
    {
        c = center;
        r = radius;
    }

    public override Vector3 center
    {
        get
        {
            return c;
        }
    }
    public override Vector3 GetSupportPoint(Vector3 dir)
    {
        return center + dir * r;
    }

    private Vector3 c;
    private float r;
}
