using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class MonsterTouchHandler : MonoBehaviour
{
    void Update()
    {
        var touches = Touch.activeTouches;

        if (touches.Count > 0 && touches[0].phase == TouchPhase.Began)
        {
            Vector2 touchPos = touches[0].screenPosition;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Monster"))
                {
                    SceneManager.LoadScene("BattleScene");
                }
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
}
