using UnityEngine;
using UnityEngine.UI;

public class UiMainCanvas : MonoBehaviour
{
    [SerializeField] private Button startScanButton;
    [SerializeField] private Button showCartButton;

    private void Start()
    {
        startScanButton.onClick.AddListener(OnStartScanButtonClicked);
        showCartButton.onClick.AddListener(OnShowCartButtonClicked);
    }

    private void OnDestroy()
    {
        startScanButton.onClick.RemoveListener(OnStartScanButtonClicked);
        showCartButton.onClick.RemoveListener(OnShowCartButtonClicked);
    }

    private void OnStartScanButtonClicked()
    {
        UiSwitchCanvas.Instance.ShowScanCanvas();
    }

    private void OnShowCartButtonClicked()
    {
        UiSwitchCanvas.Instance.ShowCartCanvas();
    }
}