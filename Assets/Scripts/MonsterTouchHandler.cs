using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class MonsterTouchHandler : MonoBehaviour
{
    void Update()
    {
        // Get all current active touches
        var touches = Touch.activeTouches;

        // Check if there's at least one touch and it's just beginning
        if (touches.Count > 0 && touches[0].phase == TouchPhase.Began)
        {
            // Get the position of the first touch on the screen
            Vector2 touchPos = touches[0].screenPosition;

            // Cast a ray from the camera to the touched position
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            // Perform a raycast and check if it hits a collider
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // If the touched object has the "Monster" tag, load the battle scene
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Monster"))
                {
                    SceneManager.LoadScene("BattleScene");
                }
            }
        }
    }

    private void OnEnable()
    {
        // Enable enhanced touch support when the script is active
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        // Disable enhanced touch support when the script is deactivated
        EnhancedTouchSupport.Disable();
    }
}
