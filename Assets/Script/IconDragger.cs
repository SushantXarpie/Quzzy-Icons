using System;
using UnityEngine;
using UnityEngine.UIElements;

public class IconDragger : MouseManipulator
{
    VisualElement dragArea;
    VisualElement iconContainer;
    VisualElement dropZone;

    Vector2 startPosition;
    Vector2 elementStartPositionLocal;
    Vector2 elementStartPositionGlobal;

    bool isActive;

    public IconDragger(VisualElement root)
    {
        dragArea = root.Q("DragArea");
        dropZone = root.Q("DropZone");

        isActive = false;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    private void OnMouseDown(MouseDownEvent evt)
    {
        iconContainer = target.parent;
        startPosition = evt.localMousePosition;

        elementStartPositionLocal = target.layout.position;
        elementStartPositionGlobal = target.worldBound.position;

        dragArea.style.display = DisplayStyle.Flex;
        target.CaptureMouse();
        isActive = true;
        evt.StopPropagation(); // Prevents the event from bubbling up to the parent element
        dragArea.Add(target);
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (!isActive || !target.HasMouseCapture()) return;

        Vector2 delta = evt.localMousePosition - startPosition;
        target.style.top = target.layout.y + delta.y;
        target.style.left = target.layout.x + delta.x;
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        iconContainer.Add(target);

        target.style.top = elementStartPositionLocal.y - iconContainer.contentRect.position.y;
        target.style.left = elementStartPositionLocal.x - iconContainer.contentRect.position.x;

        isActive = false;
        target.ReleaseMouse();
        evt.StopPropagation();

        dragArea.style.display = DisplayStyle.None;
    }
}