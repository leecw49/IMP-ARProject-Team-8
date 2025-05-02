using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public int attackInt;
    public int effectInt;
    public Sprite sprite;
    public int frequency;
    public Card cardSO;
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}
