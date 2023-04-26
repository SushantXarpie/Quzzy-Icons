using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Question> questions = new List<Question>();

    public Question currentQuestion { get; private set; }
    int questionIndex = 0;
    public int QuestionIndex { get { return questionIndex + 1; } }

    public string currentHint { get; private set; }
    int hintIndex = 0;
    public int HintIndex { get { return hintIndex + 1; } }
    public void InitializeGame()
    {
        currentQuestion = questions[questionIndex];
        currentHint = currentQuestion.GetHints()[hintIndex];
    }

    public bool IsAnswerCorrect(string answer)
    {
        return answer == currentQuestion.answer;
    }

    public void HandleCorrectAnswer()
    {
        NextQuestion();
    }

    public void HandleWrongAnswer()
    {
        if (hintIndex < 2)
        {
            hintIndex++;
            currentHint = currentQuestion.GetHints()[hintIndex];
        }
        else
        {
            NextQuestion();
        }
    }

    private void NextQuestion()
    {
        questionIndex++;
        hintIndex = 0;
        currentQuestion = questions[questionIndex];
        currentHint = currentQuestion.GetHints()[hintIndex];
    }
}
