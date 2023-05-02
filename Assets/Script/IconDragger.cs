using System;
using UnityEngine;
using UnityEngine.UIElements;

public class IconDragger : MouseManipulator
{
    private Controller controller;

    private VisualElement dragArea;
    private VisualElement iconContainer;
    private VisualElement dropZone;

    private Vector2 startPosition;
    private Vector2 elementStartPositionLocal;
    private Vector2 elementStartPositionGlobal;

    bool isActive;

    public IconDragger(VisualElement root, Controller controller)
    {
        this.controller = controller;
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
        Debug.Log("OnMouseDown");
        iconContainer = target.parent;
        startPosition = evt.localMousePosition;

        elementStartPositionLocal = target.layout.position;
        elementStartPositionGlobal = target.worldBound.position;

        dragArea.style.display = DisplayStyle.Flex;
        dragArea.Add(target);
        target.style.top = elementStartPositionGlobal.y;
        target.style.left = elementStartPositionGlobal.x;
        target.CaptureMouse();
        isActive = true;
        evt.StopPropagation(); // Prevents the event from bubbling up to the parent element
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (!isActive || !target.HasMouseCapture()) return;
        Debug.Log("OnMouseMove");
        Vector2 delta = evt.localMousePosition - startPosition;
        target.style.top = target.layout.y + delta.y;
        target.style.left = target.layout.x + delta.x;
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        if (!isActive || !target.HasMouseCapture()) return;
        Debug.Log("OnMouseUp");

        if (target.worldBound.Overlaps(dropZone.worldBound))
        {
            dropZone.Add(target);
            target.style.top = dropZone.contentRect.center.y - target.layout.height / 2;
            target.style.left = dropZone.contentRect.center.x - target.layout.width / 2;

            Debug.Log($"The Provided answer is {((Question)target.userData).display_answer}");
            controller.CheckAnswer(((Question)target.userData).answer);
        }
        else
        {
            iconContainer.Add(target);

            target.style.top = elementStartPositionLocal.y - iconContainer.contentRect.position.y;
            target.style.left = elementStartPositionLocal.x - iconContainer.contentRect.position.x;
        }
        isActive = false;
        target.ReleaseMouse();
        evt.StopPropagation();

        dragArea.style.display = DisplayStyle.None;
    }
}