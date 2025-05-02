using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class CardTouchHighlighter : MonoBehaviour
{
    private CardUI highlightedCard = null;

    void Update()
    {
        var touches = Touch.activeTouches;

        if (touches.Count == 0) return;

        if (touches.Count == 1 && (touches[0].phase == TouchPhase.Began || touches[0].phase == TouchPhase.Moved || touches[0].phase == TouchPhase.Stationary))
        {
            CardUI currentCard = GetCardUnderTouch(touches[0].screenPosition);

            if (currentCard != highlightedCard)
            {
                if (highlightedCard != null) { highlightedCard.SetHighlight(false); }

                highlightedCard = currentCard;
                if (highlightedCard != null) { highlightedCard.SetHighlight(true); }
            }
        }
        else if (touches.Count == 1 && (touches[0].phase == TouchPhase.Ended || touches[0].phase == TouchPhase.Canceled))
        {
            if (highlightedCard != null)
            {
                highlightedCard.SetHighlight(false);
                CardUIManager.Inst.OnCardClickedExternally(highlightedCard);
                highlightedCard = null;
            }
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private CardUI GetCardUnderTouch(Vector2 screenPos)
    {
        PointerEventData data = new PointerEventData(EventSystem.current)
        {
            position = screenPos
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        foreach (var ray in results)
        {
            CardUI card = ray.gameObject.GetComponentInParent<CardUI>();
            if (card != null) { return card; }
        }
        return null;
    }
}