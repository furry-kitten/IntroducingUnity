using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool KeyNeeded = false;              //Is key needed for the door
    public bool GotKey;                  //Has the player acquired key
    public GameObject KeyGameObject;            //If player has Key,  assign it here
    //public GameObject txtToDisplay;             //Display the information about how to close/open the door

    public bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not
    [SerializeField]
    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    private enum DoorState
    {
        Closed,
        Opened,
        Jammed
    }

    private DoorState doorState;      //To check the current state of the door

    /// <summary>
    /// Initial State of every variables
    /// </summary>
    private void Start() {
        GotKey = false;
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        doorState = DoorState.Closed;           //Starting state is door closed

        //txtToDisplay.SetActive(false);

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

        //If Key is needed and the KeyGameObject is not assigned, stop playing and throw error
        if (KeyNeeded && KeyGameObject == null) {
            //UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("Assign Key GameObject");
        }
    }

    private void OnTriggerEnter(Collider other) {
        //txtToDisplay.SetActive(true);
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other) {
        playerInZone = false;
        //txtToDisplay.SetActive(false);
    }

    private void Update() {
        //To Check if the player is in the zone
        if (playerInZone) {
            if (doorState == DoorState.Opened) {
                //txtToDisplay.GetComponent<Text>().text = $"Press '{KeyCode.E}' to Close";
                doorCollider.enabled = false;
            } else if (GotKey) {
                //txtToDisplay.GetComponent<Text>().text = $"Press '{KeyCode.E}' to Open";
                doorCollider.enabled = true;
            }
        }

        if (!Input.GetKeyDown(KeyCode.E) || !playerInZone) {
            return;
        }

        doorOpened = !doorOpened;           //The toggle function of door to open/close
        if (doorAnim.isPlaying == false) {
            switch (doorState) {
                case DoorState.Closed:
                    if (!KeyNeeded || GotKey) {
                        doorAnim.Play("Door_Open");
                        doorState = DoorState.Opened;
                    }

                    if (GotKey == false) {
                        doorState = DoorState.Jammed;
                    }

                    break;
                case DoorState.Opened:
                    doorAnim.Play("Door_Close");
                    doorState = DoorState.Closed;
                    break;
                case DoorState.Jammed:
                    if (GotKey) {
                        doorAnim.Play("Door_Open");
                        doorState = DoorState.Opened;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (GotKey) {
            return;
        }

        //doorAnim.Play("Door_Jam");
        doorState = DoorState.Jammed;
    }
}
