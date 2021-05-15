using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject Effect;
   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag!="Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag=="Enemy")
        {
            Destroy(collision.gameObject);
            Instantiate(Effect, collision.gameObject.transform.position, Quaternion.identity);

            GameObject.Find("LevelManager").GetComponent<LevelManager>().AddScore(100);



        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
