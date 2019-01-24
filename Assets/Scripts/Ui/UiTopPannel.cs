using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiTopPannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cartItemsText;
    [SerializeField] private Button cartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        cartButton.onClick.AddListener(OnCartButtonClicked);
        menuButton.onClick.AddListener(OnMenuButtonClicked);
        UiShowCart.onTotalItemsChanged += UpdateCartItems;
    }

    private void OnDestroy()
    {
        cartButton.onClick.RemoveListener(OnCartButtonClicked);
        menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        UiShowCart.onTotalItemsChanged -= UpdateCartItems;
    }

    private void UpdateCartItems(int obj)
    {
        cartItemsText.text = obj.ToString();
    }

    private void OnCartButtonClicked()
    {
        UiSwitchCanvas.Instance.ShowCartCanvas();
    }

    private void OnMenuButtonClicked()
    {
        UiSwitchCanvas.Instance.ShowMainCanvas();
    }
}