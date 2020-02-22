using UnityEngine;
using System.Collections;
using Fove.Unity;

public class FOVECursor : MonoBehaviour
{
   	[SerializeField]
	public FoveInterface foveInterface;

    public GameObject cursor;

	// Use this for initialization
	void Start () {
        cursor = GameObject.Find("Cursor");
	}

	// Latepdate ensures that the object doesn't lag behind the user's head motion
	void Update() {

        Vector3 headRotation = foveInterface.transform.localEulerAngles;

        Vector3 headPosition = foveInterface.transform.position.normalized;

        this.transform.position = headPosition+foveInterface.transform.forward * 30;

        this.transform.localEulerAngles = headRotation;

        EyeRays eyeRays = foveInterface.GetGazeRays();

		Ray rightEyeRay = eyeRays.right;
        RaycastHit rightEyeRayhit;

        Ray leftEyeRay = eyeRays.left;
        RaycastHit leftEyeRayhit;
        
        if (Physics.Raycast(rightEyeRay, out rightEyeRayhit, Mathf.Infinity) && Physics.Raycast(leftEyeRay, out leftEyeRayhit, Mathf.Infinity))
        {
            Vector3 cursorPosition = (rightEyeRayhit.point + leftEyeRayhit.point) / 2;
            cursor.transform.position = cursorPosition;
        }

	}
}
