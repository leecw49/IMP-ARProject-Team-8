using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    public Enemy enemy;

    public GameObject selectedEnemyPrefab;

    public int enemyKilled = 0;
    public GameObject mainMenuCanvas;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void ResetPlayer()
    {
        enemyKilled = 0;
        Player.Instance.hp = Player.Instance.maxHp;
    }

    public void SceneButton_1()
    {
        if (audioSource != null && buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);

        if (mainMenuCanvas != null)
            Destroy(mainMenuCanvas);

        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        if (audioSource != null && buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);

        if (mainMenuCanvas != null)
            Destroy(mainMenuCanvas);
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

}
