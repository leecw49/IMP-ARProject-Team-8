using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CardUIManager : MonoBehaviour
{
    public static CardUIManager Inst { get; private set; }

    [Header("Prefabs & Containers")]
    public GameObject cardUIPrefab;
    public RectTransform handArea;
    public ItemSO itemSO;
    public int initialCardCount = 3;

    private List<RectTransform> spawnedCards = new List<RectTransform>();
    private List<Item> itemBuffer = new List<Item>();

    private void Awake()
    {
        Inst = this;
    }

    public void DrawNewDeck()
    {
        SetupBuffer();
        for (int i = 0; i < initialCardCount; i++)
        {
            SpawnRandomCard();
        }
        UpdateRoundAlignment();
    }

    public void DestroySpwanedCards()
    {
        foreach (var rt in spawnedCards)
        {
            Destroy(rt.gameObject);
        }
        spawnedCards.Clear();
    }

    private void SetupBuffer()
    {
        itemBuffer.Clear();
        foreach (var item in itemSO.items)
        {
            for (int j = 0; j < item.frequency; j++)
            {
                itemBuffer.Add(item);
            }
        }
    }

    public void SpawnRandomCard()
    {
        if (itemBuffer.Count == 0) { SetupBuffer(); }

        int idx = Random.Range(0, itemBuffer.Count);
        Item selected = itemBuffer[idx];
        itemBuffer.RemoveAt(idx);
        SpawnCard(selected);
    }

    /// <summary>
    /// Instantiate and initialize a UI card
    /// </summary>
    public void SpawnCard(Item item)
    {
        GameObject go = Instantiate(cardUIPrefab, handArea);
        RectTransform rt = go.GetComponent<RectTransform>();
        spawnedCards.Add(rt);

        CardUI ui = go.GetComponent<CardUI>();
        ui.Initialize(item, OnCardClicked);
    }

    void OnCardClicked(CardUI clickedCard)
    {
        Debug.Log($"Clicked card: {clickedCard.GetItem().name}");

        foreach (var rt in spawnedCards)
        {
            CardUI cardUI = rt.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.SetHighlight(cardUI == clickedCard);
                if (cardUI == clickedCard)
                {
                    cardUI.AnimateUP();
                }
                else
                {
                    cardUI.AnimateDown();
                }
            }
        }

        DOVirtual.DelayedCall(0.6f, () => DestroySpwanedCards());

        // TODO: battle logic trigger
    }

    public void OnCardClickedExternally(CardUI clickedCard)
    {
        Debug.Log($"Clicked card: {clickedCard.GetItem().name}");

        foreach (var rt in spawnedCards)
        {
            CardUI cardUI = rt.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.SetHighlight(cardUI == clickedCard);
                if (cardUI == clickedCard)
                {
                    cardUI.AnimateUP();
                }
                else
                {
                    cardUI.AnimateDown();
                }
            }
        }

        DOVirtual.DelayedCall(0.6f, () => DestroySpwanedCards());

        // TODO: battle logic trigger
        Card cardSO = clickedCard.GetItem().cardSO;
        BattleSystem.Inst.UseCard(cardSO);
    }

    /// <summay>
    /// Position spawnedCards in a circular arc within handArea
    /// </summay>
    [ContextMenu("Update Round Alignment")]
    public void UpdateRoundAlignment()
    {
        int count = spawnedCards.Count;
        if (count == 0) return;

        float radius = handArea.rect.width * 0.4f;
        float angleRange = 60f;         // total arc degree
        float startAngle = -angleRange / 2f;
        float angleStep = count > 1 ? angleRange / (count - 1) : 0;

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector2 pos = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)) * radius;
            Vector3 scale = Vector3.one;

            RectTransform rt = spawnedCards[i];
            rt.DOLocalMove(pos, 0.5f).SetEase(Ease.OutBack);
            rt.localRotation = Quaternion.Euler(0, 0, -angle);
            rt.DOScale(scale, 0.5f);
        }
    }

    // this is for testing cardUI
    public void ForTesting()
    {
        DestroySpwanedCards();
        DrawNewDeck();
    }
}