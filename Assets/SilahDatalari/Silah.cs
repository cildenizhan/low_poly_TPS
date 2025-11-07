using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Silah : MonoBehaviour
{
    public SilahData data;

    public Transform player;

    private Animator anim;

    public Transform MainCam;
    

    float suankiMermi = 0f;
    float cooldownBitis = 0f;

    public bool isReloading = false;

    private TextMeshProUGUI mermiSayac;
    public AudioClip sarjorSesi;
    public AudioClip sikmasesi;
    private AudioSource audioSource;

    private void Start()
    {
        suankiMermi = data.sarjorBoyutu;
        anim = player.GetComponent<Animator>();
        mermiSayac = GameObject.Find("Canvas").transform.Find("MermiSayac").GetComponent<TextMeshProUGUI>();
        mermiSayac.text = suankiMermi.ToString() + "/" + data.sarjorBoyutu.ToString() ;
        if (player != null)
        {
            audioSource = player.GetComponent<AudioSource>();
        }
    }

    public virtual void Update()
    {
        if (!isReloading)
        mermiSayac.text = suankiMermi.ToString() + "/" + data.sarjorBoyutu.ToString();
    }

    public void TryReload()
    {
        if (!isReloading && suankiMermi < data.sarjorBoyutu)
        {
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        if (audioSource != null && sarjorSesi != null)
        {
            audioSource.PlayOneShot(sarjorSesi);
        }

        Debug.Log("reloading");
        mermiSayac.text = "-" + "/" + data.sarjorBoyutu.ToString();
        yield return new WaitForSeconds(data.doldurmaSuresi);

        suankiMermi = data.sarjorBoyutu;
        mermiSayac.text = suankiMermi.ToString() + "/" + data.sarjorBoyutu.ToString();

        isReloading = false;

        Debug.Log("gun is reloaded");
    }


    public void TryShoot()
    {
        if (isReloading)
        {
            return;
        }
        if (suankiMermi <= 0)
        {
            Debug.Log("sarjorde mermi yok");
            return;
        }

        if (Time.time >= cooldownBitis)
        {
            cooldownBitis = Time.time + (1/data.atisHizi); //1 i atis hizina bolup hizi hesapliyor.
            HandleShoot();
        }
    }

    public void HandleShoot()
    {
        suankiMermi--;
        mermiSayac.text = suankiMermi.ToString() + "/" + data.sarjorBoyutu.ToString();
        anim.Play("GlockPatlama",3,0f);
        if (audioSource != null && sikmasesi != null)
        {
            audioSource.PlayOneShot(sikmasesi);
        }

        anim.Play("Sikma",3,0f);

        Debug.Log("silah patladi suanki mermi sayisi = " + suankiMermi);
        Shoot();
    }

    public abstract void Shoot();
}
    