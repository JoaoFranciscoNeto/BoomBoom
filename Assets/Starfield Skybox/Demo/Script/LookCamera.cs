using UnityEngine;
using System.Collections;

public class LookCamera : MonoBehaviour 
{
    public float SpeedNormal = 10.0f;
    public float SpeedFast   = 50.0f;

    public float MouseSensitivityX = 5.0f;
	public float MouseSensitivityY = 5.0f;
    
	float _rotY = 0.0f;
    
	void Start()
	{
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void Update()
	{	
        // rotation        
        if (Input.GetMouseButton(1)) 
        {
            float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSensitivityX;
            _rotY += Input.GetAxis("Mouse Y") * MouseSensitivityY;
            _rotY = Mathf.Clamp(_rotY, -89.5f, 89.5f);
            transform.localEulerAngles = new Vector3(-_rotY, rotX, 0.0f);
        }
		
		if (Input.GetKey(KeyCode.U))
		{
			gameObject.transform.localPosition = new Vector3(0.0f, 3500.0f, 0.0f);
		}

	}
}
