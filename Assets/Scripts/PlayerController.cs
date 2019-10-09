//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
    

//    void Update()
//    {
//        //Vector3 pos, dir;
//        //Matrix4x4 m = Matrix4x4.identity;
//        //RootNode.CompositeXform(ref m, out pos, out dir);

//        //Vector3 p1 = pos;
//        //Vector3 p2 = p1 + kSightLength * dir;
//        ////LineOfSight.SetEndPoints(p1, p2);

//        //// Now update NodeCam
//        //HeadCam.transform.localPosition = pos + kNodeCamPos * dir;
//        //HeadCam.transform.LookAt(p2, transform.up);
//        if (Input.GetKey(KeyCode.A))
//        {
//            Debug.Log("Pressing A");
//            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
//            transform.Translate(-Vector3.right * Time.deltaTime * speed);
//        }


//        if (Input.GetKey(KeyCode.W))
//        {
//            Debug.Log("Pressing W");
//            //transform.Translate(Vector3.right * Time.deltaTime * speed);
//            transform.Translate(Vector3.forward * Time.deltaTime * speed);
//        }

//        if (Input.GetKey(KeyCode.S))
//        {
//            Debug.Log("Pressing S");
//            //transform.Translate(-Vector3.right * Time.deltaTime * speed);
//            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
//        }

//        if (Input.GetKey(KeyCode.D))
//        {
//            Debug.Log("Pressing D");
//            //transform.Translate(-Vector3.forward * Time.deltaTime * speed);
//            transform.Translate(Vector3.right * Time.deltaTime * speed);
//        }
//    }
//}

