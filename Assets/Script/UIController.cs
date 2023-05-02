using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private Controller controller;

    private VisualElement root;
    private Label hint;
    private Label questionNumberLabel;
    private Label hintNumberLabel;
    private Label timeLabel;
    private Label answer_indicator;
    private Label highScoreLabel;
    private Label currentScoreLabel;
    private Button nextHintButton;

    private void Awake()
    {
        Debug.Log("UIController Awake" + Time.time);
        controller = GetComponent<Controller>();
    }

    private void OnEnable()
    {
        // Debug.Log("UIController OnEnable" + Time.time);
        // controller = GetComponent<Controller>();
        GetComponent<UIDocument>().rootVisualElement.style.scale = new Vector2(Screen.safeArea.width / Screen.width, Screen.safeArea.height / Screen.height);
        root = GetComponent<UIDocument>().rootVisualElement;

        hint = root.Q<Label>("Hint");
        questionNumberLabel = root.Q<Label>("QuestionCounter");
        hintNumberLabel = root.Q<Label>("HintNum");
        timeLabel = root.Q<Label>("CounterLabel");
        answer_indicator = root.Q<Label>("AnswerIndicator");
        highScoreLabel = root.Q<Label>("HighScore");
        currentScoreLabel = root.Q<Label>("MyScore");
        nextHintButton = root.Q<Button>("NextHintButton");

        Initialize();
    }

    private void Initialize()
    {
        nextHintButton.clicked += () => controller.HandleWrongAnswer();
        Setup.InitializeDragAndDrop(root, controller);
        Setup.InitializeIcons(root, controller.GetQuestions());
    }

    public void SetHint(string hintText)
    {
        hint.text = hintText;
        Debug.Log($"Hint = {hintText}");
    }

    public void SetQuestionNumber(int questionNumber)
    {
        questionNumberLabel.text = $"Question {questionNumber}";
        Debug.Log($"Question Number = {questionNumber}");
    }

    public void SetHintNumber(int hintNumber)
    {
        hintNumberLabel.text = $"Hint {hintNumber} :";
        Debug.Log($"Hint Number = {hintNumber}");
    }
}
