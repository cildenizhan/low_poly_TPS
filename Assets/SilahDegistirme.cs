using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SilahDegistirme : MonoBehaviour
{
    public Transform glock, ak;

    private Animator anim;

    public TextMeshProUGUI mermiSayac;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.Find("Rig 1").GetChild(2).GetComponent<TwoBoneIKConstraint>().weight = 0f;
            anim.SetBool("Tufek",false);
            glock.gameObject.SetActive(true);
            ak.gameObject.SetActive(false);
            glock.transform.GetComponent<Glock>().isReloading = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.Find("Rig 1").GetChild(2).GetComponent<TwoBoneIKConstraint>().weight = 1f;
            anim.SetBool("Tufek", true);
            ak.gameObject.SetActive(true);
            glock.gameObject.SetActive(false);
            ak.transform.GetComponent<Glock>().isReloading = false;
        }
    }

}
