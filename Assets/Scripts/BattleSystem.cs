using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Inst {  get; private set; }

    public Player player;
    public Enemy enemy;
    //public GameObject Player;
   // public GameObject Enemy;

    private Turn turn = Turn.PlayerWait;

    private Texture2D _redTexture, _grayTexture;
    private SoundPlayer _soundPlayer;

    [Header("Result Images")]
    public GameObject winImage;
    public GameObject loseImage;

    private float tick;

    // test용. 나중에 수정
    public TextMeshProUGUI playerHpTMP;
    public TextMeshProUGUI enemyHpTMP;

    private void Awake()
    {
        Inst = this;
        Init();
    }

    private void Init()
    {
        player = GameManager.Instance.player;

        // test용. 나중에 수정
        enemy = FindAnyObjectByType<Enemy>();
    }

    private void Start()
    {
        //_soundPlayer = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundPlayer>();

        //enemy.NewAction();
        //player.BackupCards();

        SpawnPlayerCards();

        if (winImage != null) winImage.SetActive(false);
        if (loseImage != null) loseImage.SetActive(false);
    }

    private void Update()
    {
        if (turn == Turn.PlayerAnimation)
        {
            tick += Time.deltaTime;

            if (tick >= 1)
            {
                tick = 0;
                NextTurn();
            }
        } else if (turn == Turn.EnemyWait)
        {
            NextTurn();
        } else if (turn == Turn.EnemyAnimation)
        {
            tick += Time.deltaTime;

            if (tick >= 1)
            {
                tick = 0;
                NextTurn();
            }
        }

        //test용. 나중에 수정
        playerHpTMP.text = "PlayerHP: " + player.hp.ToString();
        enemyHpTMP.text = "EnemyHP: " + enemy.hp.ToString();
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
                turn = Turn.PlayerAnimation;
                break;
            case Turn.PlayerAnimation:
                turn = Turn.EnemyWait;
                break;
            case Turn.EnemyWait:
                /*
                if (enemy.action is Attack attackAction)
                {
                    player.hp -= attackAction.amount;
                    _soundPlayer.punch.Play();
                }
                else if (enemy.action is Heal healAction)
                {
                    enemy.hp = Math.Min(enemy.hp + healAction.amount, enemy.maxHp);
                }
                */
                turn = Turn.EnemyAnimation;
                break;
            case Turn.EnemyAnimation:
                //enemy.NewAction();
                player.TakeDamage(3);
                turn = Turn.PlayerWait;
                SpawnPlayerCards();
                break;
        }
    }

    private void EndBattle()
    {
        if (enemy.hp <= 0)
        {
            if (winImage != null) winImage.SetActive(true);

            //player.ResetCards();
            //CardType randomType = (CardType)UnityEngine.Random.Range(0, 3);
            //player.AddCardReward(randomType);
        }
        else if (player.hp <= 0)
        {
            if (loseImage != null) loseImage.SetActive(true);

            //player.ResetCards();
        }
    }

    private void SpawnPlayerCards()
    {
        CardUIManager.Inst.DrawNewDeck();
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
    }
}

