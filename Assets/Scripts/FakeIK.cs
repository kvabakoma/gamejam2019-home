using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeIK : MonoBehaviour
{

    public Vector3 startRotation;
    public GameObject bone;
    public float maxAngle = 45f;

    // Start is called before the first frame update
    void Start()
    {
        bone.transform.localEulerAngles = new Vector3();
        startRotation = bone.transform.localEulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x != 0.0f || y != 0.0f) {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            Vector3 controllerAngle = new Vector3( 
                angle + 180,
                startRotation.y,
                startRotation.z
            );
            bone.transform.localEulerAngles = controllerAngle;
        }
 
    }
}
