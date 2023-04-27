using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Game game;
    public UIController uiController;

    private void Start()
    {
        game = GetComponent<Game>();
        uiController = GetComponent<UIController>();
        game.InitializeGame();
        UpdateUI();
    }

    public void HandleCorrectAnswer()
    {
        game.HandleCorrectAnswer();
        UpdateUI();
    }

    public void HandleWrongAnswer()
    {
        game.HandleWrongAnswer();
        UpdateUI();
    }


    private void UpdateUI()
    {
        uiController.SetHint(game.currentHint);
        uiController.SetQuestionNumber(game.QuestionIndex);
        uiController.SetHintNumber(game.HintIndex);

    }
}
