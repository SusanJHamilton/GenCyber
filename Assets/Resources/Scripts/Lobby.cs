using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject mainUI;
    public DialogueManager dialogueManager;
    public GameObject levelSelectManager;
    public string levelToLoad = "Room1_1"; // Name of the scene to load
    public float requiredStayTime = 1f;

    private Camera playerCamera;
    private float stayTimer = 0f;
    private bool playerInTrigger = false;
    private bool portalEnabled = false;

    [Header("Robot Interaction")]
    public GameObject robotObject;
    public Transform playerTransform;
    public Transform robotReturnTarget;
    public float robotSpeed = 3f;
    public float robotStopDistance = 2f;

    private bool robotApproaching = false;
    private bool robotReturning = false;
    private bool robotTurningToFacePlayer = false;
    private bool robotIdle = false;
    private bool turningAfterReturn = false;
    private bool robotTurningAwayFromPlayer = false;



    public void SetSceneToLoad(string sceneName)
    {
        levelToLoad = sceneName;
    }

    public void EnablePortal()
    {
        portalEnabled = true;
        Debug.Log("Portal enabled!");
    }

    void Start()
    {
        playerCamera = Camera.main;
        Debug.Log("Lobby Entered!");

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); 
        }

        if (robotReturnTarget == null)
        {
            GameObject returnTargetGO = new GameObject("RobotReturnTarget");
            returnTargetGO.transform.position = robotObject.transform.position;
            robotReturnTarget = returnTargetGO.transform;
        }

        // Player immediately faces robot
        if (playerTransform != null && robotObject != null)
        {
            Vector3 lookPos = robotObject.transform.position;
            lookPos.y = playerTransform.position.y; // keep player rotation flat
            playerTransform.LookAt(lookPos);
        }

        // Robot starts facing away from player
        if (robotObject != null && playerTransform != null)
        {
            Vector3 awayDirection = robotObject.transform.position - playerTransform.position;
            awayDirection.y = 0f;
            if (awayDirection != Vector3.zero)
            {
                robotObject.transform.rotation = Quaternion.LookRotation(awayDirection);
            }
        }

        StartCoroutine(RobotIntro(2f));
    }


    IEnumerator FaceRobotAfterFrame()
    {
        yield return null; // wait 1 frame

        if (playerTransform != null && robotObject != null)
        {
            Vector3 lookPos = robotObject.transform.position;
            lookPos.y = playerTransform.position.y; // keep horizontal
            playerTransform.LookAt(lookPos);
            Debug.Log("Player rotated to face robot (after 1 frame)");
        }
    }


    IEnumerator RobotIntro(float delay)
    {
        yield return new WaitForSeconds(delay);
        robotTurningToFacePlayer = true; // Start turning to face player before approaching
    }

    void Update()
    {
        RobotMovement();

        if (playerInTrigger && portalEnabled)
        {
            stayTimer += Time.deltaTime;
            if (stayTimer >= requiredStayTime)
            {
                Debug.Log("Player stayed in trigger long enough. Teleporting.");
                TeleportToLevel();
                playerInTrigger = false; // Prevent repeated triggers
            }
        }
    }

    void RobotMovement()
    {
        if (robotIdle)
        {
            return;
        }
        if (robotTurningToFacePlayer)
        {
            bool doneTurning = SmoothLookAt(robotObject, playerTransform.position);
            if (doneTurning)
            {
                robotTurningToFacePlayer = false;

                if (turningAfterReturn)
                {
                    robotIdle = true;
                    turningAfterReturn = false;
                    //mainUI.SetActive(true);
                    levelSelectManager.SetActive(true);
                    Debug.Log("Robot finished turning after returning and is now idle");
                }
                else
                {
                    robotApproaching = true;
                    Debug.Log("Robot finished turning, now approaching player");
                }
            }
        }
        else if (robotApproaching)
        {
            float distance = Vector3.Distance(robotObject.transform.position, playerTransform.position);
            if (distance > robotStopDistance)
            {
                MoveTowards(robotObject, playerTransform.position);
            }
            else
            {
                robotApproaching = false;
                Debug.Log("Robot reached player");

                StartCoroutine(ShowDialogueAfterDelay(1f));
             
            }
        }
        else if (robotTurningAwayFromPlayer)
        {
            // Get direction from robot to return point
            bool doneTurning = SmoothLookAt(robotObject, robotReturnTarget.position);
            if (doneTurning)
            {
                robotTurningAwayFromPlayer = false;
                robotReturning = true;
                Debug.Log("Robot finished turning away, now walking to return position");
            }
            return;
        }
        else if (robotReturning)
        {
            float distance = Vector3.Distance(robotObject.transform.position, robotReturnTarget.position);
            if (distance > 0.1f)
            {
                MoveTowards(robotObject, robotReturnTarget.position);
            }
            else
            {
                robotReturning = false;
                turningAfterReturn = true;
                Debug.Log("Robot returned to position");
                robotTurningToFacePlayer = true; // Turn to face player after return
            }
        }
        // robotIdle == true means robot stands still and does nothing
    }

    IEnumerator ShowDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (dialogueManager != null)
        {
            dialogueManager.StartConversation(); // This shows panel + types text
        }
        else
        {
            Debug.LogWarning("DialogueManager not assigned in Lobby script.");
        }
    }

    // Returns true when rotation is close enough to target
    bool SmoothLookAt(GameObject obj, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - obj.transform.position).normalized;
        direction.y = 0f; // Keep rotation on horizontal plane only

        if (direction == Vector3.zero) return true; // No need to rotate

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, 4f * Time.deltaTime);

        // Consider done if angle difference is small enough
        float angleDifference = Quaternion.Angle(obj.transform.rotation, targetRotation);
        return angleDifference < 1f;
    }

    void MoveTowards(GameObject obj, Vector3 target)
    {
        Vector3 direction = (target - obj.transform.position).normalized;
        obj.transform.position += direction * robotSpeed * Time.deltaTime;
        obj.transform.LookAt(target);
    }

    public void SendRobotBack()
    {
        //robotReturning = true;
        robotIdle = false;
        robotApproaching = false;
        robotTurningToFacePlayer = false;
        robotTurningAwayFromPlayer = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger area.");
            playerInTrigger = true;
            stayTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger area.");
            playerInTrigger = false;
            stayTimer = 0f;
        }
    }

    void TeleportToLevel()
    {
        Debug.Log("Teleporting to " + levelToLoad);
        SceneManager.LoadScene(levelToLoad);
    }
}
