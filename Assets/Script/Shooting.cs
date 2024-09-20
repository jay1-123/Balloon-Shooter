using UnityEngine.SceneManagement;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera mainCamera;
    public int pointsPerBalloon = 10;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("hit");

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("first if");
            if (hit.collider.CompareTag("Balloon"))
            {
                Debug.Log("second if");
                // Balloon popped
                hit.collider.gameObject.SetActive(false);
                //ScoreManager.Instance.AddScore(10);  // Example score increment
                ScoreManager.Instance.AddScore(pointsPerBalloon);
            }
        }
    }
}