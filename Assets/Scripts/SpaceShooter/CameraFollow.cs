using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// camera will follow this object
	public Transform Target;
	
	//camera transform
	public Transform camTransform;
	
	// change this value to get desired smoothness
	public float SmoothTime = 0.3f;
 
	private void FixedUpdate()
	{
		// update position
		camTransform.position = Vector3.Lerp(Target.position, camTransform.position, SmoothTime);
 
		// update rotation
		camTransform.localRotation = Quaternion.Lerp(Target.rotation, camTransform.rotation, SmoothTime);
	}
}
