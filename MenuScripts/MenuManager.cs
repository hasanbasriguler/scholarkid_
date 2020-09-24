using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    [SerializeField]
    private GameObject PlayBtn;

    [SerializeField]
    private AudioSource audiosource;

    public AudioClip playbuttonsound;


    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Start()
    {        
        FadeOut();
    }

   

    void FadeOut()
    {
        PlayBtn.GetComponent<CanvasGroup>().DOFade(1, 2f);
    }

    public void StartGameLevel()
    {        
        SceneManager.LoadScene("Categories");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();            
        }
    }
    
    public void ButtonSound()
    {
        audiosource.PlayOneShot(playbuttonsound);

        StartGameLevel();

    }



}
