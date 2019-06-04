using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public static score Instance { set; get; }
    private bool gameStarted;

    private playerMotor motor;
    public static int coinScore;

    //decorations
    public GameObject[] decorations;

    //age
    public Text ageText;
    public SimpleHealthBar bdayCount;
    public Text countDownText;
    public static float agePoints;
    public static int currentAge = 1;
    private static int maxAge = 6;
    private static int ageToNextLevel = 10; //100

    //mood
    public SimpleHealthBar moodBar;
    public static float currentMood = 100;

    //health
    public SimpleHealthBar healthBar;
    public static int currentHealth = 100;

    //social
    public Text socialText;
    public static int socialLevel;

    //love
    public Text loveText;
    public GameObject loveContainer;
    public static int loveStatus;

    //events
    public eventController eventWindow;

    //skills 
    public Text skill1_text, skill2_text, skill3_text;

    //skills' images
    public Image skill1_image, skill2_image, skill3_image;

    //sports - 1
    private int skill1_ToNextLevel = 3;
    private int skill1_talentLevel;
    private GameObject skill1_level1, skill1_level2, skill1_level3;

    //arts - 2
    private int skill2_ToNextLevel = 3;
    private int skill2_talentLevel;
    private GameObject skill2_level1, skill2_level2, skill2_level3;

    //books - 3
    private int skill3_ToNextLevel = 3;
    private int skill3_talentLevel;
    private GameObject skill3_level1, skill3_level2, skill3_level3;

    //added with collectibles
    public static int skill1_points, skill2_points, skill3_points;

    public static int overallLevel = 1;
    private Color completedBar;

    private string skillName; //sport, art, brain 

    //life points
    private const int END_OF_CHILDHOOD = 6;
    private const int END_OF_TEENAGE_YEARS = 18;
    private const int END_OF_YOUNG_YEARS = 25;
    private const int END_OF_ADULT_YEARS = 36;
    private const int END_OF_MIDDLE_YEARS = 55;

    public Sprite[] skill1Sprites, skill2Sprites, skill3Sprites;


    private void Start()
    {
        Instance = this;
        completedBar = new Color32(51, 202, 9, 255);
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotor>();

        //set images unactive
        loveContainer.SetActive(false);
       

        //get skill bars
        skill1_level1 = GameObject.Find("skill1 lvl 1");
        skill1_level2 = GameObject.Find("skill1 lvl 2");
        skill1_level3 = GameObject.Find("skill1 lvl 3");

        skill2_level1 = GameObject.Find("skill2 lvl 1"); 
        skill2_level2 = GameObject.Find("skill2 lvl 2");
        skill2_level3 = GameObject.Find("skill2 lvl 3");

        skill3_level1 = GameObject.Find("skill3 lvl 1");
        skill3_level2 = GameObject.Find("skill3 lvl 2");
        skill3_level3 = GameObject.Find("skill3 lvl 3");


        if (skill1_points >= 1) //sports
            skill1_levelUp();
        else if (skill2_points >= 1) //arts
            skill2_levelUp();
        else if (skill3_points >= 1) //brains
            skill3_levelUp();
        else
            return;
    }

    void Update()
    {
        //if (!gameStarted)
            //return;

        if (mobileInput.Instance.Tap && !gameStarted)
        {
            gameStarted = true;
            motor.startRunning();
            FindObjectOfType<decorationSpawner>().IsScrolling = true;

        }

        if (!gameStarted)
            return;


        //next birthday bar
        agePoints += Time.deltaTime;
        bdayCount.UpdateBar(agePoints, ageToNextLevel);

        //current age text
        ageText.text = "Age: "+((int)currentAge).ToString();

        //social status text
        socialText.text = ((int)socialLevel).ToString();

        //love status text
        if (currentAge >= END_OF_CHILDHOOD)
        {
            //decorations[0] = decorations[1];
            loveContainer.SetActive(true);
            loveText.text = ((int)loveStatus).ToString();
        }

        //mood bar
        currentMood += Time.deltaTime;
        moodBar.UpdateBar(currentMood, 100);

        //health bar
        healthBar.UpdateBar(currentHealth, 100);
        float v = (int)(ageToNextLevel - agePoints);
        countDownText.text = v.ToString();

        //write skills
        skill1_text.text = skill1_points.ToString()+"/" + skill1_ToNextLevel;
        skill2_text.text = skill2_points.ToString() + "/" + skill2_ToNextLevel;
        skill3_text.text = skill3_points.ToString() + "/" + skill3_ToNextLevel;

        //get older
        if (agePoints >= ageToNextLevel)
            levelUp();

        //check talents
        if (skill1_points == skill1_ToNextLevel)
            skill1_levelUp();
        else if (skill2_points == skill2_ToNextLevel)
            skill2_levelUp();
        else if (skill3_points == skill3_ToNextLevel)
            skill3_levelUp();
        else
            return;
    }

    //get older
    void levelUp()
    {
        if (currentAge == maxAge)
        {
            //childhoodSkills.SetActive(false);
            getBiggestSkill();
            maxAge *=2;

            //switch (maxAge)
            //{
            //    case 18:
            //        maxAge = endOfTeenageYears;
            //        break;
            //    case endOfTeenageYears:
            //        maxAge = endOfTeenageYears;
            //        break;
            //    case endOfChildhood:
            //        maxAge = endOfTeenageYears;
            //        break;
            //}

            overallLevel++;
            print("maxage: " + maxAge);
            print("overall level: " + overallLevel);
        }

        ageToNextLevel *= 2;
        agePoints = 0;
        currentAge++;
        GetComponent<playerMotor>().setSpeed(currentAge);
      
    }

    //sports
    void skill1_levelUp()
    {
        if (skill1_talentLevel == 3)
            return;

        print("sport");
        skill1_talentLevel++;
        skill1_points = 0;
        switch (skill1_talentLevel)
        {
            case 0:
                return;
            case 1:
                skill1_level3.GetComponent<Image>().color = completedBar;
                break;
            case 2:
                skill1_level3.GetComponent<Image>().color = completedBar;
                skill1_level2.GetComponent<Image>().color = completedBar;
                break;
            case 3:
                skill1_level3.GetComponent<Image>().color = completedBar;
                skill1_level2.GetComponent<Image>().color = completedBar;
                skill1_level1.GetComponent<Image>().color = completedBar;
                Debug.Log("some nice animation here");
                break;
            default:
                Debug.Log("skill completed");
                break;
        }

        skill1_ToNextLevel *= 2;
        print("need " + skill1_ToNextLevel + "points for the next level");

        if (currentAge >= END_OF_CHILDHOOD)
        {
            StartCoroutine(eventWindow.toggleEventWindow("You are good at sports. Level " + skill1_talentLevel));
        }
        else if (currentAge >= END_OF_TEENAGE_YEARS)
        {
            switch (skillName)
            {
                case "sport":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at chess. Level " + skill1_talentLevel));
                    break;
                case "art":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at music. Level " + skill1_talentLevel));
                    break;
                case "brain":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at formal sciences. Level " + skill1_talentLevel));
                    break;
            }
            print("ur a teenager");
        }
        else if (currentAge >= END_OF_YOUNG_YEARS)
        {
            print("ur a mess");
        }
        else if (currentAge >= END_OF_ADULT_YEARS)
        {
            print("ur an adult");
        }
        else if (currentAge >= END_OF_MIDDLE_YEARS)
        {
            print("ur a middle aged person");
        }
        else
        {
            return;
        }


    }

    //arts
    void skill2_levelUp()
    {
        if (skill2_talentLevel == 3)
            return;

        print("music");
        skill2_talentLevel++;
        skill2_points = 0;
        switch (skill2_talentLevel)
        {
            case 0:
                return;
            case 1:
                skill2_level3.GetComponent<Image>().color = completedBar;
                break;
            case 2:
                skill2_level3.GetComponent<Image>().color = completedBar;
                skill2_level2.GetComponent<Image>().color = completedBar;
                break;
            case 3:
                skill2_level3.GetComponent<Image>().color = completedBar;
                skill2_level2.GetComponent<Image>().color = completedBar;
                skill2_level1.GetComponent<Image>().color = completedBar;
                Debug.Log("some nice animation here");
                break;
            default:
                Debug.Log("skill completed");
                break;
        }

       
        skill2_ToNextLevel *= 2;
        print("need " + skill2_ToNextLevel + "points for the next level");

        if (currentAge >= END_OF_CHILDHOOD)
        {
            StartCoroutine(eventWindow.toggleEventWindow("You are good at music. Level " + skill2_talentLevel));
        }
        else if (currentAge >= END_OF_TEENAGE_YEARS)
        {

            switch (skillName)
            {
                case "sport":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at football. Level " + skill2_talentLevel));
                    break;
                case "art":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at painting. Level " + skill2_talentLevel));
                    break;
                case "brain":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at social sciences. Level " + skill2_talentLevel));
                    break;
            }
            print("ur a teenager");
        }
        else if (currentAge >= END_OF_YOUNG_YEARS)
        {
            print("ur a mess");
        }
        else if (currentAge >= END_OF_ADULT_YEARS)
        {
            print("ur an adult");
        }
        else if (currentAge >= END_OF_MIDDLE_YEARS)
        {
            print("ur a middle aged person");
        }
        else
        {
            return;
        }
    }

    //brains
    void skill3_levelUp()
    {
        if (skill3_talentLevel == 3)
            return;

        print("paint");
        skill3_points = 0;
        skill3_talentLevel++;
        switch (skill3_talentLevel)
        {
            case 0:
                return;
            case 1:
                skill3_level3.GetComponent<Image>().color = completedBar;
                break;
            case 2:
                skill3_level3.GetComponent<Image>().color = completedBar;
                skill3_level2.GetComponent<Image>().color = completedBar;
                break;
            case 3:
                skill3_level3.GetComponent<Image>().color = completedBar;
                skill3_level2.GetComponent<Image>().color = completedBar;
                skill3_level1.GetComponent<Image>().color = completedBar;
                Debug.Log("some nice animation here");
                break;
            default:
                Debug.Log("skill completed");
                break;
        }

        skill3_ToNextLevel *= 2;
        print("need " + skill3_ToNextLevel + "points for the next level");

        if (currentAge >= END_OF_CHILDHOOD)
        {
            StartCoroutine(eventWindow.toggleEventWindow("You are good at books. Level " + skill3_talentLevel));
        }
        else if (currentAge >= END_OF_TEENAGE_YEARS)
        {

            switch (skillName)
            {
                case "sport":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at swimming. Level " + skill3_talentLevel));
                    break;
                case "art":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at writing. Level " + skill3_talentLevel));
                    break;
                case "brain":
                    StartCoroutine(eventWindow.toggleEventWindow("You are good at physical sciences. Level " + skill3_talentLevel));
                    break;
            }
            print("ur a teenager");
        }
        else if (currentAge >= END_OF_YOUNG_YEARS)
        {
            print("ur a mess");
        }
        else if (currentAge >= END_OF_ADULT_YEARS)
        {
            print("ur an adult");
        }
        else if (currentAge >= END_OF_MIDDLE_YEARS)
        {
            print("ur a middle aged person");
        }
        else
        {
            return;
        }
    }

 
    //get the most developed skill
    void getBiggestSkill()
    {
        skill3_talentLevel = 0;
        skill3_points = 0;

        if (skill1_talentLevel > skill2_talentLevel && skill1_talentLevel > skill3_talentLevel)
        {
            switch (currentAge)
            {
                case END_OF_CHILDHOOD:
                    skill1_image.sprite = skill1Sprites[0];
                    skill2_image.sprite = skill1Sprites[1];
                    skill3_image.sprite = skill1Sprites[2];

                    skillName = "sport";
                    print("you are talented in" + skillName);
                    break;
                case END_OF_TEENAGE_YEARS:
                    skillName = "chess";
                    print("you are talented in" + skillName);
                    break;
            }

        }
        else if (skill2_talentLevel > skill1_talentLevel && skill2_talentLevel > skill3_talentLevel)
        {
            switch (currentAge)
            {
                case END_OF_CHILDHOOD:
                    skill1_image.sprite = skill2Sprites[0];
                    skill2_image.sprite = skill2Sprites[1];
                    skill3_image.sprite = skill2Sprites[2];

                    skillName = "art";
                    print("you are talented in" + skillName);
                    break;
                case END_OF_TEENAGE_YEARS:

                    skillName = "music";
                    print("you are talented in" + skillName);
                    break;
            }
          
            print("you are talented in" + skillName);
        }
        else
        {
            switch (currentAge)
            {
                case END_OF_CHILDHOOD:
                    skill1_image.sprite = skill3Sprites[0];
                    skill2_image.sprite = skill3Sprites[1];
                    skill3_image.sprite = skill3Sprites[2];

                    skillName = "brain";
                    print("you are talented in" + skillName);
                    break;
                case END_OF_TEENAGE_YEARS:
                    skillName = "math";
                    print("you are talented in" + skillName);
                    break;
            }

        }

    }

    //public void updateModifier(float modifierAmount)
    //{

    //}


}
