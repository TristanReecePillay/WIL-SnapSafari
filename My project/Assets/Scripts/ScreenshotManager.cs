using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    public Image screenshotDisplay;
    public GameObject screenshotPanel; // Reference to the UI panel
    public GameObject minimap;
    private List<Texture2D> screenshots = new List<Texture2D>();
    private int currentScreenshotIndex = -1; // Initialize to -1 to indicate no screenshot is currently displayed
    private bool isScreenshotVisible = false;
    private bool isPanelVisible = false; // Add this variable to track panel visibility
    private bool isPaused = false;

    private void Start()
    {
        screenshotDisplay.gameObject.SetActive(false); // Hide the image initially
        screenshotPanel.SetActive(false); // Hide the panel initially
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeScreenshot();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleScreenshotDisplay();
            ToggleScreenshotPanel(); // Toggle the panel when Tab is pressed
            //Time.timeScale = 0;
            
            // Check if the Tab key is pressed
           
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle between pause and unpause
            if (isPaused)
            {
                // Resume the game
                Time.timeScale = 1.0f;
            }
            else
            {
                // Pause the game
                Time.timeScale = 0.0f;
                Debug.Log("Game is paused");
            }

            // Toggle the paused state
            isPaused = !isPaused;
        }

        // Cycle between screenshots using arrow keys
        if (isScreenshotVisible && screenshots.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShowPreviousScreenshot();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShowNextScreenshot();
            }
            
        }
    }

    private void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        minimap.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        minimap.SetActive(true);

        // Calculate the raycast origin (position on the terrain)
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Center of the screenshot
        Vector3 raycastOrigin = ray.origin;

        // Calculate the raycast direction (camera's forward direction)
        Vector3 raycastDirection = Camera.main.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, Mathf.Infinity))
        {

            if (hit.collider.CompareTag("Animals"))
            {
                // A GameObject was hit by the raycast
                GameObject hitObject = hit.collider.gameObject;

                // You can now work with the hitObject or perform any desired actions
                Debug.Log("Hit object: " + hitObject.name);

                // Handle the detection of the GameObject here
                HandleGameObjectDetection(hitObject);
            }
            else
            {
                Debug.Log("Hit object is not an animal.");
            }
           
        }
        else
        {
            // The raycast did not hit any GameObject
            Debug.Log("No object hit.");
        }

        screenshots.Add(screenshot);
        currentScreenshotIndex = screenshots.Count - 1; // Update the index to the latest screenshot

        SaveScreenshotToFile(screenshot);
    }

    private void HandleGameObjectDetection(GameObject detectedObject)
    {
        // Implement logic to handle the detected GameObject
        //  display information, trigger actions, or store data related to the detected object here
    }

    private void SaveScreenshotToFile(Texture2D screenshot)
    {
        byte[] bytes = screenshot.EncodeToPNG();
        string screenshotPath = Path.Combine(Application.dataPath, "Screenshots");
        Directory.CreateDirectory(screenshotPath);

        string screenshotFileName = $"Screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        string screenshotFilePath = Path.Combine(screenshotPath, screenshotFileName);

        File.WriteAllBytes(screenshotFilePath, bytes);
        Debug.Log($"Screenshot saved: {screenshotFilePath}");
    }

    private void ToggleScreenshotDisplay()
    {
        isScreenshotVisible = !isScreenshotVisible;

        if (isScreenshotVisible)
        {
            ShowScreenshot(currentScreenshotIndex); // Display the current screenshot
        }
        else
        {
            screenshotDisplay.gameObject.SetActive(false);
        }
    }

    private void ToggleScreenshotPanel()
    {
        isPanelVisible = !isPanelVisible;
        screenshotPanel.SetActive(isPanelVisible);
    }

    private void ShowPreviousScreenshot()
    {
        currentScreenshotIndex = (currentScreenshotIndex - 1 + screenshots.Count) % screenshots.Count;
        ShowScreenshot(currentScreenshotIndex);
    }

    private void ShowNextScreenshot()
    {
        currentScreenshotIndex = (currentScreenshotIndex + 1) % screenshots.Count;
        ShowScreenshot(currentScreenshotIndex);
    }

    private void ShowScreenshot(int index)
    {
        if (screenshots.Count > 0)
        {
            screenshotDisplay.sprite = Sprite.Create(screenshots[index], new Rect(0, 0, screenshots[index].width, screenshots[index].height), Vector2.one * 0.5f);
            screenshotDisplay.gameObject.SetActive(true);
        }
    }
}

