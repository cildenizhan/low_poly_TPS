using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCode : MonoBehaviour
{

    public Transform nisanTopu;

    public Transform cinemaObject;

    private Animator anim;

    public Camera mainCam;

    public float yumsaklik;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit raycastHit,14))
        {
            nisanTopu.position = Vector3.Slerp(nisanTopu.position,raycastHit.point,yumsaklik * Time.deltaTime);
        }
        else
        {
            nisanTopu.position = Vector3.Slerp(nisanTopu.position,Camera.main.transform.position + Camera.main.transform.forward * 14f,yumsaklik * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("SagTik", true);
            cinemaObject.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 30;
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("SagTik", false);
            cinemaObject.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 45;
        }
    }
}
