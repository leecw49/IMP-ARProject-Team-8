using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Inst {  get; private set; }

    public Player player;
    public Enemy enemy;

    private Turn turn = Turn.PlayerWait;
    private float tick;

    private Texture2D _redTexture, _grayTexture;

    [Header("Result Images")]
    public GameObject winImage;
    public GameObject loseImage;

    public TextMeshProUGUI playerHpTMP;
    public TextMeshProUGUI enemyHpTMP;
    public GameObject winButton;
    public GameObject loseButton;
    public GameObject EnemyKilledTMP;
    // for testing. Change this to icon.
    public TextMeshProUGUI enemyActionTMP;

    //BGM
    private AudioSource battleMusicSource;
    public AudioClip battleMusicClip;

    private void Awake()
    {
        Inst = this;
        Init();
    }

    private void Init()
    {
        player = Player.Instance;
        enemy = Enemy.Instance;
        //enemy = FindAnyObjectByType<Enemy>();
        enemy.ChangeActon();
        UpdateEnemyNextAction();
    }

    private void Start()
    {
        //_soundPlayer = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundPlayer>();

        //enemy.NewAction();

        SpawnPlayerCards();

        if (winImage != null) winImage.SetActive(false);
        if (loseImage != null) loseImage.SetActive(false);
        if (winButton != null) winButton.SetActive(false);
        if (loseButton != null) loseButton.SetActive(false);
        EnemyKilledTMP.SetActive(false);

        // 선택된 적 프리팹을 3D 공간의 중앙(카메라 앞) 위치에 생성
        if (GameManager.Instance.selectedEnemyPrefab != null)
        {
            Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
            Quaternion spawnRot = Quaternion.LookRotation(-Camera.main.transform.forward);

            GameObject enemyObj = Instantiate(GameManager.Instance.selectedEnemyPrefab, spawnPos, spawnRot);

            enemyObj.transform.localScale = Vector3.one * 0.5f; // 필요 시 조절

            //enemy = enemyObj.GetComponent<Enemy>();
        }

        battleMusicSource = gameObject.AddComponent<AudioSource>();
        battleMusicSource.clip = battleMusicClip;
        battleMusicSource.loop = true;
        battleMusicSource.playOnAwake = false;
        battleMusicSource.Play();


    }

    private void Update()
    {
        if ((turn == Turn.PlayerAnimation) || (turn == Turn.EnemyAnimation))
        {
            tick += Time.deltaTime;

            if (tick >= 1)
            {
                tick = 0;
                NextTurn();
            }
        } 
        else if (turn == Turn.EnemyWait)
        {
            NextTurn();
        }

        UpdateHpTMP();
    }

    public void UseCard(Card cardSO)
    {
        cardSO.UseEffect(player, enemy);

        NextTurn();
    }

    private void NextTurn()
    {
        if (player.hp <= 0 || enemy.hp <= 0)
        {
            EndBattle();
            return;
        }

        switch (turn)
        {
            case Turn.PlayerWait:
                enemy.ChangeActon();
                // for testing
                UpdateEnemyNextAction();
                turn = Turn.PlayerAnimation;
                break;
            case Turn.PlayerAnimation:
                turn = Turn.EnemyWait;
                break;
            case Turn.EnemyWait:
                // do we need this turn?
                turn = Turn.EnemyAnimation;
                break;
            case Turn.EnemyAnimation:
                enemy.DoAction(player, enemy);
                turn = Turn.PlayerWait;
                SpawnPlayerCards();
                break;
        }
    }

    private void EndBattle()
    {
        if (battleMusicSource != null && battleMusicSource.isPlaying)
        {
            battleMusicSource.Stop();
        }

        if (enemy.hp <= 0)
        {
            if (winImage != null) winImage.SetActive(true);

            if (winButton != null) winButton.SetActive(true);

            GameManager.Instance.enemyKilled++;
            ShowKillCount();
        }
        else if (player.hp <= 0)
        {
            if (loseImage != null) loseImage.SetActive(true);

            if (loseButton != null) loseButton.SetActive(true);

            ShowKillCount();
        }
    }

    private void ShowKillCount()
    {
        int killCount = GameManager.Instance.enemyKilled;
        EnemyKilledTMP.SetActive(true);

        string countText = "";
        if (killCount == 0) { countText = "You couldn't kill the enemy."; }
        else if (killCount == 1) { countText = "You killed one enemy."; }
        else { countText = $"You killed {killCount} enemies.";  }

        EnemyKilledTMP.GetComponent<TextMeshProUGUI>().text = countText;
    }

    public void Button_Win()
    {
        // SET Scene 1 to ARImageTrackingScene!!
        SceneManager.LoadScene(1);
        enemy.ResetEnemyHp(10);
    }

    public void Button_Lose()
    {
        // SET Scene 0 to MainMenuScene!!
        GameManager.Instance.ResetPlayer();
        enemy.ResetEnemyHp(10);
        SceneManager.LoadScene(0);
    }

    private void SpawnPlayerCards()
    {
        CardUIManager.Inst.DrawNewDeck();
    }

    private void UpdateHpTMP()
    {
        playerHpTMP.text = "Player HP: " + player.hp.ToString();
        enemyHpTMP.text = "Enemy HP: " + enemy.hp.ToString();
    }

    private void UpdateEnemyNextAction()
    {
        enemyActionTMP.text = "Enemy Next Action: " + enemy.GetCurrentActionName();
    }

    
    private void OnGUI()
    {
        float margin = 10;
        float xMin = margin;
        float yMin = margin;
        float width = (Screen.width - margin * 2) / 2;
        float height = 20;

        if (!_redTexture)
        {
            _redTexture = new Texture2D(1, 1);
            _redTexture.SetPixel(0, 0, Color.red);
            _redTexture.Apply();
        }

        if (!_grayTexture)
        {
            _grayTexture = new Texture2D(1, 1);
            _grayTexture.SetPixel(0, 0, Color.gray);
            _grayTexture.Apply();
        }

        GUI.skin.box.normal.background = _grayTexture;
        GUI.Box(new Rect(xMin, yMin, width, height), GUIContent.none);

        GUI.skin.box.normal.background = _redTexture;
        GUI.Box(new Rect(xMin, yMin, width * player.hp / player.maxHp, height), GUIContent.none);
        GUI.Box(new Rect(xMin, yMin + height + margin, width * enemy.hp / enemy.maxHp, height), GUIContent.none);
    }
    
}

