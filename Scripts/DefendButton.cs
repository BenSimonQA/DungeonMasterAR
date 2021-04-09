
using UnityEngine;
using UnityEngine.EventSystems;


public class DefendButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
   [HideInInspector]
    public bool PressedDef;

    public void OnPointerDown(PointerEventData eventData)
    {
        PressedDef = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PressedDef = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
