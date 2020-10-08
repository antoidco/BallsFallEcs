using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI {
    public class PressedComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool Pressed = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
    }
}
