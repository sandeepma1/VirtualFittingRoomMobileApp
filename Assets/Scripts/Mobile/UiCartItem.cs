using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCartItem : MonoBehaviour
{
    public Action<int> OnDeleteButton;
    public Item item;
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI quantityText;
    public Button deleteItemButton;

    private int quantity;
    public int Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            quantity = value;
            quantityText.text = "Quantity: " + quantity.ToString();
        }
    }

    private float price;
    public float Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
            priceText.text = "Rs. " + price.ToString(); // ₹
        }
    }

    private void Start()
    {
        deleteItemButton.onClick.AddListener(DeleteThisItem);
    }

    private void OnDestroy()
    {
        deleteItemButton.onClick.RemoveListener(DeleteThisItem);
    }

    private void DeleteThisItem()
    {
        UiShowCart.Instance.uiCartItems.Remove(item.ID);
        UiShowCart.Instance.CalculateTaxTotal();
        Destroy(gameObject);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        iconImage.sprite = item.Sprite;
        nameText.text = item.Name;
        Price = item.Price;
    }
}