using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace _Scripts
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        private Camera _mainCam;
        private Vector3 _offset;
        private bool isDragging;
        private float maxLeft, maxRight, maxDown, maxUp;

        public int Shield { get; set; } = 25;

        private void Start()
        {
            _mainCam = Camera.main;
            StartCoroutine(SetBoundaries());
        }

        private void Update()
        {
#if  UNITY_IOS || UNITY_ANDROID 
            // Handle both touch and mouse input for cross-platform compatibility
            HandleTouchInput();
#else 
            // Touch for mobile devices, mouse for WebGL/desktop builds
            HandleMouseInput();
#endif
        }

        private void HandleTouchInput()
        {
            if (Touch.activeTouches.Count > 0)
            {
                var myTouch = Touch.activeTouches[0];

                if (myTouch.phase == TouchPhase.Began)
                {
                    var touchWorldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                        myTouch.screenPosition.x,
                        myTouch.screenPosition.y,
                        10f // Safe distance from camera (adjust if needed)
                    ));
                    touchWorldPos.z = 0f;

                    var hit = Physics2D.OverlapPoint(new Vector2(touchWorldPos.x, touchWorldPos.y));
                    if (hit != null && hit.transform == transform)
                    {
                        isDragging = true;
                        _offset = touchWorldPos - transform.position;
                    }
                }

                if ((myTouch.phase == TouchPhase.Moved ||
                     myTouch.phase == TouchPhase.Stationary) && isDragging)
                {
                    var touchWorldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                        myTouch.screenPosition.x,
                        myTouch.screenPosition.y,
                        10f
                    ));
                    touchWorldPos.z = 0f;

                    var targetPos = touchWorldPos - _offset;
                    transform.position = new Vector3(
                        Mathf.Clamp(targetPos.x, maxLeft, maxRight),
                        Mathf.Clamp(targetPos.y, maxDown, maxUp),
                        0f
                    );
                }

                if (myTouch.phase == TouchPhase.Ended ||
                    myTouch.phase == TouchPhase.Canceled)
                    isDragging = false;
            }
        }

        private void HandleMouseInput()
        {
            // Only handle mouse input if no touches are active (prevents conflicts on touch devices)
            if (Touch.activeTouches.Count > 0) return;

            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    10f
                ));
                mouseWorldPos.z = 0f;

                var hit = Physics2D.OverlapPoint(new Vector2(mouseWorldPos.x, mouseWorldPos.y));
                if (hit != null && hit.transform == transform)
                {
                    isDragging = true;
                    _offset = mouseWorldPos - transform.position;
                }
            }

            if (Input.GetMouseButton(0) && isDragging)
            {
                var mouseWorldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    10f
                ));
                mouseWorldPos.z = 0f;

                var targetPos = mouseWorldPos - _offset;
                transform.position = new Vector3(
                    Mathf.Clamp(targetPos.x, maxLeft, maxRight),
                    Mathf.Clamp(targetPos.y, maxDown, maxUp),
                    0f
                );
            }

            if (Input.GetMouseButtonUp(0))
                isDragging = false;
        }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        public int Health { get; set; } = 100;

        public void TakeDamage(int value)
        {
            Health -= value;
            if (Health <= 0) Destroy(gameObject);
        }

        private IEnumerator SetBoundaries()
        {
            yield return new WaitForEndOfFrame();
            maxLeft = _mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
            maxRight = _mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
            maxDown = _mainCam.ViewportToWorldPoint(new Vector2(0, 0.09f)).y;
            maxUp = _mainCam.ViewportToWorldPoint(new Vector2(0, 0.85f)).y;
        }
    }
}