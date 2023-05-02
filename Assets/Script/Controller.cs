using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private UIController uiController;


    private int currentCounter;
    public int CurrentCounter
    {
        get
        {
            return currentCounter;
        }
        set
        {
            if (value == 0)
            {
                HandleWrongAnswer();
                return;
            }
            uiController.SetTimer(currentCounter);
            currentCounter = value;
        }
    }

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

        ResetCounter();
        StartCoroutine(UpdateCounter());
    }

    public void HandleCorrectAnswer()
    {
        game.HandleCorrectAnswer();
        UpdateUI();
        ResetCounter();
    }

    public void HandleWrongAnswer()
    {
        game.HandleWrongAnswer();
        UpdateUI();
        ResetCounter();
    }

    public void CheckAnswer(string answer)
    {
        uiController.GiveAnswerFeedBack(game.IsAnswerCorrect(answer));
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
    }

    public List<Question> GetQuestions()
    {
        if (game == null) Awake();
        return game.questions;
    }


    private void ResetCounter()
    {
        CurrentCounter = 30;
    }

    private IEnumerator UpdateCounter()
    {
        yield return new WaitForSeconds(1);
        CurrentCounter--;
        StartCoroutine(UpdateCounter());
    }

    private void UpdateUI()
    {
        uiController.SetHint(game.currentHint);
        uiController.SetQuestionNumber(game.QuestionIndex);
        uiController.SetHintNumber(game.HintIndex);

    }
}
