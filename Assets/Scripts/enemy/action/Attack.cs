using UnityEngine;

public class Attack : Action
{
    public int amount;

    public Attack(int amount, GameObject icon = null) : base(icon)
    {
        this.amount = amount;
    }
}
