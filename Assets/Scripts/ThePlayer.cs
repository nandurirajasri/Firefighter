using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ThePlayer : MonoBehaviour {

    public SceneNode RootNode;
    

    // Use this for initialization
	void Start () {
    }

    void Update()
    {
        Vector3 pos, dir;
        Matrix4x4 m = Matrix4x4.identity;
        RootNode.CompositeXform(ref m, out pos, out dir);
    }

    public SceneNode GetRootNode() { return RootNode; }
    
}
