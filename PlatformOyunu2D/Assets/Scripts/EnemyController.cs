using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] bool OnGroud;
    [SerializeField] float SpeedX;
    [SerializeField] float Width;
     private Rigidbody2D MyBody;
    [SerializeField] LayerMask Engel;
    [SerializeField] float GizmozUzunluk;
    private static int TotalEnemyNumber = 0;
    // Start is called before the first frame update
    void Start()
    {   
        Width = GetComponent<SpriteRenderer>().bounds.extents.x;
        MyBody = GetComponent<Rigidbody2D>();
        TotalEnemyNumber++;
        
    } 

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D Hit = Physics2D.Raycast(transform.position + (transform.right * Width/2), Vector2.down, 2f,Engel);
      
        if (Hit.collider!=null)
        {
            OnGroud = true;
        }
        else
        {
            OnGroud = false;
        }
        CanavariDondurme();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 PlayerRealPosition  = transform.position + (transform.right * Width / 2);
        Gizmos.DrawLine(PlayerRealPosition,PlayerRealPosition + new Vector3(0, -GizmozUzunluk, 0));
    }
    void CanavariDondurme()
    {
        if (!OnGroud)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
        MyBody.velocity = new Vector2(transform.right.x * SpeedX, 0f);
    }
}
