using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TabletUICollider : XRBaseInteractable
{
    public GraphicRaycaster target;
    public Vector2 scale;

    public BoxCollider screenPokeColider;

    private List<RaycastResult> raycastResults = new List<RaycastResult>();

    void Start()
    {
        if (screenPokeColider == null)
            screenPokeColider = GetComponent<BoxCollider>();

        /*
        selectEntered.AddListener(
            new UnityEngine.Events.UnityAction<SelectEnterEventArgs>(args => Debug.Log(args, this))
        );
        hoverEntered.AddListener(
            new UnityEngine.Events.UnityAction<HoverEnterEventArgs>(args => Debug.Log(args, this))
        );
        */

        target.GetComponent<Canvas>().targetDisplay = 0;
    }

    private bool ClickUI(Vector2 pos)
    {
        if (target == null)
            return false;

        var data = new PointerEventData(null);
        data.button = PointerEventData.InputButton.Left;
        data.position = pos;
        data.displayIndex = 0;
        data.pressPosition = pos;

        raycastResults.Clear();
        target.Raycast(data, raycastResults);

        if (raycastResults.Count == 0)
            return false;

        var obj = raycastResults[0].gameObject;

        var executed = ExecuteEvents.ExecuteHierarchy<IPointerClickHandler>(
            obj, data,
            (x, y) => x.OnPointerClick((PointerEventData)y)
        );

        return executed;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        var poke = args.interactorObject as XRPokeInteractor;
        if (poke == null)
            return;

        var pos = transform.InverseTransformPoint(
            poke.attachTransform.position
        ) - screenPokeColider.center;
        
        // Debug.Log($"{pos}", this);

        var sz = screenPokeColider.size;
        pos.x /= sz.x;
        pos.y /= sz.y;
        pos.z /= sz.z;
        
        pos = pos + Vector3.one * 0.5f;
        
        // Debug.Log($"{pos}", this);

        var x = pos.x * scale.x;
        var y = pos.y * scale.y;
        // Debug.Log($"{x} {y}", this);

        ClickUI(new Vector2(x, y));
    }
}
