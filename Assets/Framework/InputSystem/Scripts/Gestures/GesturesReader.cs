using System;
using System.Collections;
using UnityEngine;

public class GesturesReader : MonoBehaviour
{
    public static GesturesReader instance;

    [Header("GESTURES SETTINGS")]
    public float dragThresholdPixels = 10f;
    public float holdTimeSeconds = 0.5f;

    [Header("RAYCAST SETTINGS")]
    public LayerMask raycastMask = ~0;
    public float raycastMaxDistance = 100f;

    // events that can be used for callig in other scripts
    public event Action<Vector2> onTap;
    public event Action<Vector2> onHold;
    public event Action<Vector2> onDragStart;
    public event Action<Vector2, Vector2> onDrag; // (delta, position)
    public event Action<Vector2> onDragEnd;
    public event Action<RaycastHit> onObjectTouched;

    private bool _isTouchActive;
    private Vector2 _startScreenPos;
    private Vector2 _lastScreenPos;
    private float _startTime;
    private bool _isDragging;
    private bool _isHoldInvoked;
    private Coroutine _holdCoroutine;

    private Camera _mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (InputController.instance != null)
        {
            InputController.instance.onPrimaryTouchStarted += HandlePrimaryStarted;
            InputController.instance.onPrimaryTouchPerformed += HandlePrimaryPerformed;
            InputController.instance.onPrimaryTouchCancelled += HandlePrimaryCancelled;
        }
    }

    private void OnDisable()
    {
        if (InputController.instance != null)
        {
            InputController.instance.onPrimaryTouchStarted -= HandlePrimaryStarted;
            InputController.instance.onPrimaryTouchPerformed -= HandlePrimaryPerformed;
            InputController.instance.onPrimaryTouchCancelled -= HandlePrimaryCancelled;
        }
    }

    // called when the touch started
    private void HandlePrimaryStarted(Vector2 screenPos)
    {
        _isTouchActive = true;
        _startScreenPos = screenPos;
        _lastScreenPos = screenPos;
        _startTime = Time.time;
        _isDragging = false;
        _isHoldInvoked = false;

        // start coroutine for hold
        if (_holdCoroutine != null) StopCoroutine(_holdCoroutine);
        _holdCoroutine = StartCoroutine(HoldRoutine());

        // if an object its hitted
        if (TryRaycast(screenPos, out RaycastHit hit))
        {
            onObjectTouched?.Invoke(hit);
        }
    }

    // called when the touch its in performed
    private void HandlePrimaryPerformed(Vector2 screenPos)
    {
        if (!_isTouchActive) return;

        Vector2 deltaFromLast = screenPos - _lastScreenPos;
        float totalDistance = (screenPos - _startScreenPos).magnitude;

        // if its not in drag, check the thresHold
        if (!_isDragging && totalDistance >= dragThresholdPixels)
        {
            _isDragging = true;

            // interropt hold, if drag its started
            if (_holdCoroutine != null)
            {
                StopCoroutine(_holdCoroutine);
                _holdCoroutine = null;
            }
            _isHoldInvoked = false;
            onDragStart?.Invoke(_startScreenPos);
        }

        if (_isDragging)
        {
            onDrag?.Invoke(deltaFromLast, screenPos);
        }

        _lastScreenPos = screenPos;
    }

    // called when the touch its realesed o cancelled
    private void HandlePrimaryCancelled()
    {
        if (!_isTouchActive) return;

        if (_isDragging)
        {
            onDragEnd?.Invoke(_lastScreenPos);
        }
        else
        {
            // if its not a drag and its not invoked an hold, its a tap
            if (!_isHoldInvoked)
            {
                onTap?.Invoke(_startScreenPos);
            }
        }

        // state clean
        _isTouchActive = false;
        _isDragging = false;
        _isHoldInvoked = false;
        if (_holdCoroutine != null) { StopCoroutine(_holdCoroutine); _holdCoroutine = null; }
    }

    // coroutine that invoke hold, if the touch remains active for holdTimeSeconds
    private IEnumerator HoldRoutine()
    {
        yield return new WaitForSeconds(holdTimeSeconds);

        // if the touch its still active, and we are not in drag, invoke hold
        if (_isTouchActive && !_isDragging)
        {
            _isHoldInvoked = true;
            onHold?.Invoke(_startScreenPos);
        }

        _holdCoroutine = null;
    }

    // utility raycast
    private bool TryRaycast(Vector2 screenPos, out RaycastHit hit)
    {
        Camera cam = _mainCamera ?? Camera.main;
        if (cam == null)
        {
            hit = default;
            return false;
        }
        Ray ray = cam.ScreenPointToRay(screenPos);
        return Physics.Raycast(ray, out hit, raycastMaxDistance, raycastMask);
    }
}