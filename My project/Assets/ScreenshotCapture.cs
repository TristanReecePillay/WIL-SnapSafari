using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    private int screenshotCount = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button click
        {
            CaptureScreenshot();
        }
    }

    private void CaptureScreenshot()
    {
        string screenshotFileName = "Screenshot_" + screenshotCount + ".png";
        string screenshotFilePath = System.IO.Path.Combine(Application.dataPath, screenshotFileName);

        ScreenCapture.CaptureScreenshot(screenshotFilePath);

        Debug.Log("Screenshot captured: " + screenshotFilePath);

        screenshotCount++;
    }
}
