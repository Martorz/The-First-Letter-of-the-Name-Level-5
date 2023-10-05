using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Good2D : MonoBehaviour {

    public float pixelsPerUnit = 16f;
    public float zoom = 300f;
    public bool usePixelScale = false;
    public float pixelScale = 4f;

    Vector3 cameraPos = Vector3.zero;

    public void Move(Vector3 dir)
    {
        ApplyZoom();
        cameraPos += dir;
        AdjustCamera();
    }

    public void MoveTo(Vector3 pos)
    {
        ApplyZoom();
        cameraPos += pos;
        AdjustCamera();
    }

    public void AdjustCamera()
    {
        
        /*Camera.main.transform.position = new Vector3(RoundToNeaestPixel(cameraPos.x), RoundToNeaestPixel(cameraPos.y), -10f);*/
    }

    public float RoundToNeaestPixel(float pos) {
        float screenPixelPerUnit = Screen.height / (Camera.main.orthographicSize * 2f);
        float pixelValue = Mathf.Round(pos * screenPixelPerUnit);

        return pixelValue / screenPixelPerUnit;
    }

    public void ApplyZoom()
    {
        if (!usePixelScale) {
            float smallestDimension = Screen.height < Screen.width ? Screen.height : Screen.width;
            pixelScale = Mathf.Clamp(Mathf.Round(smallestDimension / zoom), 1f, 8f);
        }
        Camera.main.orthographicSize = (Screen.height / (pixelsPerUnit * pixelScale)) * 0.5f;
        //Camera.main.orthographicSize = 10;
    }

    public Vector3 GetCameraPos() {
        return cameraPos;
    }
}
