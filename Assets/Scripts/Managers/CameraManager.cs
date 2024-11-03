using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [System.Serializable]
    public class CameraData // Define class for a Camera
    {
        [Tooltip("Write a name for the camera that will be visible to the player.")]
        public string name; // public reference for a writable name in the inspector
        [Tooltip("Please insert the reference for the camera object.")]
        public GameObject cameraObject; // public reference to camera object
    }
    [Header("Cameras")]
    [Tooltip("Click here to add or remove camera references.")]
    public List<CameraData> cameras = new List<CameraData>(); // Creates a list of class cameras
    private int currentCameraIndex = 0; // Index for current camera accessible

    [Header("Transition Options")]
    [Tooltip("Place transition prefab if testing for an animation. Leave blank for no animation.")]
    [SerializeField] private GameObject TransitionalScreenPrefab; // Prefab with Animator for transition effect
    [Tooltip("Click here to add or remove sfx clips.")]
    public AudioClip[] soundEffects; // Array to hold the sound effect clips

    [Header("UI Elements")]
    [Tooltip("Place reference to displayCameraText for displaying camera name.")]
    public TMP_Text displayCameraText; // Reference to the TMP_Text object for displaying the camera name

    private void Start()
    {
        // Checks if a camera exists and isn't null
        if (cameras.Count > 0 && cameras[0].cameraObject != null)
        {
            ActivateCamera(0); // Set the initial active camera
        }
    }

    private void Update()
    {
        // If the correct input is pressed down, activate the previous camera
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(SwitchCamera(true)); // Checks if the input is asking for the previous camera, aka going left in the list
        } // Else, if the other correct input is pressed down, activate the next camera
        else if (Input.GetKeyDown(KeyCode.RightControl))
        {
            StartCoroutine(SwitchCamera(false)); // Checks if the input is asking for the next camera, aka going right in the list
        }
    }

    public void ActivateCamera(int index)
    {
        // Checks if the input index is within the bounds of the camera list
        if (index < 0 || index >= cameras.Count)
        {
            return; // If the camera doesn't exist in the list or if the index is invalid, STOP.
        }

        // Deactivate all current cameras
        foreach (var cam in cameras)
        {
            // If the camera object exists, make it inactive
            if (cam.cameraObject != null)
            {
                cam.cameraObject.SetActive(false);
            }
        }

        // Activate camera at index if camera object isn't null
        if (cameras[index].cameraObject != null)
        {
            // Activates camera object
            cameras[index].cameraObject.SetActive(true);
            currentCameraIndex = index; // Sets current index to the index given
        }

        UpdateDisplayCamText(); // Update the camera name text on activation
    }

    private void NextCamera()
    {
        // Taking the current camera index and incrementing to the next camera within the list.
        int nextIndex = (currentCameraIndex + 1) % cameras.Count;
        ActivateCamera(nextIndex); // Activating the next camera in the list.
    }

    private void PrevCamera()
    {
        // Navigates to the previous index in a circular motion without leaving the list's bounds.
        int prevIndex = (currentCameraIndex - 1 + cameras.Count) % cameras.Count;
        ActivateCamera(prevIndex); // Activating the previous camera in the list
    }

    // Updates camera name text in the UI
    private void UpdateDisplayCamText()
    {
        if (displayCameraText != null)
        {
            // Checks if there are cameras in the list
            if (cameras.Count > 0)
            {
                displayCameraText.text = cameras[currentCameraIndex].name; // Sets the display text to the active camera's name
            }
        }
    }

    private IEnumerator SwitchCamera(bool IsLeft)
    {
        // Determines the direction of the camera switch
        switch (IsLeft)
        {
            case true: // Calling the method to switch to the previous camera if IsLeft is true (-1)
                PrevCamera();
                break;
            case false: // Calling the method to switch to the next camera if IsLeft is false (1)
                NextCamera();
                break;
        }

        // Checks if the Transitional Screen is null, if null then skip the process of transitioning
        if (TransitionalScreenPrefab != null)
        {
            // Instantiate a new transitional screen from the prefab to overlay when the camera switches
            GameObject transition = Instantiate(TransitionalScreenPrefab);
            GameObject canvas = GameObject.Find("Canvas"); // Finds Canvas object and makes it a reference
            transition.transform.SetParent(canvas.transform, false); // Sets transition screen as a child of Canvas to overlay over Camera screen
            PlayRandomSFX(); // Calls function to randomize and play transitional sound effect
            yield return new WaitForSeconds(0.1f); // Wait a second for the transition animation to play
        }
    }

    private void PlayRandomSFX()
    {
        // Checks if there are cameras in the list
        if (cameras.Count > 0)
        {
            // Get the audio source component from the active camera
            AudioSource currentAudio = cameras[currentCameraIndex].cameraObject.GetComponent<AudioSource>();

            // Check if the audio source and sound effects exist
            if (currentAudio != null && soundEffects.Length > 0)
            {
                // Pick randomized index from the soundEffects array
                int randomIndex = Random.Range(0, soundEffects.Length);

                // Play the selected sound from the current camera's audio source
                currentAudio.PlayOneShot(soundEffects[randomIndex]);
            }
        }
    }
}
