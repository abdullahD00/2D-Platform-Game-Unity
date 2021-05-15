using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform PlayerTransform;
    [SerializeField] float MinX;
    [SerializeField] float MaxX;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(PlayerTransform.position.x, MinX, MaxX),transform.position.y, transform.position.z);
        
    }
}
