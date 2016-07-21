using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Basic implementation of how to use a Vive controller as an input device.
 * Can only interact with items with InteractableBase component
 */
public class Controller : MonoBehaviour
{
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    /*
    private Valve.VR.EVRButtonId padDown = Valve.VR.EVRButtonId.k_EButton_DPad_Down;
    private Valve.VR.EVRButtonId padUp = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    private Valve.VR.EVRButtonId padRight = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    private Valve.VR.EVRButtonId padLeft = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    */

    private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();

    private InteractableItem closestItem;
    private InteractableItem interactingItem;

    private Transform VRCamera;
    private float moveSpeed;
    private Vector3 lockY;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        VRCamera = GameObject.FindWithTag("Camera").transform;
        moveSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        /* MOVE IN CAMERA RIG */
        if (controller.GetPress(touchPad)) {
            if (controller.GetAxis(touchPad).y > 0.5f)
            {
                //Debug.Log("padUp pressed");
                lockY = new Vector3(VRCamera.forward.x, 0, VRCamera.forward.z);
                this.transform.parent.Translate(lockY * Time.deltaTime * moveSpeed);
                
            }
            if (controller.GetAxis(touchPad).y < -0.5f)
            {
                //Debug.Log("padDown pressed");
                lockY = new Vector3(VRCamera.forward.x, 0, VRCamera.forward.z);
                this.transform.parent.Translate(lockY * Time.deltaTime * -moveSpeed);
            }
            if (controller.GetAxis(touchPad).x > 0.5f)
            {
                //Debug.Log("padRight pressed");
                lockY = new Vector3(VRCamera.right.x, 0, VRCamera.right.z);
                this.transform.parent.Translate(lockY * Time.deltaTime * moveSpeed);
            }
            if (controller.GetAxis(touchPad).x < -0.5f)
            {
                //Debug.Log("padLeft pressed");
                lockY = new Vector3(VRCamera.right.x, 0, VRCamera.right.z);
                this.transform.parent.Translate(lockY * Time.deltaTime * -moveSpeed);
            }
        }

        
        /* TRANSFORM SCENE */
        /*
        if (controller.GetPressDown(gripButton)) {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Debug.Log("change to piano");
                SceneManager.LoadSceneAsync("PianoRoom");
            }
            else
            {
                Debug.Log("change to island");
                SceneManager.LoadSceneAsync("SmallIsland");
            }
        }
        */

        if (controller.GetPress(triggerButton))
        {
            if (controller.GetPress(triggerButton)) {
                //Debug.Log("trigger press");
                controller.TriggerHapticPulse(100);
            }
          
            // Find the closest item to the hand in case there are multiple and interact with it
            float minDistance = float.MaxValue;

            float distance;
            foreach (InteractableItem item in objectsHoveringOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }

            interactingItem = closestItem;
            closestItem = null;

            if (interactingItem)
            {
                // Begin Interaction should already end interaction from previous
                if (controller.GetPressDown(triggerButton))
                {
                    interactingItem.OnTriggerPressDown(this);
                }
            }
        }

        if (controller.GetPressUp(triggerButton) && interactingItem != null)
        {
            interactingItem.OnTriggerPressUp(this);
        }
    }

    // Adds all colliding items to a HashSet for processing which is closest
    private void OnTriggerEnter(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
        }
        //Debug.Log("in");
    }

    // Remove all items no longer colliding with to avoid further processing
    private void OnTriggerExit(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
        }
        //Debug.Log("out");
    }
}