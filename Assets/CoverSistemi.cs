using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSistemi : MonoBehaviour
{
    bool Cover,ileri;

    Animator anim;


    public float CoverDist;

    public GameObject TopCoverRay,CoverObj;

   

    private Vector3 CoverHitNoktasi;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Cover = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CoverSys();
    }

    public void CoverSys()
    {
        if (Cover == false)
        {
            RaycastHit hit;

            if (Physics.Raycast(TopCoverRay.transform.position, TopCoverRay.transform.forward, out hit, CoverDist))
            {
                if (hit.distance <= CoverDist && hit.collider.gameObject.tag == "Cover")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Cover = true;
                        CoverObj = hit.collider.gameObject;
                        ileri = true;
                        rb.isKinematic = true;
                        CoverHitNoktasi = hit.point;
                    }
                }
            }
        }
        else
        {
            if (ileri == true)
            {
                Vector3 hedefPozisyon = new Vector3(CoverHitNoktasi.x, 1.20f,CoverHitNoktasi.z);

                hedefPozisyon.z -= 1f;
                transform.position = Vector3.Slerp(transform.position, hedefPozisyon, Time.deltaTime * 40);
                StartCoroutine(zaman());

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("e");
                ileri = false;
                StartCoroutine(coverSilme());
                rb.isKinematic = false;
            }
        }
    }

    IEnumerator zaman()
    {
        yield return new WaitForSeconds(0.2f);
        ileri = false;
    }

    IEnumerator coverSilme()
    {
        yield return new WaitForSeconds(1);
        Cover = false;
    }

}
