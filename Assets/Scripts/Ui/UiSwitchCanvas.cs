using UnityEngine;

public class UiSwitchCanvas : MonoBehaviour
{
    public static UiSwitchCanvas Instance = null;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas scanCanvas;
    [SerializeField] private Canvas cartCanvas;
    [SerializeField] private Canvas itemCanvas;

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
        itemCanvas.sortingOrder = 0;
        SetScanner(false);
    }

    public void ShowScanCanvas()
    {
        scanCanvas.sortingOrder = 1;
        cartCanvas.sortingOrder = 0;
        mainCanvas.sortingOrder = 0;
        itemCanvas.sortingOrder = 0;
        SetScanner(true);
    }

    public void ShowCartCanvas()
    {
        cartCanvas.sortingOrder = 1;
        scanCanvas.sortingOrder = 0;
        mainCanvas.sortingOrder = 0;
        itemCanvas.sortingOrder = 0;
        SetScanner(false);
    }

    public void ShowItemViewerCanvas()
    {
        itemCanvas.sortingOrder = 1;
        cartCanvas.sortingOrder = 0;
        scanCanvas.sortingOrder = 0;
        mainCanvas.sortingOrder = 0;
        SetScanner(false);
    }

    private void SetScanner(bool flag)
    {
        UiScanner.Instance.isScannerOn = flag;
        if (flag)
        {
            UiScanner.Instance.StartBarcodeScanner();
        }
        else
        {
            UiScanner.Instance.StopBarcodeScanner();
        }
    }
}
