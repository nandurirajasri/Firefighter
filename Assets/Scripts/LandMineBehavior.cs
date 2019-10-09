using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineBehavior : MonoBehaviour {
    public GameObject player = null;
    public TheWorld worldRef = null;
    public GameObject SimpleExp;

	// Use this for initialization
	void Start () {
        Debug.Assert(player != null);
        Debug.Assert(worldRef != null); 
	}
	
	// Update is called once per frame
	void Update () {
        if(minetoPlayerDist() <= 0.16f)
        {
            createExplosion();
            worldRef.setActivatedTrap(true);
        }
	}

    public float minetoPlayerDist()
    {
        float dist = (player.transform.localPosition - transform.localPosition).magnitude;
        //Debug.Log("The distance is" + dist);
        return dist;
    }

    public void createExplosion()
    {
        //Debug.Log("Inside of explosion method");
        Instantiate(SimpleExp, player.transform.localPosition, Quaternion.identity);
        //Debug.Log("Explosion GameObject made");
    }
}
