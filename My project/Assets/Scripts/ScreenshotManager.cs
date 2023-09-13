using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    //COMMENTING FOR TESTING PURPOSES
    
    /*public string screenshotFolder = "Screenshots"; // The folder where screenshots will be saved
    public KeyCode previousScreenshotKey = KeyCode.LeftArrow; // Key to cycle to the previous screenshot
    public KeyCode nextScreenshotKey = KeyCode.RightArrow; // Key to cycle to the next screenshot

    private List<string> screenshotPaths = new List<string>();
    private int currentScreenshotIndex = 0;

    private void Start()
    {
        // Ensure the screenshot folder exists
        Directory.CreateDirectory(screenshotFolder);

        // Get all screenshot files in the folder
        string[] screenshotFiles = Directory.GetFiles(screenshotFolder, "*.png");

        // Add the paths of all screenshots to the list
        screenshotPaths.AddRange(screenshotFiles);

        if (screenshotPaths.Count == 0)
        {
            Debug.LogWarning("No screenshots found in the folder: " + screenshotFolder);
        }
        else
        {
            ShowCurrentScreenshot();
        }
    }

    private void Update()
    {
        // Check for input to cycle through screenshots
        if (Input.GetKeyDown(previousScreenshotKey))
        {
            ShowPreviousScreenshot();
        }
        else if (Input.GetKeyDown(nextScreenshotKey))
        {
            ShowNextScreenshot();
        }
    }

    private void ShowCurrentScreenshot()
    {
        if (screenshotPaths.Count > 0)
        {
            string currentScreenshotPath = screenshotPaths[currentScreenshotIndex];
            Texture2D screenshotTexture = LoadScreenshotTexture(currentScreenshotPath);

            if (screenshotTexture != null)
            {
                // Display the screenshot wherever you want, for example, on a UI RawImage
                // Assuming you have a RawImage component named "screenshotImage" on your canvas
                // You can change this according to your needs
                GameObject.Find("screenshotImage").GetComponent<UnityEngine.UI.RawImage>().texture = screenshotTexture;
            }
        }
    }

    private void ShowNextScreenshot()
    {
        if (screenshotPaths.Count > 0)
        {
            currentScreenshotIndex = (currentScreenshotIndex + 1) % screenshotPaths.Count;
            ShowCurrentScreenshot();
        }
    }

    private void ShowPreviousScreenshot()
    {
        if (screenshotPaths.Count > 0)
        {
            currentScreenshotIndex = (currentScreenshotIndex - 1 + screenshotPaths.Count) % screenshotPaths.Count;
            ShowCurrentScreenshot();
        }
    }

    private Texture2D LoadScreenshotTexture(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2); // Create a new Texture2D
        if (texture.LoadImage(fileData)) // Load the image data into the Texture2D
        {
            return texture;
        }
        else
        {
            Debug.LogError("Failed to load screenshot: " + path);
            return null;
        }
    }*/
}

