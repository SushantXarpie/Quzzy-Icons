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

    public void HandleAnswer(string answer)
    {
        if (game.IsAnswerCorrect(answer))
        {
            game.HandleCorrectAnswer();
        }
        else
        {
            game.HandleWrongAnswer();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        uiController.SetHint(game.currentHint);
        uiController.SetQuestionNumber(game.QuestionIndex);
        uiController.SetHintNumber(game.HintIndex);

    }
}
