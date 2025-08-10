using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
 

public static class Utils
{

    
    public static int addLayerMask(int originalLayerNumber, string name)
    {
        return originalLayerNumber |= (1 << LayerMask.NameToLayer(name));
    }

    public static void addCullingMaskToTheTargetCamera(Camera camera, string layerName)
    {
        camera.cullingMask |= 1 << LayerMask.NameToLayer(layerName);
    }

    public static void removeCullingMaskToTheTargetCamera(Camera camera, string layerName)
    {
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer(layerName));
    }

    public static T convertStringToEnum<T>(string str)
    {
        return (T)Enum.Parse(typeof(T), str, true);
    }

    public static void startDelayedAction(UnityAction action, float waitingTime)
    {
        int xx = 0;
        DOTween.To(() => xx, x => xx = x, 1, waitingTime).OnComplete(() =>
        {
            if (action != null)
                action();
        });
    }

    public static void ChangeLayers(GameObject go, string name)
    {
        Transform[] trs = go.GetComponentsInChildren<Transform>(true);
        foreach (Transform tr in trs)
        {
            tr.gameObject.layer = LayerMask.NameToLayer(name);
        }
    }

    public static void ChangeLayers(GameObject go, int layer)
    {
        Transform[] trs = go.GetComponentsInChildren<Transform>(true);
        foreach (Transform tr in trs)
        {
            tr.gameObject.layer = layer;
        }
    }

    public static void ChangeTags(GameObject go, string TAG)
    {
        Transform[] trs = go.GetComponentsInChildren<Transform>(true);
        foreach (Transform tr in trs)
        {
            tr.gameObject.tag = TAG;
        }
    }

    public static Vector2 RotateVector2(this Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }

    public static Vector2 RotateVector3(this Vector3 v, float degrees)
    {
        return Quaternion.Euler(0, degrees, 0) * v;
    }
 
 
 

    public static Tweener showTargetCanvasGroup(CanvasGroup targetUI, bool isShow, float time = 0.2f, UnityAction action = null)
    {
        if (isShow)
            targetUI.gameObject.SetActive(true);    //시작에는 무조건 on

        if (targetUI != null)
        {
            targetUI.interactable = true;
            targetUI.blocksRaycasts = false;
            if (isShow)
            {
                startDelayedAction(() =>
                {
                    targetUI.blocksRaycasts = true;
                }, time);

                return DOTween.To(() => targetUI.alpha, x => targetUI.alpha = x, 1, time).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    if (action != null)
                        action();
                });
            }
            else
            {
                return DOTween.To(() => targetUI.alpha, x => targetUI.alpha = x, 0, time).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    if (action != null)
                        action();
                });
            }
        }
        else
            return null;
    }

    public static string getDate()
    {
        return DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
    }

    public static Texture2D reSampleAndCrop(Texture2D source, int targetWidth, int targetHeight)
    {
        int sourceWidth = source.width;
        int sourceHeight = source.height;
        float sourceAspect = (float)sourceWidth / sourceHeight;
        float targetAspect = (float)targetWidth / targetHeight;
        int xOffset = 0;
        int yOffset = 0;
        float factor = 1;
        if (sourceAspect > targetAspect)
        { // crop width
            factor = (float)targetHeight / sourceHeight;
            xOffset = (int)((sourceWidth - sourceHeight * targetAspect) * 0.5f);
        }
        else
        { // crop height
            factor = (float)targetWidth / sourceWidth;
            yOffset = (int)((sourceHeight - sourceWidth / targetAspect) * 0.5f);
        }
        Color32[] data = source.GetPixels32();
        Color32[] data2 = new Color32[targetWidth * targetHeight];
        for (int y = 0; y < targetHeight; y++)
        {
            float yPos = y / factor + yOffset;
            int y1 = (int)yPos;
            if (y1 >= sourceHeight)
            {
                y1 = sourceHeight - 1;
                yPos = y1;
            }

            int y2 = y1 + 1;
            if (y2 >= sourceHeight)
                y2 = sourceHeight - 1;
            float fy = yPos - y1;
            y1 *= sourceWidth;
            y2 *= sourceWidth;
            for (int x = 0; x < targetWidth; x++)
            {
                float xPos = x / factor + xOffset;
                int x1 = (int)xPos;
                if (x1 >= sourceWidth)
                {
                    x1 = sourceWidth - 1;
                    xPos = x1;
                }
                int x2 = x1 + 1;
                if (x2 >= sourceWidth)
                    x2 = sourceWidth - 1;
                float fx = xPos - x1;
                var c11 = data[x1 + y1];
                var c12 = data[x1 + y2];
                var c21 = data[x2 + y1];
                var c22 = data[x2 + y2];
                float f11 = (1 - fx) * (1 - fy);
                float f12 = (1 - fx) * fy;
                float f21 = fx * (1 - fy);
                float f22 = fx * fy;
                float r = c11.r * f11 + c12.r * f12 + c21.r * f21 + c22.r * f22;
                float g = c11.g * f11 + c12.g * f12 + c21.g * f21 + c22.g * f22;
                float b = c11.b * f11 + c12.b * f12 + c21.b * f21 + c22.b * f22;
                float a = c11.a * f11 + c12.a * f12 + c21.a * f21 + c22.a * f22;
                int index = x + y * targetWidth;

                data2[index].r = (byte)r;
                data2[index].g = (byte)g;
                data2[index].b = (byte)b;
                data2[index].a = (byte)a;
            }
        }

        var tex = new Texture2D(targetWidth, targetHeight);
        tex.SetPixels32(data2);
        tex.Apply(true);
        return tex;
    }

    public static byte[] saveScreenshot(int width, int height)
    {
        RenderTexture renderTexture = new RenderTexture(width, height, 16);
        Camera.main.targetTexture = renderTexture;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        byte[] bytes = screenshot.EncodeToJPG();
        // System.IO.File.WriteAllBytes(Application.dataPath + "/" + "screenshot.jpg", bytes);

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        UnityEngine.Object.Destroy(renderTexture);

        return bytes;
    }

    public static void setTypeWriterEffect(TextMeshProUGUI _textMeshPro, string message, ScrollRect scrollRect, float typeSpeed = 80f)
    {
        _textMeshPro.text = "";
        DOTween.To(() => _textMeshPro.text.Length, x => _textMeshPro.text = message.Substring(0, x), message.Length, message.Length / typeSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            _textMeshPro.text = message;
            scrollRect.verticalNormalizedPosition = 0f;
        }); 
    }

    //get x number of random integer, within a r size of array
    //overlapping is not allowed
    public static int[] getRandomIntegers(int how_many, int array_size)
    {
        int[] result = new int[how_many]; 
        for (int i = 0; i < how_many; i++)
        {
            int random = UnityEngine.Random.Range(0, array_size);
            while (Array.IndexOf(result, random) != -1)
            {
                random = UnityEngine.Random.Range(0, array_size);
            }
            result[i] = random;
        }
        return result;
    } 
}