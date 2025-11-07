using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKod : MonoBehaviour
{

    public Vector3 TargetPosition;

    public float MermiHasari;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetPosition == null)
            return;
        transform.position = Vector3.Lerp(transform.position,TargetPosition,Time.deltaTime * 50);

        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.layer == 7)
            {
                GameObject.Find("Oyuncu").GetComponent<CanKontrol>().HasarAlma(MermiHasari);
            }
            if (hit.gameObject.layer == 8)
            {
                hit.GetComponent<DusmanYapayZeka>().TakeDamage(MermiHasari);
            }
            Destroy(gameObject);
        }
    }


    
}
