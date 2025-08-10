using UnityEngine;

public class ScreenInteraction : MonoBehaviour
{
    public GameObject levelSelectUI; // UI Canvas with buttons
    private Camera playerCamera;
    public GameObject mainUI;

    void Start()
    {
        playerCamera = Camera.main;
        levelSelectUI.SetActive(false); // Hide initially
        if (mainUI != null) mainUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    Debug.Log("Screen clicked - showing level select UI");
                    levelSelectUI.SetActive(true);
                    if (mainUI != null) mainUI.SetActive(false);
                }
            }
        }
    }
}
