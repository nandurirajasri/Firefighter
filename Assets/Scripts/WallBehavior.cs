using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour {
    public GameObject player = null;
    float mD;       // mN dot P = mD;, direction of plane
   // public LineSegment mNormal; // This is the normal vector we will display

    // Use this for initialization
    void Start () {
        Debug.Assert(player != null);
    }

    private void Update()
    {
        UpdatePlaneEquation();
        ProcessBarrier();
    }

    public void UpdatePlaneEquation()
    {
        //mD = Vector3.Dot(transform.localPosition, GetNormal());
        mD = Vector3.Dot(transform.position, GetNormal());
    }

    public Vector3 GetNormal() { return -transform.forward; }
    public float GetD() { return mD; }

    public float DistantToPoint(Vector3 p)
    {
        return Vector3.Dot(player.transform.localPosition, GetNormal()) - mD;
    }

    public bool InActiveZone(Vector3 p)
    {
        //Vector3 d = p - transform.localPosition;
        Vector3 d = p - transform.position;
        return d.magnitude < (transform.localScale.x / 2f);
    }

    public bool PtInfrontOf(Vector3 p)
    {
        //Vector3 va = p - transform.localPosition;
        Vector3 va = p - transform.position;
        return (Vector3.Dot(va, GetNormal()) > 0f);
    }

    public void ProcessBarrier()
    {
        float d = DistantToPoint(player.transform.position);
        Vector3 onBarrier = player.transform.position - GetNormal() * d;
        //Debug.Log(d);

        if (InActiveZone(onBarrier))
        {
            if (Mathf.Abs(d) <= 0.20f) // close enough
            {
                //Debug.Log("Inside first check: Distance");
                if (PtInfrontOf(player.transform.position))
                {
                    //Debug.Log("Inside second check: InFrontOfQWall");
                    // Push back
                    Vector3 newPos = (-transform.forward * .20f) + player.transform.position;
                    player.transform.position = newPos;
                }
                else // (!PtInfrontOf(player.transform.localPosition)
                {
                    // Push back
                    Vector3 newPos = (transform.forward * .20f) + player.transform.position;
                    player.transform.position = newPos;
                }
            }
        }
    }
}
