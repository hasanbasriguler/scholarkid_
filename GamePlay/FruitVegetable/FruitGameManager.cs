using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class FruitGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objectframeprefab;

    [SerializeField]
    private Transform framespanel;

    [SerializeField]
    private Text QuestionText;

    [SerializeField]
    private Transform questionpanel;

    private GameObject[] objectstrings;

    [SerializeField]
    private Sprite[] lettersprite = new Sprite[25];

    [SerializeField]
    private Transform soundbutton;

    [SerializeField]
    private GameObject gameoverpanel;

    [SerializeField]
    private Text gameoverpointtext;

    [SerializeField]
    private Text gamepointtext;

    [SerializeField]
    private Sprite[] gameoverbackrounds = new Sprite[4];

    [SerializeField]
    private GameObject[] boomeffect = new GameObject[5];

    [SerializeField]
    private Text secondtext;

    [SerializeField]
    AudioSource audiosource;

    
    public AudioClip[] enletternumbersounds = new AudioClip[25];
    public AudioClip[] trletternumbersounds = new AudioClip[25];
    public AudioClip[] deletternumbersounds = new AudioClip[25];
    public AudioClip[] frletternumbersounds = new AudioClip[25];

    public AudioClip[] badresultsounds = new AudioClip[4];
    public AudioClip[] middleresultsounds = new AudioClip[3];
    public AudioClip[] otherresultsounds = new AudioClip[10];

    public AudioClip buttonssound;
    public AudioClip createbaloonsound;
    public AudioClip timesupsound;
    public AudioClip trueanswersound;
    public AudioClip wronganswersound;


    int stringnumber = 3;
    int randomletters;
    string questsprite;
    string buttonpoint;
    bool buttonclick;
    string trueanswer;
    int lasthealth;
    int finishsituation;
    bool notfinished = true;
    int randomeffect;
    int lastseconds;
    bool seconds = true;
    int casenumber;   
    int questionnumber;
    int levelnumber;



    Health health;
    PointManager pointManager;
    


    List<UnityEngine.Sprite> firstanswerlist = new List<UnityEngine.Sprite>();
    List<UnityEngine.Sprite> answerlist = new List<UnityEngine.Sprite>();


    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        objectstrings = new GameObject[stringnumber];
        lasthealth = 4;
        gameoverpanel.GetComponent<RectTransform>().localScale = Vector2.zero;

        foreach (var item in boomeffect)
        {
            item.GetComponent<RectTransform>().localScale = Vector2.zero;

        }

        soundbutton.GetComponent<RectTransform>().localScale = Vector2.zero;
        questionpanel.GetComponent<RectTransform>().localScale = Vector2.zero;
       
        objectframeprefab.GetComponent<RectTransform>().localScale = Vector2.zero;

        health = Object.FindObjectOfType<Health>();
        pointManager = Object.FindObjectOfType<PointManager>();
       


        health.healthcontrol(lasthealth);


    }

    void Start()
    {
        buttonclick = false;

        lastseconds = 30;
       
        levelnumber = 0;
        
        secondtext.text = lastseconds.ToString();

        for (int i = 0; i < 25; i++)
        {
            firstanswerlist.Add(lettersprite[i]);
        }


       


        createobjects();
    }


    public void createobjects()
    {
        foreach (var item in boomeffect)
        {
            item.GetComponent<RectTransform>().localScale = Vector2.zero;

        }

       

        objectstrings = new GameObject[stringnumber];

        for (int i = 0; i < stringnumber; i++)
        {
            GameObject frame = Instantiate(objectframeprefab, framespanel);
            frame.transform.GetComponent<Button>().onClick.AddListener(() => TouchedButton());
            objectstrings[i] = frame;
        }

        lettersandnumberstexts();

        StartCoroutine(Dofaderoutine());

        Invoke("questionpanelopen", 1f);

    }


    void TouchedButton()
    {
        if (buttonclick)
        {
            buttonpoint = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Image>().sprite.name;

            resultcontrol();

        }


    }

   

    public void ButonSound()
    {
        audiosource.PlayOneShot(buttonssound);
    }


    void resultcontrol()
    {
        if (buttonpoint == trueanswer)
        {
            audiosource.PlayOneShot(trueanswersound);

            pointManager.pointincreasing();

            buttonclick = false;

            firstanswerlist.RemoveAt(questionnumber);

            if (firstanswerlist.Count < 6)
            {
                firstanswerlist.Clear();

                for (int i = 0; i < 25; i++)
                {
                    firstanswerlist.Add(lettersprite[i]);
                }
            }

            answerlist.Clear();


            StartCoroutine(DestroyRoutine());

            StopAllCoroutines();

            RemoveAllFrame();

            levelnumber++;

            if (stringnumber < 20)
            {
                if (levelnumber < 4 || levelnumber == 8 || levelnumber == 12 || levelnumber == 16 || levelnumber == 20 || levelnumber == 24 || levelnumber == 28 || levelnumber == 32 || levelnumber == 36 || levelnumber == 40)
                {
                    stringnumber++;
                }

            }


            if (notfinished)
            {
                createobjects();
            }


        }
        else
        {
            audiosource.PlayOneShot(wronganswersound);

            lasthealth--;

            health.healthcontrol(lasthealth);

            if (lasthealth == 1)
            {

                GameFinished();

            }


        }


    }

    void GameFinished()
    {
        buttonclick = false;
        notfinished = false;
        seconds = false;

        QuestionText.text = " ";

        StopAllCoroutines();

        RemoveAllFrame();

        soundbutton.GetComponent<RectTransform>().localScale = Vector2.zero;
        questionpanel.GetComponent<RectTransform>().localScale = Vector2.zero;

        gameoverpointtext.text = gamepointtext.text;

        finishsituation = int.Parse(gameoverpointtext.text);


        if (finishsituation < 16)
        {
            audiosource.PlayOneShot(badresultsounds[(Random.Range(0, badresultsounds.Length))]);

            gameoverpanel.GetComponent<Image>().sprite = gameoverbackrounds[0];

        }

        else if (finishsituation >= 16 && finishsituation < 31)
        {
            audiosource.PlayOneShot(middleresultsounds[(Random.Range(0, middleresultsounds.Length))]);

            gameoverpanel.GetComponent<Image>().sprite = gameoverbackrounds[1];

        }

        else if (finishsituation >= 31 && finishsituation < 51)
        {
            audiosource.PlayOneShot(otherresultsounds[(Random.Range(0, otherresultsounds.Length))]);

            gameoverpanel.GetComponent<Image>().sprite = gameoverbackrounds[2];

        }

        else if (finishsituation >= 51)
        {
            audiosource.PlayOneShot(otherresultsounds[(Random.Range(0, otherresultsounds.Length))]);

            gameoverpanel.GetComponent<Image>().sprite = gameoverbackrounds[3];

        }

        randomeffect = Random.Range(0, boomeffect.Length);
        boomeffect[randomeffect].GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);

        gameoverpanel.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);




    }



    void RemoveAllFrame()
    {
        foreach (var item in objectstrings)
        {
            item.GetComponent<DestroyObject>().destroyobjects();

        }


    }



    IEnumerator Dofaderoutine()
    {
        foreach (var frame in objectstrings)
        {

            frame.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
            audiosource.PlayOneShot(createbaloonsound);
            yield return new WaitForSeconds(0.2f);
        }
    }


    IEnumerator DestroyRoutine()
    {
        questionpanel.GetComponent<RectTransform>().DOScale(0, 0.3f).SetEase(Ease.InOutSine);
        soundbutton.GetComponent<RectTransform>().DOScale(0, 0.3f).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(0.05f);


    }

    IEnumerator TimerRoutine()
    {
        while (seconds)
        {
            yield return new WaitForSeconds(1f);

            if (lastseconds < 10)
            {
                secondtext.text = "0" + lastseconds.ToString();
            }
            else
            {
                secondtext.text = lastseconds.ToString();
            }

            lastseconds--;

            if (lastseconds < 0)
            {
                audiosource.PlayOneShot(timesupsound);

                GameFinished();
            }


        }

    }


    void lettersandnumberstexts()
    {
        foreach (var frame in objectstrings)
        {
            randomletters = Random.Range(0, firstanswerlist.Count);
            answerlist.Add(firstanswerlist[randomletters]);

            frame.transform.GetChild(1).GetComponent<Image>().sprite = firstanswerlist[randomletters];
        }

    }

    void questionpanelopen()
    {
        quest();
        buttonclick = true;
        questionpanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
        soundbutton.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);

        StartCoroutine(TimerRoutine());

    }

    void quest()
    {
        questsprite = answerlist[Random.Range(0, answerlist.Count)].name;

        for (int i = 0; i < firstanswerlist.Count; i++)
        {
            if (questsprite == firstanswerlist[i].name)
            {
                questionnumber = i;
            }
        }

        trueanswer = questsprite;

        if (seconds)
        {
            FindQuestText();
        }

        QuestionText.text = questsprite;


    }

    void FindQuestText()
    {
        switch (questsprite)
        {
            case "1watermelon":                
                casenumber = 0;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Watermelon";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Karpuz";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Wassermelone";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Pastèque";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "2melon":                
                casenumber = 1;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Melon";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);                        
                        break;

                    case 1:
                        questsprite = "Kavun";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);                        
                        break;

                    case 2:
                        questsprite = "Melone";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);                        
                        break;

                    case 3:
                        questsprite = "Melon";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);                        
                        break;
                }
                break;

            case "3banana":                
                casenumber = 2;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Banana";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Muz";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Banane";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "banane";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "4Strawberry":                
                casenumber = 3;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Strawberry";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Çilek";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Erdbeere";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Fraise";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "5grape":                
                casenumber = 4;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Grape";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Üzüm";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Traube";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Grain de raisin";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "6apple":                
                casenumber = 5;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Apple";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Elma";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Apfel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Pomme";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "7pineapple":                
                casenumber = 6;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Pineapple";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);                        
                        break;

                    case 1:
                        questsprite = "Ananas";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Ananas";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Ananas";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "8oranges":                
                casenumber = 7;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Orange";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Portakal";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Orange";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Orange";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "9cucumber":                
                casenumber = 8;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Cucumber";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Salatalık";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Gurke";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Concombre";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "10tomato":                
                casenumber = 9;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Tomato";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Domates";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Tomate";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Tomate";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "11carrot":                
                casenumber = 10;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Carrot";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Havuç";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Karotte";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Carotte";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "12cherries":                
                casenumber = 11;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Cherry";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kiraz";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kirsche";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cerise";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "13onion":                
                casenumber = 12;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Onion";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Soğan";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Zwiebel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Oignon";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "14garlic":                
                casenumber = 13;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Garlic";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Sarımsak";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Knoblauch";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Ail";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "15pepper":                
                casenumber = 14;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Pepper";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Biber";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Pfeffer";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Poivre";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "16corn":                
                casenumber = 15;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Corn";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Mısır";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Mais";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Blé";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "17kiwi":                
                casenumber = 16;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Kiwi";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kivi";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kiwi";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Kiwi";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "18apricot":                
                casenumber = 17;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "apricot";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kayısı";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Aprikose";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Abricot";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "19peach":                
                casenumber = 18;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "peach";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Şeftali";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Pfirsich";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Pêche";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "20lemon":                
                casenumber = 19;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Lemon";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Limon";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Zitrone";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Citron";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "21pumpkin":                
                casenumber = 20;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Pumpkin";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kabak";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kürbis";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Citrouille";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "22pear":                
                casenumber = 21;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Pear";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Armut";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Birne";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Poire";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "23eggplant":                
                casenumber = 22;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Eggplant";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Patlıcan";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Aubergine";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Aubergine";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "24olive":                
                casenumber = 23;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Olive";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Zeytin";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Olive";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Olive";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "25pomegranate":                
                casenumber = 24;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "pomegranate";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Nar";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Granatapfel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Grenade";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;
            
                
                

        }
    }

    public void LearnObjectSound()
    {
        if (PlayerPrefs.GetInt("languageselect") == 0)
        {
            audiosource.PlayOneShot(enletternumbersounds[casenumber]);
        }

        else if (PlayerPrefs.GetInt("languageselect") == 1)
        {
            audiosource.PlayOneShot(trletternumbersounds[casenumber]);
        }

        else if (PlayerPrefs.GetInt("languageselect") == 2)
        {
            audiosource.PlayOneShot(deletternumbersounds[casenumber]);
        }

        else if (PlayerPrefs.GetInt("languageselect") == 3)
        {
            audiosource.PlayOneShot(frletternumbersounds[casenumber]);
        }


    }



}
