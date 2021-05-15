using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
   
    [SerializeField] Text TxtToplamPuan;
    [SerializeField] float CoinDonmeHizi;
    [SerializeField] float CoinDonmeHiziDegeri;
    private void Update()
    {
        transform.Rotate(new Vector3(0f, CoinDonmeHiziDegeri, 0f));
        //Coinlerin kendi etrafında dönmeleri
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        /*int ToplamDeger = int.Parse(TxtToplamPuan.text);
        ToplamDeger = ToplamDeger + 50;
        TxtToplamPuan.text = ToplamDeger.ToString();*/
        GameObject.Find("LevelManager").GetComponent<LevelManager>().AddScore(50);

    }
}
