using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Controller controller;

    private VisualElement root;
    private Label hint;
    private Label questionNumberLabel;
    private Label hintNumberLabel;
    private Label timeLabel;
    private Label answer_indicator;
    private Label highScoreLabel;
    private Label currentScoreLabel;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        hint = root.Q<Label>("Hint");
        questionNumberLabel = root.Q<Label>("QuestionCounter");
        hintNumberLabel = root.Q<Label>("HintNum");
        timeLabel = root.Q<Label>("CounterLabel");
        answer_indicator = root.Q<Label>("AnswerIndicator");
        highScoreLabel = root.Q<Label>("HighScore");
        currentScoreLabel = root.Q<Label>("MyScore");
    }

    private void Start()
    {
        controller = GetComponent<Controller>();
    }

    public void SetHint(string hintText)
    {
        hint.text = hintText;
    }

    public void SetQuestionNumber(int questionNumber)
    {
        questionNumberLabel.text = $"Question {questionNumber.ToString()}";
    }

    public void SetHintNumber(int hintNumber)
    {
        hintNumberLabel.text = $"Hint {hintNumber.ToString()} :";
    }
}
