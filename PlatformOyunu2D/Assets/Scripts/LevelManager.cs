using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] Text TxtToplamPuan;
    
    // Start is called before the first frame update
    private void Start()
    {
        TxtToplamPuan = GameObject.Find("TxtSkorSayisiDegeri").GetComponent<Text>();
    }
    public  void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    } 
    public void Restart()
    {
        
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (WinPanel.active == true)
        {  
            WinPanel.SetActive(false);

            Time.timeScale = 1;

        }
        else if (LosePanel.active == true)
        {   
            LosePanel.SetActive(false);
            Time.timeScale = 1;


        }

        
        

    }
    public void Cancel()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        if (WinPanel.active == true)
        {
            
            WinPanel.SetActive(false);
            Time.timeScale = 1;


        }
        else if (LosePanel.active == true)
        {
            
            LosePanel.SetActive(false);
            Time.timeScale = 1;


        }
    }
    public void AddScore(int skor)
    {
        int ToplamDeger = int.Parse(TxtToplamPuan.text);
        ToplamDeger = ToplamDeger + skor;
        TxtToplamPuan.text = ToplamDeger.ToString();

    }
}
   
