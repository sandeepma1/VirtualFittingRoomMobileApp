using UnityEngine;
using UnityEngine.UI;

public class UiMainCanvas : MonoBehaviour
{
    [SerializeField] private Button startScanButton;

    private void Start()
    {
        startScanButton.onClick.AddListener(OnStartScanButtonClicked);
    }

    private void OnDestroy()
    {
        startScanButton.onClick.RemoveListener(OnStartScanButtonClicked);
    }

    private void OnStartScanButtonClicked()
    {
        UiSwitchCanvas.Instance.ShowScanCanvas();
    }
}