using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Setup
{
    public static void InitializeDragAndDrop(VisualElement root, Controller controller)
    {
        root.Query<VisualElement>("IconBoard").Children<VisualElement>().ForEach((icon) =>
        {
            icon.AddManipulator(new IconDragger(root, controller));
        });
    }

    public static void InitializeIcons(VisualElement root, List<Question> questions)
    {
        int currentIconIndex = 0;

        foreach (Question question in questions)
        {
            VisualElement icon = root.Query<VisualElement>("IconBoard").Children<VisualElement>().AtIndex(currentIconIndex);
            icon.style.backgroundImage = Resources.Load<Texture2D>("Images/" + question.answer);
            icon.userData = question;

            currentIconIndex++;
        }
    }
}
