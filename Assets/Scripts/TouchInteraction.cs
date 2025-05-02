using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Monster"))
                {
                    Debug.Log("Monster touched: " + hit.collider.gameObject.name);
                    SceneManager.LoadScene("BattleScene");
                }
            }
        }
    }
}
