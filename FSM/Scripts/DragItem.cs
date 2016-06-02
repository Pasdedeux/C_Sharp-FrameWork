using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour , IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform t_rect;
	// Use this for initialization
	void Start () {
        t_rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrag( PointerEventData eventData )
    {
        t_rect.pivot.Set( 0, 0 );
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        /*GameObject.FindGameObjectWithTag( "Bag" ).*/GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent( GameObject.FindGameObjectWithTag( "XuanKuang" ).transform, true );

        transform.localScale = new Vector3( 0.8f, 0.8f, 0.8f );
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );

        if( eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "Panel" )
        {
            Debug.Log( "Throw in bag" );
            Debug.Log( eventData.pointerCurrentRaycast.gameObject.name );
            transform.SetParent( GameObject.FindGameObjectWithTag( "Bag" ).transform);
        }
        /*GameObject.FindGameObjectWithTag( "Bag" ).*/GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
}
