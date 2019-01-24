using UnityEngine;

public class UiSwitchCanvas : MonoBehaviour
{
    public static UiSwitchCanvas Instance = null;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas scanCanvas;
    [SerializeField] private Canvas cartCanvas;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowMainCanvas();
    }

    public void ShowMainCanvas()
    {
        mainCanvas.sortingOrder = 1;
        scanCanvas.sortingOrder = 0;
        cartCanvas.sortingOrder = 0;
        UiScanner.Instance.isScannerOn = false;
        UiScanner.Instance.StopBarcodeScanner();
    }

    public void ShowScanCanvas()
    {
        scanCanvas.sortingOrder = 1;
        cartCanvas.sortingOrder = 0;
        mainCanvas.sortingOrder = 0;
        UiScanner.Instance.isScannerOn = true;
        UiScanner.Instance.StartBarcodeScanner();
    }

    public void ShowCartCanvas()
    {
        cartCanvas.sortingOrder = 1;
        scanCanvas.sortingOrder = 0;
        mainCanvas.sortingOrder = 0;
        UiScanner.Instance.isScannerOn = false;
        UiScanner.Instance.StopBarcodeScanner();
    }
}
