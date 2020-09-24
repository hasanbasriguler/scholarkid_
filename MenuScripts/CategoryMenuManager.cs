using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CategoryMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] categorybuttons = new GameObject[6];

    [SerializeField]
    private GameObject[] languagebuttons = new GameObject[4];

    [SerializeField]
    private GameObject Soundimage;

    [SerializeField]
    private GameObject backbutton;

    [SerializeField]
    AudioSource audiosource;

    public AudioClip enbuttonsound;
    public AudioClip trbuttonsound;
    public AudioClip debuttonsound;
    public AudioClip frbuttonsound;
    public AudioClip buttonsopensound;

    public AudioClip categorybuttonssound;
    
    int sceneindex;

    AdsScript adsscript;


    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        
        foreach (var item in categorybuttons)
        {
            item.GetComponent<RectTransform>().localScale = Vector2.zero;
            
        }

        foreach (var item in languagebuttons)
        {
            item.GetComponent<RectTransform>().localScale = Vector2.zero;
        
        }

        Soundimage.GetComponent<RectTransform>().localScale = Vector2.zero;
        backbutton.GetComponent<RectTransform>().localScale = Vector2.zero;
    }

    void Start()
    {
        adsscript = Object.FindObjectOfType<AdsScript>();
        
        
        sceneindex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ButtonRoutine());
    }

       
    IEnumerator ButtonRoutine()
    {
        foreach (var item in categorybuttons)
        {
            item.GetComponent<RectTransform>().DOScale(1, 0.2f).SetEase(Ease.OutBack);
            audiosource.PlayOneShot(buttonsopensound);
            yield return new WaitForSeconds(0.2f);
        }

        foreach (var item in languagebuttons)
        {
            item.GetComponent<RectTransform>().DOScale(1, 0.1f).SetEase(Ease.OutBack);
            //audiosource.PlayOneShot(buttonsopensound);
            yield return new WaitForSeconds(0.1f);
        }

        Soundimage.GetComponent<RectTransform>().DOScale(1, 0.1f).SetEase(Ease.OutBack);        
        backbutton.GetComponent<RectTransform>().DOScale(1, 0.1f).SetEase(Ease.OutBack);       


    }


    public void GobackStartMenu()
    {        
        SceneManager.LoadScene("StartMenu");
    }

    public void LettersSelect()
    {
        audiosource.PlayOneShot(categorybuttonssound);
        SceneManager.LoadScene("GamePlay");
    }

    public void FruitSelect()
    {
        audiosource.PlayOneShot(categorybuttonssound);
        SceneManager.LoadScene("FruitVegetable");
    }

    public void AnimalSelect()
    {
        audiosource.PlayOneShot(categorybuttonssound);
        SceneManager.LoadScene("Animals");
    }

    public void GeometrySelect()
    {
        audiosource.PlayOneShot(categorybuttonssound);
        SceneManager.LoadScene("Geometry");
    }

    public void ColorsSelect()
    {
        audiosource.PlayOneShot(categorybuttonssound);
        SceneManager.LoadScene("Colors");
    }

    public void OrgansSelect()
    {
        audiosource.PlayOneShot(categorybuttonssound);
        SceneManager.LoadScene("Organs");
    }


    public void EnLanguageSelect()
    {

        PlayerPrefs.SetInt("languageselect", 0);
        audiosource.PlayOneShot(enbuttonsound);
        
        

    }

    public void TrLanguageSelect()
    {
        PlayerPrefs.SetInt("languageselect", 1);
        audiosource.PlayOneShot(trbuttonsound);
    }

    public void DeLanguageSelect()
    {
        PlayerPrefs.SetInt("languageselect", 2);
        audiosource.PlayOneShot(debuttonsound);
    }

    public void FrLanguageSelect()
    {
        PlayerPrefs.SetInt("languageselect", 3);
        audiosource.PlayOneShot(frbuttonsound);
    }    

    public void LanguageLearnSound()
    {
        switch (PlayerPrefs.GetInt("languageselect"))
        {
            case 0:
                audiosource.PlayOneShot(enbuttonsound);
                break;

            case 1:
                audiosource.PlayOneShot(trbuttonsound);
                break;

            case 2:
                audiosource.PlayOneShot(debuttonsound);
                break;

            case 3:
                audiosource.PlayOneShot(frbuttonsound);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {           
            SceneManager.LoadScene(sceneindex - 1);
        }
    }

    public void ButtonSound()
    {
        audiosource.PlayOneShot(categorybuttonssound);

        GobackStartMenu();

    }


}








