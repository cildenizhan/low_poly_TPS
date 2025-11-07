using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock : Silah
{

    public bool tufekmi;

    public GameObject mermiPrefab;

    public override void Update()
    {
        base.Update();

        if (tufekmi && Input.GetMouseButton(0))
        {
            TryShoot();
        }
        if (!tufekmi && Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            TryReload();
        }
    }

    public override void Shoot()
    {
        
        GameObject mermi = Instantiate(mermiPrefab);
        
        mermi.transform.parent = GameObject.Find("Mermiler").transform;

        mermi.transform.position = transform.Find("Namlu").position;
        mermi.GetComponentInChildren<TrailRenderer>().Clear();
        mermi.transform.GetChild(0).GetComponent<TrailRenderer>().enabled = true;
        mermi.transform.GetComponent<MeshRenderer>().enabled = true;
        mermi.GetComponent<MermiKod>().MermiHasari = data.mermiHasari;



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit,data.mermininKatedecegiMesafe))
        {
            mermi.GetComponent<MermiKod>().TargetPosition = raycastHit.point;
            Debug.Log(data.silahAdi + "hit " + raycastHit.collider.name);
        }
        else
        {
            mermi.GetComponent<MermiKod>().TargetPosition = Camera.main.transform.position + Camera.main.transform.forward * 300f;
        }

    }
}
