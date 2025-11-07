using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "YeniSilahDatasi",menuName ="Silah/Silah Verileri")]
public class SilahData : ScriptableObject
{
    public string silahAdi;

    public LayerMask targetLayerMask;

    [Header("Mermi atma degiskenleri")]

    public float atisHizi;
    public float mermininKatedecegiMesafe;
    public float mermiHasari;

    [Header("Mermi Doldurma degiskenleri")]
    public float sarjorBoyutu;
    public float doldurmaSuresi;
}
