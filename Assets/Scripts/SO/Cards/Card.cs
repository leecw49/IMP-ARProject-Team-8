using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public abstract class Card : ScriptableObject
{
    public abstract void UseEffect(Player player, Enemy enemy);
}
