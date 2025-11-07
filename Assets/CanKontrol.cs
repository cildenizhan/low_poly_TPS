using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanKontrol : MonoBehaviour
{
    public float can = 100;

    public TextMeshProUGUI canSayac;
    public GameObject gameOverPanel;
    private void Update()
    {
        if (canSayac.text != "+" + can)
        {
            canSayac.text = "+" + can;
        }
    }

    public void HasarAlma(float Hasar)
    {
        can -= Hasar;

        if (can < 0)
        {
            can = 0;
        }

        if (can <= 0)
        {
            OyunBitir();
        }
    }

    private void OyunBitir()
    {
        Time.timeScale = 0f;

        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
