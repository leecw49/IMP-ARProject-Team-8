using UnityEngine;

[System.Serializable]
public class EnemyAcitonList
{
    public int actionNum; // Probably not necessary
    public string actionName; // Probably not necessary
    public EnemyActionSO actonScript;
}

[CreateAssetMenu(fileName = "ActonListSO", menuName = "Scriptable Objects/ActonListSO")]
public class ActonListSO : ScriptableObject
{
    public EnemyAcitonList[] enemyAcitonLists;
}
