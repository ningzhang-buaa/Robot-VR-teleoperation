using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripperRot : MonoBehaviour
{
    private Transform m_Transform;
    // Start is called before the first frame update
    void Start()
    {
         m_Transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotation();
    }

    void Rotation()
    {
        float angle = this.transform.localEulerAngles.y;
        //print("angle:"+this.transform.localEulerAngles);
        if(angle>1&&angle<45)
        {
            if(Input.GetKey(KeyCode.JoystickButton2))
            {
                m_Transform.Rotate (Vector3.up, -1, Space.Self); 
            }
            if(Input.GetKey(KeyCode.JoystickButton3))
            {
                m_Transform.Rotate (Vector3.up, 1, Space.Self); 
            }
        }
        else if(angle<=1)
        {
            if(Input.GetKey(KeyCode.JoystickButton2))
            {
                m_Transform.Rotate (Vector3.up, -1, Space.Self); 
            }
        }
        else if(angle>=45&&angle<=46)
        {
            if(Input.GetKey(KeyCode.JoystickButton3))
            {
                m_Transform.Rotate (Vector3.up, 1, Space.Self); 
            }
        }
    }
}
