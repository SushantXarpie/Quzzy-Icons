using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Setup
{
    public static void Initialize(VisualElement root)
    {
        InitializeDragAndDrop(root);
        InitializeIcons(root);
    }

    private static void InitializeDragAndDrop(VisualElement root)
    {
        root.Query<VisualElement>("IconBoard").Children<VisualElement>().ForEach((icon) =>
        {
            icon.AddManipulator(new IconDragger(root));
        });
    }

    private static void InitializeIcons(VisualElement root)
    {

    }
}
