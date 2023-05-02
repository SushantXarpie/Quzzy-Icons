using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Game game;
    public UIController uiController;

    // private void OnEnable()
    // {
    //     game = GetComponent<Game>();
    //     uiController = GetComponent<UIController>();
    // }

    private void Start()
    {
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

    public List<Question> GetQuestions()
    {
        return game.questions;
    }

    private void UpdateUI()
    {
        uiController.SetHint(game.currentHint);
        uiController.SetQuestionNumber(game.QuestionIndex);
        uiController.SetHintNumber(game.HintIndex);

    }
}
