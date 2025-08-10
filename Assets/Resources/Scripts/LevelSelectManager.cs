using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public Lobby lobbyScript; // Lobby script GameObject here
    public GameObject levelSelectUI;
    public GameObject mainUI;
    public GameObject player; // drag the player
    private MoveToPortal mover;
    public DialogueManager dialogueManager;

    void Start()
    {
       Debug.Log("LevelSelectManager Start() called");
       if (player == null)
        {
            player = GameObject.FindWithTag("Player"); // Make sure your player has the tag!
            mover = player.GetComponent<MoveToPortal>();
        }

        if (player != null)
        {
            mover = player.GetComponent<MoveToPortal>();
        }
        else
        {
            Debug.LogError("Player not found in scene!");
        }
    }

    public void SelectLevel(string sceneName)
    {
        Debug.Log("Level selected: " + sceneName);
        lobbyScript.SetSceneToLoad(sceneName);
        lobbyScript.EnablePortal();
        levelSelectUI.SetActive(false); // Hide UI after selection
        //if (mainUI != null) mainUI.SetActive(true);

       dialogueManager.ShowHintWithContinue(
       "Now, Operative... step into the center of the light. Take a breath, clear your mind, and let the system guide your transition.",
        () =>
        {
            // After player clicks "Continue"
            if (mover != null)
            {
                mover.StartMovingToPortal();
            }
            else
            {
                Debug.LogError("mover is not assigned!");
            }
        }
        );
    }
    public void CloseUI()
    {
        levelSelectUI.SetActive(false);
        if (mainUI != null) mainUI.SetActive(true);
    }

    void Update()
    {
        if (levelSelectUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }
}
