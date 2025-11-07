using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    bool sa = false;
    private void Update()
    {
        if (transform.rotation.eulerAngles.x < 45 && !sa)
        {
            transform.Rotate(new Vector3(1,0,0));
            if (transform.rotation.eulerAngles.z > 45)
            {
                sa = true;
            }
        }
    }
}
