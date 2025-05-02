using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject gameCam;
    public Player player;
    public Enemy enemy;

    public GameObject battleSystem;

    public GameObject menuPanel;
    public GameObject gamePanel;

    public RectTransform EnemyGroup;
    public RectTransform EnemyHealthBar;

    public RectTransform PlayerGroup;
    public RectTransform PlayerHealthBar;


    public void GameStart()
    {
        gameCam.SetActive(true);

        menuPanel.SetActive(false);

        player.gameObject.SetActive(true);
    }

    /*
    void LateUpdate()
    {
        if (enemy.maxHp > 0)
            EnemyHealthBar.localScale = new Vector3(enemy.hp / enemy.maxHp, 1, 1);
        else
            EnemyHealthBar.localScale = new Vector3(0, 1, 1);

        if (player.maxHp > 0)
            PlayerHealthBar.localScale = new Vector3(player.hp / player.maxHp, 1, 1);
        else
            PlayerHealthBar.localScale = new Vector3(0, 1, 1);
        

    }
    */

    public void NewBattle(GameObject enemy)
    {
        SceneManager.LoadScene("BattleScene");
        battleSystem.SetActive(true);
        battleSystem.GetComponent<BattleSystem>().enemy = enemy.GetComponent<Enemy>();
        gamePanel.SetActive(true);
    }

    // test용. 나중에 지울 것
    public void SceneButton_BattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
