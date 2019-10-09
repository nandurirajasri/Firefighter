using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBehavior : MonoBehaviour {
    public TheWorld worldRef = null;
    public GameObject playerFeet = null;
    public GameObject playerBody = null;
    public GameObject playerHead = null;
    public UILineSegment lazer = null;
    public LineEndPt endPt1 = null;
    public LineEndPt endPt2 = null;
    public GameObject SimpleExp;

	// Use this for initialization
	void Start () {
        Debug.Assert(worldRef != null);
        Debug.Assert(playerFeet != null);
        Debug.Assert(playerBody != null);
        Debug.Assert(playerHead != null);
        Debug.Assert(endPt1 != null);
        Debug.Assert(endPt2 != null);

        lazer.SetEndPoints(endPt1.transform.localPosition, endPt2.transform.localPosition);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 ptOnLinel;
        Debug.Log("Distance to feet" + lazer.DistantToPoint(playerFeet.transform.position, out ptOnLinel));
        Debug.Log("Distance to body" + lazer.DistantToPoint(playerHead.transform.position, out ptOnLinel));
        Debug.Log("Distance to head" + lazer.DistantToPoint(playerBody.transform.position, out ptOnLinel));


        if (Mathf.Abs(lazer.DistantToPoint(playerFeet.transform.position, out ptOnLinel)) <= 0.18f ||
            Mathf.Abs(lazer.DistantToPoint(playerBody.transform.position, out ptOnLinel)) <= 0.18f ||
            Mathf.Abs(lazer.DistantToPoint(playerHead.transform.position, out ptOnLinel)) <= 0.18f)
        {
            createExplosion();
            worldRef.setActivatedTrap(true);
        }
	}

    public void createExplosion()
    {
        Debug.Log("Inside of explosion method");
        Instantiate(SimpleExp, playerFeet.transform.localPosition, Quaternion.identity);
        Debug.Log("Explosion GameObject made");
    }
}
