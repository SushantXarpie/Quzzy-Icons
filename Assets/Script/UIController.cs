using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private Controller controller;

    private VisualElement root;
    private VisualElement dropZone;
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
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.scale = new Vector2(Screen.safeArea.width / Screen.width, Screen.safeArea.height / Screen.height);
        root.Q<VisualElement>("MainContainer").style.scale = new Vector2(Screen.safeArea.width / Screen.width, Screen.safeArea.height / Screen.height);
        hint = root.Q<Label>("Hint");
        questionNumberLabel = root.Q<Label>("QuestionCounter");
        hintNumberLabel = root.Q<Label>("HintNum");
        timeLabel = root.Q<Label>("CounterLabel");
        answer_indicator = root.Q<Label>("AnswerIndicator");
        highScoreLabel = root.Q<Label>("HighScore");
        currentScoreLabel = root.Q<Label>("MyScore");
        nextHintButton = root.Q<Button>("NextHintButton");
        dropZone = root.Q<VisualElement>("DropZone");

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

    public void SetTimer(int time)
    {
        timeLabel.text = $"Time Remaining : {time} seconds";
    }

    public void GiveAnswerFeedBack(bool correct)
    {
        Debug.Log($"Answer is {correct}");
        answer_indicator.style.visibility = Visibility.Visible;
        answer_indicator.text = correct ? "Your answer was Correct" : "Your answer was Wrong";

        StyleColor color = correct ? new StyleColor(new Color32(0, 132, 19, 255)) : new StyleColor(new Color32(132, 0, 19, 255));
        answer_indicator.style.color = color;
        StartCoroutine(CleanUpQuestion());
    }

    private IEnumerator CleanUpQuestion()
    {
        yield return new WaitForSeconds(3);
        answer_indicator.style.visibility = Visibility.Hidden;

        if (dropZone.childCount > 0)
        {
            dropZone.RemoveAt(0);
        }

    }
}
