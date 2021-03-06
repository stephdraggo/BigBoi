using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BigBoi.Menus
{
    /// <summary>
    /// Displays some text when mouse hovers over the object this script is attached to.
    /// Works for canvas and objects.
    /// </summary>
    [AddComponentMenu("BigBoi/Menu System/Methods/Display Text on Hover (Unity Default)")]
    public class UnityHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, TextArea, Tooltip("Text to display when the player hovers over this object.")]
        private string displayText = "";

        [SerializeField, Tooltip("Where to display the tooltip.")]
        private Text displayArea;

        void Start()
        {
            if (displayArea == null) //error catching
            {
                Debug.LogError($"No text object assigned to display the tooltip for {gameObject.name}.");
                enabled = false;
                return;
            }
        }

        #region Canvas functionality
        public void OnPointerEnter(PointerEventData _eventData)
        {
            displayArea.gameObject.SetActive(true);
            displayArea.text = displayText;
        }

        public void OnPointerExit(PointerEventData _eventData)
        {
            displayArea.gameObject.SetActive(false);
        }
        #endregion
        //OR
        #region Object functionality
        public void OnMouseEnter()
        {
            displayArea.gameObject.SetActive(true);
            displayArea.text = displayText;
        }

        public void OnMouseExit()
        {
            displayArea.gameObject.SetActive(false);
        }
        #endregion

        /// <summary>
        /// This might show a null reference exception but it's not a problem.
        /// This is just a clean-up method.
        /// </summary>
        public void OnDisable()
        {
            displayArea.gameObject.SetActive(false);
        }
    }
}