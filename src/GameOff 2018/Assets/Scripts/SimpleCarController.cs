using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimpleCarController : MonoBehaviour {
	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public float CurrentSpeed = 0;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 1000;

	public float TopSpeed = 500f;

    private WheelCollider[] wheels;

    private void Awake()
    {
        wheels = new WheelCollider[] { frontDriverW, frontPassengerW, rearDriverW, rearPassengerW };
    }

    public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
        CurrentSpeed = 2 * 22 / 7 * frontDriverW.radius * frontDriverW.rpm * 60 / 1000;

        // BrakeForce needs to be a order of magnitude higher than motorforce in order to overcome interia
        float BrakeForce = 0;
        if (Input.GetKey(KeyCode.Space)) {
            BrakeForce = motorForce * motorForce;
        }

        float GasForce = 0;
		if (CurrentSpeed < TopSpeed) {
            GasForce = m_verticalInput * motorForce;
        }

        SetMotorTorque(GasForce);
        SetBrakeTorque(BrakeForce);
	}

    private void SetMotorTorque(float t) {
        foreach(WheelCollider w in wheels) {
            w.motorTorque = t;
        }
    }

    private void SetBrakeTorque(float t) {
        foreach (WheelCollider w in wheels) {
            w.brakeTorque = t;
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
