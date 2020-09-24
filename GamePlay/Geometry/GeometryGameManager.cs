using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GeometryGameManager : MonoBehaviour
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
    private Sprite[] lettersprite = new Sprite[15];

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

   
    public AudioClip[] enletternumbersounds = new AudioClip[15];
    public AudioClip[] trletternumbersounds = new AudioClip[15];
    public AudioClip[] deletternumbersounds = new AudioClip[15];
    public AudioClip[] frletternumbersounds = new AudioClip[15];

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

        for (int i = 0; i < 15; i++)
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

                for (int i = 0; i < 15; i++)
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
                if (levelnumber<2|| levelnumber==8 || levelnumber==12 || levelnumber==16 || levelnumber==20 || levelnumber==24 || levelnumber==28 || levelnumber==32 || levelnumber==36|| levelnumber==40 || levelnumber==42 || levelnumber==45)
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
            case "0square":                
                casenumber = 0;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Square";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kare";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Quadrat";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Carré";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "1rectangle":                
                casenumber = 1;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Rectangle";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);                        
                        break;

                    case 1:
                        questsprite = "Dikdörtgen";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);                        
                        break;

                    case 2:
                        questsprite = "Rechteck";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);                        
                        break;

                    case 3:
                        questsprite = "Rectangle";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);                        
                        break;
                }
                break;

            case "2circle":                
                casenumber = 2;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Circle";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Çember";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kreis";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cercle";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "3triangle":                
                casenumber = 3;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Triangle";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Üçgen";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Dreieck";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Triangle";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "4trapezoid":                
                casenumber = 4;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Trapezoid";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Yamuk";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Trapez";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Trapèze";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "5sphere":                
                casenumber = 5;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Sphere";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Küre";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kugel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Sphère";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "6triangularprism":                
                casenumber = 6;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Triangular Prism";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);                        
                        break;

                    case 1:
                        questsprite = "Üçgen Prizma";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Dreieckiges Prisma";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Prisme Triangulaire";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "7cylinder":                
                casenumber = 7;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Cylinder";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Silindir";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Zylinder";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cylindre";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "8cone":                
                casenumber = 8;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Cone";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Koni";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kegel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cône";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "9pentagon":                
                casenumber = 9;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Pentagon";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Beşgen";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Pentagon";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Pentagone";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "10hexagon":                
                casenumber = 10;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Hexagon";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Altıgen";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Hexagon";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Hexagone";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "11octagon":                
                casenumber = 11;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Octagon";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Sekizgen";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Achteck";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Octogone";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "12cube":                
                casenumber = 12;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Cube";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Küp";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Würfel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cube";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "13pyramid":                
                casenumber = 13;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Pyramid";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Piramit";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Pyramide";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Pyramide";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "14rectangularprism":                
                casenumber = 14;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Rectangular Prism";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Dikdörtgen Prizma";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Rechteckiges Prisma";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Prisme Rectangulaire";
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
