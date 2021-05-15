using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    #region Değişkenler 
    [SerializeField] float Speed;
     private float MySpeedX;
    private Rigidbody2D MyBody;
    private Vector3 DefaultLocalScale;
    public bool OnGroud; //Zeminin üzerinde mi, değil mi?
    private bool CanDoubleJump;
    [SerializeField] float JumpPower;
    [SerializeField] GameObject Arrow;
    [SerializeField] bool Attacked;
    [SerializeField] float CurrentAttackTimer;
    [SerializeField] float DefaultAttackTimer;
    private Animator MyAnimator;
    [SerializeField] int ArrowNumber;
    [SerializeField] Text ArrowNumberValue;
    [SerializeField] AudioClip DieMusic;
    [SerializeField] GameObject LosePanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject BitirmeRozeti;




    #endregion

    void Start()
    {   Attacked = false;
        DefaultAttackTimer = 1;
        MyBody = GetComponent<Rigidbody2D>();
        DefaultLocalScale = transform.localScale;
        MyAnimator = gameObject.GetComponent<Animator>();
        ArrowNumberValue.text = ArrowNumber.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        #region sağa ve sola tuşlarına basarak playerin hareketinin sağlanması 
        MySpeedX = Input.GetAxis("Horizontal");
        
        //MySpeedX Horizontal olduğu için -1 ile 1 arasında basılma sürelerine bağlı olarak değer alıcak.
       MyAnimator.SetFloat("Speed", Mathf.Abs(MySpeedX));
            //game onject hangi scriptin içerisindeyse ve o script hangi objede kullanılıyorsa yani şöyle
            //PlayerController Player objesinin içinde kullanılıyor o yüzden yukarda ki kod tamam durumda...

        MyBody.velocity = new Vector2(MySpeedX * Speed, MyBody.velocity.y);
        //GetComponent<Rigidbody2D>().velocity = new Vector2(MySpeedX * Speed, GetComponent<Rigidbody2D>().velocity.y);
        #endregion

        #region Playerin sağa ve sola hareketine göre yüzünü sağa ve sola dönmesi
        if (MySpeedX>0)
        {
            transform.localScale = new Vector3(DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
        }
        else if (MySpeedX<0)
        {
            transform.localScale = new Vector3(-DefaultLocalScale.x, DefaultLocalScale.y, DefaultLocalScale.z);
        }
        #endregion

        #region Playerin zıplaması ve isteğe göre 2 kez zıplaması
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (OnGroud == true)
            {
                MyBody.velocity = new Vector2(MyBody.velocity.x, JumpPower);
                CanDoubleJump = true;
                MyAnimator.SetTrigger("Jump"); 
            }
            else
            {
                if (CanDoubleJump == true)
                {
                    MyBody.velocity = new Vector2(MyBody.velocity.x, JumpPower);
                    CanDoubleJump = false;
                }
            }
        }
        #endregion

        #region Playerin ok atmasının sağlanması
        if (Input.GetMouseButtonDown(0) && ArrowNumber>0)
        {
            if (Attacked == false)
            {  
                Attacked = true;
                MyAnimator.SetTrigger("Attack");

                Invoke("fire", 0.5f);
                ArrowNumber--;
                ArrowNumberValue.text = ArrowNumber.ToString();
                
                //Fonksiyon geciktirmek için kullanılan fonksiyon
               
            }
        }
        if (Attacked == true)
        {
            CurrentAttackTimer -= Time.deltaTime;
        }
        else
        {
            CurrentAttackTimer = DefaultAttackTimer;
        }
        if (CurrentAttackTimer <= 0)
        {
            Attacked = false;
        }
        //Oyundan defaultAttacktimer ı 2 yaparsak 2 saniye sonra atmamıza izin verir 1 yaparsa bir saniye sonra..
        
    }  //Update fonksiyonunun bitişi
    //Okun atma animasyonunda gözükmesi için fire fonksiyonunu update dışında tanımlı olması gerekiyor.
    void fire()
    {
        GameObject Okumuz = Instantiate(Arrow, transform.position, Quaternion.identity);
        Okumuz.transform.parent = GameObject.Find("ArrowsClone").transform;
        

        if (transform.localScale.x > 0)
        {
            Okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
        }
        else
        {
            Vector3 OkumuzScale = Okumuz.transform.localScale;
            Okumuz.transform.localScale = new Vector3(-OkumuzScale.x, OkumuzScale.y, OkumuzScale.z);
            Okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
        }
        #endregion


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            Die();
        }
        else if (collision.gameObject.tag=="EnemyZemin")
        {
            Die();
        }
        else if (collision.gameObject.tag=="Finish")
        {
            /* WinPanel.active = true;
             Time.timeScale = 0;*/

            StartCoroutine(Wait());
            
        }
        
    }
    //Oyundan bağımsız olarak yaptığım bitirme rozetini aldığında rozetin kaybolmasını sağlayan kodlar
    [SerializeField] void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name== "BitirmeRozeti")
        {
            Destroy(BitirmeRozeti);
            StartCoroutine(Wait());
        } 
    }


    public void Die()
    {
        GameObject.Find("AudioController").GetComponent<AudioSource>().clip = null;
        GameObject.Find("AudioController").GetComponent<AudioSource>().PlayOneShot(DieMusic);
       MyAnimator.SetFloat("Speed", 0);
       MyAnimator.SetTrigger("Die");
        //MyBody.constraints = RigidbodyConstraints2D.FreezePosition;
        MyBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        LosePanel.SetActive(true);
       StartCoroutine(Wait());
            
       
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);
        WinPanel.SetActive(true);
        Time.timeScale = 0;
        
       
    }





}
