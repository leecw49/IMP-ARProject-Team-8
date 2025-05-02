using UnityEngine;

[CreateAssetMenu(fileName = "New Defence Card", menuName = "Cards/Defence")]
public class DefenceCard : Card
{
    public override void UseEffect(Player player, Enemy enemy)
    {
        player.BlockAttack(1);
        Debug.Log($"[Defence Card] used! You'll block the next attack.");
        //throw new System.NotImplementedException();
    }
}
