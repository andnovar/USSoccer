// prefrontal cortex -- http://prefrontalcortex.de
// Full Android Sensor Access for Unity3D
// Contact:
// 		contact@prefrontalcortex.de

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinimalSensorCamera : MonoBehaviour {

    public Text alt;

	// Use this for initialization
	void Start () {
		// you can use the API directly:
		// Sensor.Activate(Sensor.Type.RotationVector);
		
		// or you can use the SensorHelper, which has built-in fallback to less accurate but more common sensors:
		SensorHelper.ActivateRotation();

		useGUILayout = false;
	}
	
	// Update is called once per frame
	void Update () {
        // direct Sensor usage:
        // transform.rotation = Sensor.rotationQuaternion; --- is the same as Sensor.QuaternionFromRotationVector(Sensor.rotationVector);

        // Helper with fallback:
        //Vector3 rot = SensorHelper.rotation.eulerAngles;
        //Debug.Log(rot);
        //transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
        transform.rotation = SensorHelper.rotation;
    }
}