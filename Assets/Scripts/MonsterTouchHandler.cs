using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterTouchHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Touch touch = Input.touches[0];
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Monster"))
                {
                    SceneManager.LoadScene("BattleScene");
                }
            }
        }
    }
}
