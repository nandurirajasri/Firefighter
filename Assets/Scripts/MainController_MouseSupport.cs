using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {

    // Mouse click selection 
    void SelectionSupport()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            SendSelectionRay();
            //translator.gameObject.SetActive(true);
        }
        else
        {
            translator.ResetSelectMode();
            //translator.gameObject.SetActive(false);
        }
    }

    void SendSelectionRay()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // check for GUI
        {
            Ray aRay = worldCam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Click inside SendSelectionRay()");
                if (!translator.SelectMode(aRay))
                {
                    //Debug.Log("SelectMode returned false");
                    Transform t = world.SelectAMarker(aRay);
                    //Debug.Log(t.transform.position);
                    translator.SetTargetTransform(t);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                translator.DragTargetTranslation();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                translator.ResetSelectMode();
            }
        }
    }
}
