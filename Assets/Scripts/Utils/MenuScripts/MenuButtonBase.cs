using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuButtonBase : MonoBehaviour
{
    public float hoverScale = 1.2f;
    public float scalingSpeed = 5f;

    private Vector3 initialScale;
    private bool isHovering = false;

    protected virtual void Start()
    {
        initialScale = transform.localScale;
    }

    protected virtual void Update()
    {
        if (isHovering)
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale * hoverScale, Time.deltaTime * scalingSpeed);
        else
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * scalingSpeed);
    }

    protected abstract void Click();

    protected void OnMouseDown() => Click();
    void OnMouseEnter() => isHovering = true;
    void OnMouseExit() => isHovering = false;
}
