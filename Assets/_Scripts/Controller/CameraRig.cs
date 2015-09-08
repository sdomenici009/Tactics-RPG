using UnityEngine;
using System.Collections;

public class CameraRig : MonoBehaviour 
{
	public float speed = 3f;
	public Transform follow;
	Transform _transform;

	private Vector3 target;
	private float zoomFactor = 5;

	[SerializeField] private bool rotate;
	[SerializeField] private float rotationSpeed;
	private float yRotationOffset;
	private Quaternion targetRotation;
	
	void Awake ()
	{
		_transform = transform;
	}
	
	void Update ()
	{
		if(Input.mouseScrollDelta.y != 0)
		{
			zoomFactor -= Input.mouseScrollDelta.y/5f;
		}

		if(rotate)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				yRotationOffset -= 90;
			}

			if(Input.GetKeyDown(KeyCode.Q))
			{
				yRotationOffset += 90;
			}

			targetRotation = Quaternion.Euler(new Vector3(0, 45 + yRotationOffset, 0));

			_transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}

		Camera.main.orthographicSize = zoomFactor;

		if (follow)
			_transform.position = Vector3.Lerp(_transform.position, follow.position, speed * Time.deltaTime);
	}
}