﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtBest;

    [SerializeField] private Text moodText;
    [SerializeField] private Text ageText;

    void Update()
    {
        txtBest.text = "Best: " + GameManager.singleton.best;
        txtScore.text = "Score: " + GameManager.singleton.score;

        moodText.text = "Mood: " + ((int)score.currentMood).ToString();
        ageText.text = "Age: " + ((int)score.currentAge).ToString();
    }
}
