using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameJoystik
{

    [RequireComponent(typeof(Image))]
    public class Joystik : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IRemoveObject, IInitObject
    {

        #region Constants
        private const int CHILD_INDEX_TOUCH = 0;
        private const float OFFSET_TOUCH = 2.5f;

        #endregion

        #region Fields


        private Image treshold;
        private Image touch;

        private Vector3 inputDir;

        public Vector3 InputDir { get => inputDir; }
        public static Joystik Active { get => joystik; }

        private static Joystik joystik;

        public event Action<Vector3> onMove;

        #endregion

        #region Init components
        // Start is called before the first frame update
        void Awake()
        {
            Init();
        }

        public void Init()
        {
            if (joystik == null)
            {
                if (!TryGetComponent(out treshold))
                {
                    throw new JoystikException($"{name} not have component UnityEngine.UI.Image");
                }

                if (transform.childCount == 0)
                {
                    throw new JoystikException($"{name} not have child of index 0");
                }


                GameObject touchObj = transform.GetChild(CHILD_INDEX_TOUCH).gameObject;


                if (!touchObj.TryGetComponent(out touch))
                {
                    throw new JoystikException($"{touchObj.name} not have component UnityEngine.UI.Image");
                }

                joystik = this;
            }

            else
            {
                Remove();
            }
        }

        #endregion


        #region Interactions on UI
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetInputDir(Vector3.zero);
            SetAnchoredPositionTouch(inputDir);


        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(treshold.rectTransform, eventData.position, eventData.pressEventCamera, out position))
            {
                Vector3 sizeDelta = treshold.rectTransform.sizeDelta;


                position.x = (position.x / sizeDelta.x);
                position.y = (position.y / sizeDelta.y);

                Vector3 pivotTreshold = treshold.rectTransform.pivot;

                float x = pivotTreshold.x == 1f ? position.x * 2 + 1 : position.x * 2 - 1;
                float y = pivotTreshold.y == 1f ? position.y * 2 + 1 : position.y * 2 - 1;


                SetInputDir(new Vector3(x, y, 0));

                if (inputDir.magnitude > 1)
                {
                    SetInputDir(inputDir.normalized);
                }

                Vector3 newPosTouch = new Vector3(inputDir.x * (sizeDelta.x / OFFSET_TOUCH), inputDir.y * (sizeDelta.y / OFFSET_TOUCH));
                SetAnchoredPositionTouch(newPosTouch);
                

            }
        }

        #endregion
        
        private void SetInputDir (Vector3 point)
        {
            inputDir = point;

            onMove?.Invoke(inputDir);
        }

        private void SetAnchoredPositionTouch (Vector3 point)
        {
            touch.rectTransform.anchoredPosition = point;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public void Remove(float time)
        {
            Destroy(gameObject, time);
        }
    }

    

}
