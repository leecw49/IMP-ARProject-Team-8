using UnityEngine;

public class Heal : Action
{
    public int amount;

    public Heal(int amount, GameObject icon = null) : base(icon)
    {
        this.amount = amount;
    }
}
