using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    public Image screenshotDisplay;
    public GameObject screenshotPanel; // Reference to the UI panel
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
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        screenshots.Add(screenshot);
        currentScreenshotIndex = screenshots.Count - 1; // Update the index to the latest screenshot

        SaveScreenshotToFile(screenshot);
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

