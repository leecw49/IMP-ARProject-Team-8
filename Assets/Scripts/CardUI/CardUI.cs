using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardUI : MonoBehaviour
{
    [Header("UI Components")]
    public RectTransform rectTransform;
    public Image cardFrame;
    public Image cardImage;
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI attackTMP;
    public TextMeshProUGUI descriptionTMP;
    public Button cardButton; // used for testing on computer. Disable on Mobile
    public Image highlightOverlay;

    private Item cardItem;
    private UnityEngine.Events.UnityAction<CardUI> clickAction;
    //private bool isFingerOver = false;

    /*
    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }
    
    private void OnEnable()
    {
        Touch.onFingerDown += OnFingerDown;
        Touch.onFingerUp += OnFIngerUp;
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= OnFingerDown;
        Touch.onFingerUp -= OnFIngerUp;
    }
    */

    /// <summary>
    /// Set UI fields from Item data
    /// </summary>
    public void Initialize(Item item, UnityEngine.Events.UnityAction<CardUI> onClick)
    {
        cardItem = item;
        cardImage.sprite = item.sprite;
        nameTMP.text = item.name;
        if (item.attackInt != 0) { attackTMP.text = item.attackInt.ToString(); }
        else { attackTMP.text = ""; }
        //effectTMP.text = item.effectInt.ToString();
        descriptionTMP.text = item.description;
        descriptionTMP.gameObject.SetActive(false);
        highlightOverlay.enabled = false;

        clickAction = onClick;
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(() => clickAction?.Invoke(this));
    }

    /*
    private void OnFingerDown(Finger finger)
    {
        Vector2 screeenPos = finger.screenPosition;
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screeenPos, null))
        {
            highlightOverlay.enabled = true;
            descriptionTMP.gameObject.SetActive(true);
            isFingerOver = true;
        }
    }

    private void OnFIngerUp(Finger finger)
    {
        // Vector2 screenPos = finger.screenPosition;
        if (isFingerOver)
        {
            highlightOverlay.enabled = false;
            descriptionTMP.gameObject.SetActive(false);
            isFingerOver = false;

            clickAction?.Invoke();
        }
    }
    */

    public Item GetItem()
    {
        return cardItem;
    }

    public void SetHighlight(bool active)
    {
        highlightOverlay.enabled = active;
    }

    public void AnimateUP()
    {
        rectTransform.DOLocalMoveY(rectTransform.localPosition.y + 150f, 0.5f).SetEase(Ease.OutBack);
    }

    public void AnimateDown()
    {
        rectTransform.DOLocalMoveY(rectTransform.localPosition.y - 200f, 0.5f).SetEase(Ease.InBack);
    }
}