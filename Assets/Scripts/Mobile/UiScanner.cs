using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;
using TMPro;

public class UiScanner : MonoBehaviour
{
    public static UiScanner Instance = null;
    public static Action<Item> OnBarcodeScanComplete;

    [SerializeField] private TextMeshProUGUI textHeader;
    [SerializeField] private RawImage webcamTexture;
    [SerializeField] private Button backToMainMenuButton;

    private IScanner BarcodeScanner;
    private AudioSource audioSource;

    public bool isScannerOn = false;

    private void Awake()
    {
        Instance = this;
    }

    IEnumerator Start()
    {
        // When the app start, ask for the authorization to use the webcam
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            throw new Exception("This Webcam library can't work without the webcam authorization");
        }

        InitializeBarcodeScanner();
        backToMainMenuButton.onClick.AddListener(OnBackToMainMenuButton);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        backToMainMenuButton.onClick.RemoveListener(OnBackToMainMenuButton);
    }

    private void Update()
    {
        if (BarcodeScanner == null)
        {
            return;
        }
        BarcodeScanner.Update();
    }

    private void OnBackToMainMenuButton()
    {
        UiSwitchCanvas.Instance.ShowMainCanvas();
    }

    private void InitializeBarcodeScanner()
    {
        // Create a basic scanner
        BarcodeScanner = new Scanner();
        BarcodeScanner.Camera.Play();

        // Display the camera texture through a RawImage
        BarcodeScanner.OnReady += (sender, arg) =>
        {
            // Set Orientation & Texture
            webcamTexture.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            webcamTexture.transform.localScale = BarcodeScanner.Camera.GetScale();
            webcamTexture.texture = BarcodeScanner.Camera.Texture;

            // Keep Image Aspect Ratio
            var rect = webcamTexture.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
            StartBarcodeScanner();
        };

        // Track status of the scanner
        BarcodeScanner.StatusChanged += (sender, arg) =>
        {
            // TextHeader.text = "Status: " + BarcodeScanner.Status;
        };
    }

    public void StartBarcodeScanner()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Start");
            return;
        }
        if (!isScannerOn)
        {
            return;
        }
        // Start Scanning
        BarcodeScanner.Scan((barCodeType, barCodeValue) =>
        {
            OnScanComplete(barCodeValue);
        });
    }

    private void OnScanComplete(string barCodeValue)
    {
        print(barCodeValue);
        int itemId;
        Item item;
        //If barcode value is in int/numbers
        if (int.TryParse(barCodeValue, out itemId))
        {
            item = ItemDatabase.GetItemByItemId(itemId);
        }
        else
        {
            textHeader.text = "Barcode is invalid or record not found, please scan different product or contact store manager.";
            return;
        }

        if (item == null)
        {
            textHeader.text = "Item not found as per Barcode value, please contact store manager.";
        }
        else
        {
            textHeader.text = "";
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif 
            StopBarcodeScanner();
            audioSource.Play();
            OnBarcodeScanComplete?.Invoke(item);
            OpenCartWindow();
        }
    }

    public void StopBarcodeScanner()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Stop");
            return;
        }
        // Stop Scanning
        BarcodeScanner.Stop();
    }

    public void OpenCartWindow()
    {
        UiSwitchCanvas.Instance.ShowCartCanvas();
    }

    /// <summary>
    /// This coroutine is used because of a bug with unity (http://forum.unity3d.com/threads/closing-scene-with-active-webcamtexture-crashes-on-android-solved.363566/)
    /// Trying to stop the camera in OnDestroy provoke random crash on Android
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator StopCamera(Action callback)
    {
        // Stop Scanning
        webcamTexture = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }
}