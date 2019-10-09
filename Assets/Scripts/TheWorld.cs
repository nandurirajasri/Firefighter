using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheWorld : MonoBehaviour
{
    public MainController controller;
    public ThePlayer player;        // FeetSN
    public GameObject ui;
    public Text win;
    public Text lost;

    bool stopGame;
    bool activatedTrap;
    Transform currentSelected = null;
    private const int kSelectorLayer = 8;
    private const int kSelectorLayerMask = 1 << kSelectorLayer;

    // Use this for initialization
    void Start()
    {
        ui.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lost.gameObject.SetActive(false);
        stopGame = false;
        activatedTrap = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerCondition();
    }

    void CheckPlayerCondition()
    {
        // Win Condition
        if ((player.transform.localPosition.x >= 10f && player.transform.localPosition.x <= 11f) &&
            (player.transform.localPosition.z <= 1.0f && player.transform.localPosition.z >= 0f))
        {
            ui.gameObject.SetActive(true);
            win.gameObject.SetActive(true);
            stopGame = true;
        }

        // Lose Condition
        if (player.transform.localPosition.x < -1f || activatedTrap)
        {
            ui.gameObject.SetActive(true);
            lost.gameObject.SetActive(true);
            stopGame = true;
        }
    }

    public bool getState()
    {
        return stopGame;
    }

    public Transform SelectAMarker(Ray aRay)
    {
       RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(aRay, out hitInfo, Mathf.Infinity, kSelectorLayerMask);
        if (hit)    // We can only hit either the player or the cube
        {
            // Cube
            if (hitInfo.transform.gameObject.tag.Equals("Cube"))
            {
                string name = hitInfo.transform.gameObject.name;
                currentSelected = hitInfo.transform;
                Debug.Log("RayHit: " + name);
            }

            // Player
            if (hitInfo.transform.gameObject.tag.Equals("Player"))
            {
                controller.LookAtPlayer(player.transform);
                string name = hitInfo.transform.parent.parent.gameObject.name;
                currentSelected = hitInfo.transform.parent.parent;
                Debug.Log("RayHit: " + name);
            }
        }
        else
        {
            currentSelected = null;
        }
        return currentSelected;
    }

    public void setActivatedTrap(bool cond)
    {
        activatedTrap = cond;
    }
}
