using System;
using UnityEngine;
using UnityEngine.UIElements;

public class IconDragger : MouseManipulator
{
    private Controller controller;

    private VisualElement dragArea;
    private VisualElement iconContainer;
    private VisualElement dropZone;

    private Vector3 startPosition;
    private Vector3 elementStartPositionLocal;
    private Vector3 elementStartPositionGlobal;

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
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        iconContainer = target.parent;
        startPosition = evt.localPosition;

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

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!isActive || !target.HasMouseCapture()) return;
        Vector2 delta = evt.localPosition - startPosition;
        target.style.top = target.layout.y + delta.y;
        target.style.left = target.layout.x + delta.x;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!isActive || !target.HasMouseCapture()) return;

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