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


        private Image _treshold;
        private Image _touch;

        private Vector3 _inputDir;

        private static Joystik _joystik;

        #endregion


        #region Properties
        public Vector3 InputDir => _inputDir;
        public static Joystik Active => _joystik;

        #endregion


        #region Init components

        public void Init()
        {
            if (_joystik == null)
            {
                if (!TryGetComponent(out _treshold))
                {
                    throw new JoystikException($"{name} not have component UnityEngine.UI.Image");
                }

                if (transform.childCount == 0)
                {
                    throw new JoystikException($"{name} not have child of index 0");
                }


                GameObject touchObj = transform.GetChild(CHILD_INDEX_TOUCH).gameObject;


                if (!touchObj.TryGetComponent(out _touch))
                {
                    throw new JoystikException($"{touchObj.name} not have component UnityEngine.UI.Image");
                }

                _joystik = this;
            }

            else
            {
                Remove();
            }
        }

        #endregion


        #region Interactions on UI
        
        

        public void OnPointerUp(PointerEventData eventData)
        {
            SetInputDir(Vector3.zero);

            SetAnchoredPositionTouch(_inputDir);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_treshold.rectTransform, eventData.position, eventData.pressEventCamera, out position))
            {
                Vector3 sizeDelta = _treshold.rectTransform.sizeDelta;


                position.x = (position.x / sizeDelta.x);
                position.y = (position.y / sizeDelta.y);

                Vector3 pivot_treshold = _treshold.rectTransform.pivot;

                float x = pivot_treshold.x == 1f ? position.x * 2 + 1 : position.x * 2 - 1;
                float y = pivot_treshold.y == 1f ? position.y * 2 + 1 : position.y * 2 - 1;


                SetInputDir(new Vector3(x, y, 0));

                if (_inputDir.magnitude > 1)
                {
                    SetInputDir(_inputDir.normalized);
                }

                Vector3 newPostouch = new Vector3(_inputDir.x * (sizeDelta.x / OFFSET_TOUCH), _inputDir.y * (sizeDelta.y / OFFSET_TOUCH));

                SetAnchoredPositionTouch(newPostouch);
                

            }
        }

        #endregion
        void Awake() => Init();

        public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

        private void SetInputDir (Vector3 point) => _inputDir = point;

        private void SetAnchoredPositionTouch (Vector3 point) =>  _touch.rectTransform.anchoredPosition = point;

        public void Remove() => Destroy(gameObject);

        public void Remove(float time) => Destroy(gameObject, time);
    }

    

}
