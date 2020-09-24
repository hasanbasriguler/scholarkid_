using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class AnimalsGameManager : MonoBehaviour
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

    public AudioClip catsound;
    public AudioClip dogsound;
    public AudioClip birdsound;
    public AudioClip sheepsound;
    public AudioClip chickensound;
    public AudioClip ducksound;
    public AudioClip cowsound;
    public AudioClip elephantsound;
    public AudioClip horsesound;


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
            if (buttonpoint=="0cat")
            {
                audiosource.PlayOneShot(catsound);
            }
            else if(buttonpoint=="1dog")
            {
                audiosource.PlayOneShot(dogsound);
            }
            else if (buttonpoint=="2bird")
            {
                audiosource.PlayOneShot(birdsound);
            }
            else if (buttonpoint=="8sheep")
            {
                audiosource.PlayOneShot(sheepsound);
            }
            else if (buttonpoint=="10chicken")
            {
                audiosource.PlayOneShot(chickensound);
            }
            else if (buttonpoint=="11duck")
            {
                audiosource.PlayOneShot(ducksound);
            }
            else if (buttonpoint=="13cow")
            {
                audiosource.PlayOneShot(cowsound);
            }
            else if (buttonpoint== "21elephant")
            {
                audiosource.PlayOneShot(elephantsound);
            }
            else if (buttonpoint=="24horse")
            {
                audiosource.PlayOneShot(horsesound);
            }
            else
            {
                audiosource.PlayOneShot(trueanswersound);
            }        
                       

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
                if (levelnumber<4|| levelnumber==8 || levelnumber==12 || levelnumber==16 || levelnumber==20 || levelnumber==24 || levelnumber==28 || levelnumber==32 || levelnumber==36|| levelnumber==40)
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
            case "0cat":                
                casenumber = 0;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Cat";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kedi";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Katze";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "chat";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "1dog":                
                casenumber = 1;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Dog";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);                        
                        break;

                    case 1:
                        questsprite = "Köpek";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);                        
                        break;

                    case 2:
                        questsprite = "Hund";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);                        
                        break;

                    case 3:
                        questsprite = "chien";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);                        
                        break;
                }
                break;

            case "2bird":                
                casenumber = 2;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Bird";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kuş";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Vogel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Oiseau";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "3bear":                
                casenumber = 3;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Bear";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Ayı";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Bär";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Ours";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "4lion":                
                casenumber = 4;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Lion";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Aslan";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Löwe";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Lion";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "5tortoise":                
                casenumber = 5;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Tortoise";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kaplumbağa";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Schildkröte";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "tortue";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "6fish":                
                casenumber = 6;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Fish";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);                        
                        break;

                    case 1:
                        questsprite = "Balık";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Fisch";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "poisson";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "7rabbit":                
                casenumber = 7;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Rabbit";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Tavşan";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Hase";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Lapin";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "8sheep":                
                casenumber = 8;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "sheep";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Koyun";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Schaf";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Mouton";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "9camel":                
                casenumber = 9;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Camel";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Deve";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kamel";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Chameau";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "10chicken":                
                casenumber = 10;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "chicken";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Tavuk";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Hähnchen";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Poulet";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "11duck":                
                casenumber = 11;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Duck";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Ördek";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Ente";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Canard";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "12butterfly":                
                casenumber = 12;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Butterfly";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kelebek";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Schmetterling";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Papillon";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "13cow":                
                casenumber = 13;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Cow";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "İnek";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Kuh";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Vache";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "14mouse":                
                casenumber = 14;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Mouse";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Fare";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Maus";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Souris";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "15zebra":                
                casenumber = 15;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Zebra";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Zebra";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Zebra";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Zèbre";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "16bee":                
                casenumber = 16;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Bee";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Arı";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Biene";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Abeille";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "17stork":                
                casenumber = 17;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Stork";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Leylek";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Storch";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cigogne";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "18deer":                
                casenumber = 18;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Deer";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Geyik";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Hirsch";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cerf";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "19kangaroo":                
                casenumber = 19;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Kangaroo";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Kanguru";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Känguru";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Kangourou";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "20ostrich":                
                casenumber = 20;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Ostrich";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Devekuşu";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Strauß";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Autruche";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "21elephant":                
                casenumber = 21;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Elephant";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Fil";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Elefant";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "l'éléphant";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "22giraffe":                
                casenumber = 22;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Giraffe";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Zürafa";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Giraffe";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "girafe";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "23monkey":                
                casenumber = 23;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Monkey";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "Maymun";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Affe";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Singe";
                        audiosource.PlayOneShot(frletternumbersounds[casenumber]);
                        break;
                }
                break;

            case "24horse":                
                casenumber = 24;
                switch (PlayerPrefs.GetInt("languageselect"))
                {
                    case 0:
                        questsprite = "Horse";
                        audiosource.PlayOneShot(enletternumbersounds[casenumber]);
                        break;

                    case 1:
                        questsprite = "At";
                        audiosource.PlayOneShot(trletternumbersounds[casenumber]);
                        break;

                    case 2:
                        questsprite = "Pferd";
                        audiosource.PlayOneShot(deletternumbersounds[casenumber]);
                        break;

                    case 3:
                        questsprite = "Cheval";
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
