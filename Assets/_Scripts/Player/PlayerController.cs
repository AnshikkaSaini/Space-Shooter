using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace _Scripts
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        private Camera _mainCam;
        private Vector3 _offset;
        private float maxLeft, maxRight, maxDown, maxUp;
        private bool isDragging = false;

        public int Shield { get; set; } = 25;
        public int Health { get; set; } = 100;

        void Start()
        {
            _mainCam = Camera.main;
            StartCoroutine(SetBoundaries());
        }

        void Update()
        {
            if (Touch.activeTouches.Count > 0)
            {
                Touch myTouch = Touch.activeTouches[0];

                if (myTouch.phase == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    Vector3 touchWorldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                        myTouch.screenPosition.x,
                        myTouch.screenPosition.y,
                        10f // Safe distance from camera (adjust if needed)
                    ));
                    touchWorldPos.z = 0f;

                    Collider2D hit = Physics2D.OverlapPoint(new Vector2(touchWorldPos.x, touchWorldPos.y));
                    if (hit != null && hit.transform == transform)
                    {
                        isDragging = true;
                        _offset = touchWorldPos - transform.position;
                    }
                }

                if ((myTouch.phase == UnityEngine.InputSystem.TouchPhase.Moved ||
                     myTouch.phase == UnityEngine.InputSystem.TouchPhase.Stationary) && isDragging)
                {
                    Vector3 touchWorldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                        myTouch.screenPosition.x,
                        myTouch.screenPosition.y,
                        10f
                    ));
                    touchWorldPos.z = 0f;

                    Vector3 targetPos = touchWorldPos - _offset;
                    transform.position = new Vector3(
                        Mathf.Clamp(targetPos.x, maxLeft, maxRight),
                        Mathf.Clamp(targetPos.y, maxDown, maxUp),
                        0f
                    );
                }

                if (myTouch.phase == UnityEngine.InputSystem.TouchPhase.Ended ||
                    myTouch.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
                {
                    isDragging = false;
                }
            }
        }

        void OnEnable() => EnhancedTouchSupport.Enable();
        void OnDisable() => EnhancedTouchSupport.Disable();

        private IEnumerator SetBoundaries()
        {
            yield return new WaitForEndOfFrame();
            maxLeft = _mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
            maxRight = _mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
            maxDown = _mainCam.ViewportToWorldPoint(new Vector2(0, 0.09f)).y;
            maxUp = _mainCam.ViewportToWorldPoint(new Vector2(0, 0.85f)).y;
        }

        public void TakeDamage(int value)
        {
            Health -= value;
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
