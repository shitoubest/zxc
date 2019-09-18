using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJKShape {
    public virtual Vector3 center{ get; private set; }
    public virtual Vector3 GetSupportPoint(Vector3 dir)
    {
        return Vector3.zero;
    }

}
