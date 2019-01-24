using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase m_instance = null;
    public static List<Item> items = new List<Item>();
    private string fileName = "ItemDatabase";

    private void Awake()
    {
        m_instance = this;
        ConstructItemDatabase();
    }

    public static Item GetItemByItemId(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id)
            {
                return items[i];
            }
        }
        return null;
    }

    void ConstructItemDatabase()
    {
        string[] lines = new string[100];
        string[] chars = new string[100];
        TextAsset itemCSV = Resources.Load("CSV/" + fileName) as TextAsset;
        lines = Regex.Split(itemCSV.text, "\r\n");
        for (int i = 1; i < lines.Length - 1; i++)
        {
            chars = Regex.Split(lines[i], ",");

            items.Add(new Item(
               IntParse(chars[0]),
                chars[1],
                (ItemType)System.Enum.Parse(typeof(ItemType), chars[2]),
                FloatParse(chars[3]),
                chars[4]
            ));
        }
    }

    int IntParse(string text)
    {
        int num;
        if (int.TryParse(text, out num))
        {
            return num;
        }
        else
            return 0;
    }

    float FloatParse(string text)
    {
        float result = 0.01f;
        float.TryParse(text, out result);
        return result;
    }
}

[System.Serializable]
public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public float Price { get; set; }
    public string Size { get; set; }
    public Sprite Sprite { get; set; }

    public Item() { }

    public Item(int id, string name, ItemType type, float price, string size)
    {
        ID = id;
        Name = name;
        Type = type;
        Price = price;
        Size = size;
        Sprite = Resources.Load<Sprite>("Textures/" + id);
    }
}

public enum ItemType
{
    Shirt,
    Pant
}