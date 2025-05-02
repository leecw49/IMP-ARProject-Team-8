using UnityEngine;
using Random = UnityEngine.Random;

public class DefaultEnemy : Enemy
{
    public GameObject attackIcon, healIcon;

    public DefaultEnemy(int maxHp) : base(maxHp)
    {
        
    }

    public override Action NewAction()
    {
        if (hp * 2 < maxHp && Random.value < 0.5f)
        {
            action = new Heal(5, healIcon);
        }
        else
        {
            action = new Attack(5, attackIcon);
        }

        if (icon != null)
        {
            Destroy(icon);
        }
        var position = transform.position + new Vector3(0, 1, 0);
        Instantiate(action.getIcon(), position, transform.rotation);
        
        return action;
    }
}
