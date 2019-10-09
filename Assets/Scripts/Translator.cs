using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    // we expect this is bounded to a Selector GameObject
    // with empty object as parent and X/Y/Z/Origin children

    enum TranslateMode
    {
        TranslateInX,
        TranslateInY,
        TranslateInZ,
        NoTransalte
    };

    private const int kSelectorLayer = 8;
    private const int kSelectorLayerMask = 1 << kSelectorLayer;
    private TranslateMode mCurrentMode = TranslateMode.NoTransalte;
    private Vector3 mInitMousePosition = Vector3.zero;
    private const float kMouseScaleFactor = 0.01f;

    private Transform mTarget = null;
    private Color XColor = Color.red;
    private Color YColor = Color.green;
    private Color ZColor = Color.blue;
    private Color SelectedColor = Color.yellow;
    private Quaternion q;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
            child.transform.gameObject.layer = kSelectorLayer;
        SetChildrenColor();

        transform.gameObject.SetActive(false);
    }

    public void SetTargetTransform(Transform t)
    {
        mTarget = t;
        transform.gameObject.SetActive(t != null);

        if (mTarget != null)
        {
            Debug.Log("mTarget: " + mTarget.name);
            //Debug.Log("Inside mTarget != null");
            transform.localPosition = t.position;
            Vector3 transScale = t.localScale;

            if (mTarget.tag != "Cube")
            {
                transScale = t.GetChild(0).GetChild(0).localScale;
            }
            //Debug.Log(transScale);
            transform.localScale = transScale;
        }
    }

    public void ResetSelectMode()
    {
        mCurrentMode = TranslateMode.NoTransalte;
        SetChildrenColor();
    }

    public bool SelectMode(Ray aRay)    // return the status of consuming the mouse
    {
        if (mTarget == null)
            return false;

        SetChildrenColor();
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(aRay, out hitInfo, Mathf.Infinity, kSelectorLayerMask);
        if (hit)
        {
            string hitName = hitInfo.transform.name;
            //Debug.Log("RayHit inside Translator.cs: " + hitName);
            mInitMousePosition = Input.mousePosition;

            if (hitName == "X")
                mCurrentMode = TranslateMode.TranslateInX;
            else if (hitName == "Y")
                mCurrentMode = TranslateMode.TranslateInY;
            else if (hitName == "Z")
                mCurrentMode = TranslateMode.TranslateInZ;
            else
                mCurrentMode = TranslateMode.NoTransalte;

            if (mCurrentMode != TranslateMode.NoTransalte)
                hitInfo.transform.GetComponent<Renderer>().material.color = SelectedColor;
        }
        return (mCurrentMode != TranslateMode.NoTransalte);
    }

    public void DragTargetTranslation()
    {
        if (mTarget == null)
            return;

        if (mCurrentMode != TranslateMode.NoTransalte)
        {   // this means, one of the translation axis is already selected
            Vector3 delta = kMouseScaleFactor * (Input.mousePosition - mInitMousePosition);
            mInitMousePosition = Input.mousePosition;
            switch (mCurrentMode)
            {
                case TranslateMode.TranslateInX:
                    delta.y = delta.z = 0;
                    break;
                case TranslateMode.TranslateInY:
                    delta.x = delta.z = 0;
                    break;
                case TranslateMode.TranslateInZ:
                    delta.z = -delta.x;
                    delta.y = delta.x = 0;
                    break;
            }
            if (mTarget.name == "FeetSN" || mTarget.name == "TorsoSN" || mTarget.name == "HeadSN")       // Player parts
            {
                if (mCurrentMode == TranslateMode.TranslateInX)
                {
                    q = Quaternion.AngleAxis(delta.x * 10f, Vector3.right);
                }
                else if (mCurrentMode == TranslateMode.TranslateInY)
                {
                    q = Quaternion.AngleAxis(delta.y * 10f, Vector3.up);
                }
                else if (mCurrentMode == TranslateMode.TranslateInZ)
                {
                    q = Quaternion.AngleAxis(delta.z * 10f, Vector3.forward);
                }

                mTarget.localRotation *= q;
            }
            else
            {
                mTarget.localPosition += delta;
                transform.position = mTarget.position;
            }
        }
    }

    void SetChildrenColor()
    {
        transform.FindChild("X").gameObject.GetComponent<Renderer>().material.color = XColor;
        transform.FindChild("Y").gameObject.GetComponent<Renderer>().material.color = YColor;
        transform.FindChild("Z").gameObject.GetComponent<Renderer>().material.color = ZColor;
    }
}
