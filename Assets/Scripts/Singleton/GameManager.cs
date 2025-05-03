using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Enemy enemy;

    public GameObject selectedEnemyPrefab;

    public int enemyKilled = 0;

    public void ResetPlayer()
    {
        enemyKilled = 0;
        Player.Instance.hp = Player.Instance.maxHp;
    }

    public void SceneButton_1()
    {
        SceneManager.LoadScene(1);
    }
}
