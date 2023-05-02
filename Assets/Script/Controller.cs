using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private UIController uiController;
    private void Awake()
    {
        Debug.Log("Controller Awake" + Time.time);
        game = GetComponent<Game>();
        uiController = GetComponent<UIController>();
    }
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

    public void CheckAnswer(string answer)
    {
        if (game.IsAnswerCorrect(answer))
        {
            HandleCorrectAnswer();
            Debug.Log("Correct Answer");
        }
        else
        {
            HandleWrongAnswer();
            Debug.Log("Wrong Answer");
        }
        uiController.GiveAnswerFeedBack(game.IsAnswerCorrect(answer));
    }

    public List<Question> GetQuestions()
    {
        if (game == null) Awake();
        return game.questions;
    }

    private void UpdateUI()
    {
        uiController.SetHint(game.currentHint);
        uiController.SetQuestionNumber(game.QuestionIndex);
        uiController.SetHintNumber(game.HintIndex);

    }
}
