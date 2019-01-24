using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UiShowCart : MonoBehaviour
{
    public static Action<int> onTotalItemsChanged;
    public static UiShowCart Instance = null;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button checkoutButton;
    [SerializeField] private Button okButton;
    [SerializeField] private UiCartItem uiCartItemPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private TextMeshProUGUI subTotalLabelText;
    [SerializeField] private TextMeshProUGUI subTotalText;
    [SerializeField] private TextMeshProUGUI taxText;
    [SerializeField] private TextMeshProUGUI TotalText;
    public Dictionary<int, UiCartItem> uiCartItems = new Dictionary<int, UiCartItem>();
    private const float gstTax = 0.05f; //in %
    public int totalItems = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonPressed);
        checkoutButton.onClick.AddListener(OnCheckoutButtonPressed);
        okButton.onClick.AddListener(OnOkButtonPressed);
        UiItemViewer.AddSelectedItem += PopulateCart;
    }

    private void OnDestroy()
    {
        continueButton.onClick.RemoveListener(OnContinueButtonPressed);
        checkoutButton.onClick.RemoveListener(OnCheckoutButtonPressed);
        okButton.onClick.RemoveListener(OnOkButtonPressed);
        UiItemViewer.AddSelectedItem -= PopulateCart;
    }

    private void OnContinueButtonPressed()
    {
        UiSwitchCanvas.Instance.ShowScanCanvas();
    }

    private void OnCheckoutButtonPressed()
    {
        popupPanel.SetActive(true);
    }

    private void OnOkButtonPressed()
    {
        popupPanel.SetActive(false);
    }

    private void PopulateCart(Item item)
    {
        // if same item added, then increase quanity
        if (uiCartItems.ContainsKey(item.ID))
        {
            uiCartItems[item.ID].Quantity = uiCartItems[item.ID].Quantity + 1;
        }
        else
        {
            UiCartItem uiCartItem = Instantiate(uiCartItemPrefab, contentParent);
            uiCartItem.SetItem(item);
            uiCartItem.Quantity = 1;
            uiCartItems.Add(item.ID, uiCartItem);
        }

        foreach (KeyValuePair<int, UiCartItem> entry in uiCartItems)
        {
            print("All cart items " + entry.Value.item.Name);
        }

        CalculateTaxTotal();
    }

    public void CalculateTaxTotal()
    {
        float subTotal = 0;
        float tax = 0;
        float total = 0;
        totalItems = 0;
        foreach (KeyValuePair<int, UiCartItem> entry in uiCartItems)
        {
            subTotal += entry.Value.Price * entry.Value.Quantity;
            totalItems += entry.Value.Quantity;
        }
        onTotalItemsChanged?.Invoke(totalItems);
        tax = subTotal * gstTax;
        total = subTotal + tax;

        subTotalLabelText.text = "Cart Sub Total (" + totalItems + " items)";
        subTotalText.text = "Rs. " + subTotal.ToString();
        taxText.text = "Rs. " + tax.ToString();
        TotalText.text = "Rs. " + total.ToString();
    }
}