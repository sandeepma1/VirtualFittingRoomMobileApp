using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UiItemViewer : MonoBehaviour
{
    public static Action<Item> AddSelectedItem;
    public static UiItemViewer Instance = null;
    [SerializeField] private Button addToCartButton;
    [SerializeField] private Button scanAgainButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI sizeText;
    private Item tempItem;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        addToCartButton.onClick.AddListener(AddToCartButton);
        scanAgainButton.onClick.AddListener(ScanAgainButton);
        UiScanner.OnBarcodeScanComplete += PopulateItem;
    }

    private void AddToCartButton()
    {
        AddSelectedItem?.Invoke(tempItem);
        UiSwitchCanvas.Instance.ShowCartCanvas();
    }

    private void ScanAgainButton()
    {
        UiSwitchCanvas.Instance.ShowScanCanvas();
    }

    public void PopulateItem(Item item)
    {
        tempItem = item;
        itemImage.sprite = item.Sprite;
        itemNameText.text = item.Name;
        priceText.text = "Rs." + item.Price;
        sizeText.text = "Size:" + item.Size;
    }
}