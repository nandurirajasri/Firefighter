using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MainController : MonoBehaviour
{
    public TheWorld world;
    public Dropdown cameraModeSelect;
    public Camera firstPersonCam;
    public Camera worldCam;
    public ThePlayer player;            // FeetSN
    public GameObject view;             // Head
    public Button wcResetViewButton;
    public GameObject lookAt;
    public Translator translator;

    int playerSpeed = 2;
    float fpSpeed = 100f;
    bool lookingAtPlayer = false;

    // Use this for initialization
    void Start()
    {
        firstPersonCam.gameObject.SetActive(false);
        worldCam.gameObject.SetActive(false);
        wcResetViewButton.gameObject.SetActive(false);

        Button resetView = wcResetViewButton.GetComponent<Button>();
        resetView.onClick.AddListener(ResetLookAt);
    }

    // Update is called once per frame
    void Update()
    {
        switch (CheckCameraMode())
        {
            case 0:
                while (!world.getState())
                {
                    UpdateFPCamera();
                    if (!CheckPlayerMovement())
                        FPRotateCheck();
                    break;
                }
                break;
            default:    // Case 1 | World Camera
                SelectionSupport();
                break;
        }
    }

    public int CheckCameraMode()
    {
        if (cameraModeSelect.value == 0)      // FP
        {
            firstPersonCam.gameObject.SetActive(true);
            worldCam.gameObject.SetActive(false);
            wcResetViewButton.gameObject.SetActive(false);
            lookAt.gameObject.SetActive(false);
            translator.gameObject.SetActive(false);
            return 0;
        }
        else                            // World
        {
            firstPersonCam.gameObject.SetActive(false);
            worldCam.gameObject.SetActive(true);
            wcResetViewButton.gameObject.SetActive(true);
            if (!lookingAtPlayer)
            {
                lookAt.gameObject.SetActive(true);
            }
            return 1;
        }
    }

    bool CheckPlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Pressing A");
            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            player.transform.Translate(-Vector3.right * Time.deltaTime * playerSpeed);
            return true;
        }


        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Pressing W");
            //transform.Translate(Vector3.right * Time.deltaTime * speed);
            player.transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
            return true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Pressing S");
            //transform.Translate(-Vector3.right * Time.deltaTime * speed);
            player.transform.Translate(-Vector3.forward * Time.deltaTime * playerSpeed);
            return true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Pressing D");
            //transform.Translate(-Vector3.forward * Time.deltaTime * speed);
            player.transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
            return true;
        }
        return false;
    }

    void UpdateFPCamera()
    {
        firstPersonCam.transform.localPosition = view.transform.position;
        firstPersonCam.transform.localRotation = view.transform.rotation;
    }

    void FPRotateCheck()
    {
        if (Input.GetKey("up"))
        {
            Vector3 v3 = new Vector3(-Input.GetAxis("Vertical"), 0.0f, 0.0f);
            view.transform.Rotate(v3 * fpSpeed * Time.deltaTime);
        }

        if (Input.GetKey("down"))
        {
            Vector3 v3 = new Vector3(-Input.GetAxis("Vertical"), 0.0f, 0.0f);
            view.transform.Rotate(v3 * fpSpeed * Time.deltaTime);
        }

        if (Input.GetKey("left"))
        {
            Vector3 v3 = new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f);
            player.transform.Rotate(v3 * fpSpeed * Time.deltaTime);

        }

        if (Input.GetKey("right"))
        {
            Vector3 v3 = new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f);
            player.transform.Rotate(v3 * fpSpeed * Time.deltaTime);
        }
    }

    void ResetLookAt()
    {
        Vector3 originalLookAtPosition = new Vector3(5.2f, 0, 4.8f);
        lookAt.transform.localPosition = originalLookAtPosition;

        Vector3 originalCamPosition = new Vector3(5.2f, 10f, 4.8f);
        worldCam.transform.localPosition = originalCamPosition;

        Vector3 originalCamRotation = new Vector3(90f, 0f, 0f);
        worldCam.transform.eulerAngles = originalCamRotation;

        lookingAtPlayer = false;
        translator.gameObject.SetActive(false);
    }

    public void LookAtPlayer(Transform player)
    {
        lookingAtPlayer = true;
        lookAt.SetActive(false);

        Vector3 playerPosition = player.transform.localPosition;
        lookAt.transform.localPosition = playerPosition;

        Vector3 camPosition = new Vector3(player.transform.localPosition.x, 3f, player.transform.localPosition.z);
        worldCam.transform.localPosition = camPosition;

        Vector3 camRotation = new Vector3(90f, 0f, 0f);
        worldCam.transform.eulerAngles = camRotation;
    }
}
