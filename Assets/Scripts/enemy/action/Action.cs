using UnityEngine;

public abstract class Action
{
    protected GameObject icon;

    public Action(GameObject icon = null)
    {
        this.icon = icon;
    }

    public GameObject getIcon()
    {
        return icon;
    }
}
