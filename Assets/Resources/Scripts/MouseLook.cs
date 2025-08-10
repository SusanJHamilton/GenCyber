using UnityEngine;
using TMPro;

// Daniel Flanigan, 2014
// This is a combined mouse look and camera move script.
// The cam move script is by: Francis R. Griffiths-Keam

public class MouseLook : MonoBehaviour
{
	Vector2 _mouseAbsolute;
	Vector2 _smoothMouse;
	[Space (20)]
	[Header ("Mouse Look Settings :")]
	public Vector2
		clampInDegrees = new Vector2 (360, 180);
	
	//public bool lockCursor;
	public CursorLockMode lockCursor;
	
	public Vector2 sensitivity = new Vector2 (2, 2);
	public Vector2 smoothing = new Vector2 (3, 3);
	public Vector2 targetDirection;
	public Vector2 targetCharacterDirection;
	
	// Assign this if there's a parent object controlling motion, such as a Character Controller.
	// Yaw rotation will affect this object instead of the camera if set.
	public GameObject characterBody;
	
	[Space (20)]
	[Header ("Camera Move Settings :")]
	
	public float acceleration = 1.0f;
	public float maxSpeed = 5;
	public float dampingSpeed = 0.2f;
	
	public KeyCode fwdKey = KeyCode.W;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode backKey = KeyCode.S;
	public KeyCode rightKey = KeyCode.D;
	private float speedX, speedZ=0;
	public GameObject avatar;
	public bool isInputEnabled = true;

    public bool fwdKeyAlt = false;
    public bool leftKeyAlt = false;
    public bool backKeyAlt = false;
    public bool rightKeyAlt = false;

    public void setXMouseSensitivity(float x){
		sensitivity = new Vector2(x, sensitivity.y);
	}

	public void setYMouseSensitivity(float y){
		sensitivity = new Vector2(sensitivity.x, y);
	}

	void Start ()
	{
		// Set target direction to the camera's initial orientation.
		targetDirection = transform.localRotation.eulerAngles;
		
		// Set target direction for the character body to its inital state.
		if (characterBody)
			targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
	}
	
	void Update ()
	{
		//if (!isInputEnabled)
		//	return;
		
		//when not editor mode
		// #if !UNITY_EDITOR
		// if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
		// 	return;
		// #endif

		// //block when the cursor is on UI
		// if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		// 	return;
		
		// //block when the focus is on the input field
		// if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
		// 	return;

		// Ensure the cursor is always locked when set
		//Screen.lockCursor = lockCursor;
		Cursor.lockState = lockCursor;

		var mouseDelta = new Vector2(0, 0);

        //only when the right mouse button is pressed
        if (Input.GetMouseButton(1) == true && isInputEnabled)
        {
            // Get raw mouse input for a cleaner reading on more sensitive mice.
            mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }
		
		// Allow the script to clamp based on a desired target value.
		var targetOrientation = Quaternion.Euler (targetDirection);
		var targetCharacterOrientation = Quaternion.Euler (targetCharacterDirection);

		
		
		// Scale input against the sensitivity setting and multiply that against the smoothing value.
		mouseDelta = Vector2.Scale (mouseDelta, new Vector2 (sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
		
		// Interpolate mouse movement over time to apply smoothing delta.
		_smoothMouse.x = Mathf.Lerp (_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
		_smoothMouse.y = Mathf.Lerp (_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);
		
		// Find the absolute mouse movement value from point zero.
		_mouseAbsolute += _smoothMouse;
		
		// Clamp and apply the local x value first, so as not to be affected by world transforms.
		if (clampInDegrees.x < 360)
			_mouseAbsolute.x = Mathf.Clamp (_mouseAbsolute.x, -clampInDegrees.x * 0.2f, clampInDegrees.x * 0.2f);
		
		var xRotation = Quaternion.AngleAxis (-_mouseAbsolute.y, targetOrientation * Vector3.right);
		transform.localRotation = xRotation;
		
		// Then clamp and apply the global y value.
		if (clampInDegrees.y < 360)
			_mouseAbsolute.y = Mathf.Clamp (_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);
		
		transform.localRotation *= targetOrientation;
		
		// If there's a character body that acts as a parent to the camera
		if (characterBody) {
			var yRotation = Quaternion.AngleAxis (_mouseAbsolute.x, characterBody.transform.up);
			characterBody.transform.localRotation = yRotation;
			characterBody.transform.localRotation *= targetCharacterOrientation;
		} else {
			var yRotation = Quaternion.AngleAxis (_mouseAbsolute.x, transform.InverseTransformDirection (Vector3.up));
			transform.localRotation *= yRotation;
        }
    }

	void FixedUpdate(){


        //only when mouse is in the game window
        // #if !UNITY_EDITOR
        // if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        // 	return;
        // #endif

        // if (Input.GetKey (rightKey)) {
        // 	speedX += acceleration * Time.deltaTime;
        // }
        // else if (Input.GetKey (leftKey)) {
        // 	speedX -= acceleration * Time.deltaTime;
        // }
        // if (Input.GetKey (backKey)) {
        // 	speedZ -= acceleration * Time.deltaTime;
        // } else if (Input.GetKey (fwdKey)) {
        // 	speedZ += acceleration * Time.deltaTime;
        // }

        // speedX = Mathf.Lerp( speedX,0,dampingSpeed * Time.deltaTime);
        // speedZ = Mathf.Lerp( speedZ,0,dampingSpeed * Time.deltaTime);

        // speedX = Mathf.Clamp( speedX,-maxSpeed*Time.deltaTime, maxSpeed*Time.deltaTime);
        // speedZ = Mathf.Clamp( speedZ,-maxSpeed*Time.deltaTime, maxSpeed*Time.deltaTime);

        // transform.position = transform.TransformPoint( new Vector3( speedX,0,speedZ) );

        //block when the cursor is on UI
        // if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        // 	return;

        //alt movement stuffs

        if (rightKeyAlt)
        {
            avatar.transform.position += avatar.transform.right * Time.deltaTime * maxSpeed;
        }
        else if (leftKeyAlt)
        {
            avatar.transform.position -= avatar.transform.right * Time.deltaTime * maxSpeed;
        }
        if (backKeyAlt)
        {
            avatar.transform.position -= avatar.transform.forward * Time.deltaTime * maxSpeed;
        }
        else if (fwdKeyAlt)
        {
            avatar.transform.position += avatar.transform.forward * Time.deltaTime * maxSpeed;
        }

        if (!isInputEnabled)
			return;
		
		//block when the focus is on the input field
		if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
			return;


		//move the avatar when the key WASD is pressed
		if (Input.GetKey (rightKey)) {
			avatar.transform.position += avatar.transform.right * Time.deltaTime * maxSpeed;
		}
		else if (Input.GetKey (leftKey)) {
			avatar.transform.position -= avatar.transform.right * Time.deltaTime * maxSpeed;
		}
		if (Input.GetKey (backKey)) {
			avatar.transform.position -= avatar.transform.forward * Time.deltaTime * maxSpeed;
		} else if (Input.GetKey (fwdKey)) {
			avatar.transform.position += avatar.transform.forward * Time.deltaTime * maxSpeed;
		}
    }

}
