using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {
	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public float CurrentSpeed = 0;

	private float BrakeForce;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 1000;

	public float TopSpeed = 500f;
	
	public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical") * 10;
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
		// BrakeForce needs to be a order of magnitude higher than motorforce in order to overcome interia
		BrakeForce = motorForce * motorForce;

		CurrentSpeed = 2 * 22 / 7 * frontDriverW.radius * frontDriverW.rpm * 60 / 1000;

		if (CurrentSpeed < TopSpeed) {

			if (m_verticalInput <= 0) {
				//apply reverse
				rearDriverW.motorTorque = m_verticalInput * -1 * motorForce * -1;
				rearPassengerW.motorTorque = m_verticalInput * -1 * motorForce * -1;
				frontDriverW.motorTorque = m_verticalInput * -1 * motorForce * -1;
				frontPassengerW.motorTorque = m_verticalInput * -1 * motorForce * -1;
			} else {
				rearDriverW.motorTorque = m_verticalInput * motorForce;
				rearPassengerW.motorTorque = m_verticalInput * motorForce;
				frontDriverW.motorTorque = m_verticalInput * motorForce;
				frontPassengerW.motorTorque = m_verticalInput * motorForce;
			}
		}

		// if spacebar is pressed, apply brakes
		if (Input.GetKey(KeyCode.Space)) {
			//remove motor force
			rearDriverW.motorTorque = 0;
			rearPassengerW.motorTorque = 0;
			frontDriverW.motorTorque = 0;
			frontPassengerW.motorTorque = 0;

			// apply brakes
			rearDriverW.brakeTorque = BrakeForce;
			rearPassengerW.brakeTorque = BrakeForce;
			frontDriverW.brakeTorque = BrakeForce;
			frontPassengerW.brakeTorque = BrakeForce;
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			rearDriverW.brakeTorque = 0;
			rearPassengerW.brakeTorque = 0;
			frontDriverW.brakeTorque = 0;
			frontPassengerW.brakeTorque = 0;
		}

		// if down/'s' is pressed apply brakes
		if (m_verticalInput == 0) {
			// apply brakes
			rearDriverW.brakeTorque = BrakeForce;
			rearPassengerW.brakeTorque = BrakeForce;
			frontDriverW.brakeTorque = BrakeForce;
			frontPassengerW.brakeTorque = BrakeForce;
		} else {
			rearDriverW.brakeTorque = 0;
			rearPassengerW.brakeTorque = 0;
			frontDriverW.brakeTorque = 0;
			frontPassengerW.brakeTorque = 0;
		}
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}
}
