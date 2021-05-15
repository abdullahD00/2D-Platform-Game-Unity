using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] Text TxtTimeValue;
    [SerializeField] float Timee;
    private bool GameActive;
    // Start is called before the first frame update
    void Start()
    {
        GameActive = true;
        TxtTimeValue.text = Timee.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameActive==true)
        {
        Timee -= Time.deltaTime;
        TxtTimeValue.text = ((int)Timee).ToString();
        }
        
        if (Timee<0)
        {

            GameActive = false;
            Timee = 60;
           gameObject.GetComponent<PlayerController>().Die();
            
        }
    }
}
