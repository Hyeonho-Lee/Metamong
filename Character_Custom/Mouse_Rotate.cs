using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse_Rotate : MonoBehaviour, IDragHandler
{
    public float speed = 10.0f;

    public void OnDrag(PointerEventData eventData) {
        float x = eventData.delta.x * Time.deltaTime * speed;

        transform.Rotate(0, -x, 0, Space.World);
    }
}