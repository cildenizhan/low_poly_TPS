using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraKontrol : MonoBehaviour
{

    
    public Transform player;

    public Transform Ori;

    public CameraStyle currentStyle;

    public Transform AimBakmaYonu;

    public enum CameraStyle
    {
        Basit,
        AimModu
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
       

        if (currentStyle == CameraStyle.Basit)
        {
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);

            Ori.forward = viewDir.normalized;

            float horizontalIn = Input.GetAxis("Horizontal");
            float verticalIn = Input.GetAxis("Vertical");

            Vector3 inputDir = Ori.forward * verticalIn + Ori.right * horizontalIn;

            if (inputDir != Vector3.zero)
            {
                player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * 40);
            }
        }
        else if (currentStyle == CameraStyle.AimModu)
        {
            Vector3 dirAimBakmaYonu = AimBakmaYonu.position - new Vector3(transform.position.x, AimBakmaYonu.position.y, transform.position.z);

            Ori.forward = dirAimBakmaYonu.normalized;

            

            player.forward = Vector3.Slerp(player.forward, dirAimBakmaYonu.normalized, Time.deltaTime * 40);
        }
    }
}
