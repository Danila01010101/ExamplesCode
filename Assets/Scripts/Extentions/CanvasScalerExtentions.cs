using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CanvasScalerExtentions
{
    public static void SetDefaultCanvasSettings(this CanvasScaler canvasScaler)
    {
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.matchWidthOrHeight = 0.5f;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
    }
}
