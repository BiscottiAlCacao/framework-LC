using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputController : MonoBehaviour, MobileActions.IMobileMapActions
{
    public static InputController instance;
    private MobileActions _mobileActions;

    [Header("INPUT SETTINGS")]
    [SerializeField] private float _timeForHold;
    private float _savedTimeForHold;

    private float _timePrimaryInput;
    private float _timeSecondaryInput;

    private bool _primaryTouchHolded;
    private bool _secondaryTouchHolded;

    // events that can be used for callig in other scripts
    public Action<Vector2> onPrimaryTouchStarted;
    public Action<Vector2> onPrimaryTouchPerformed;
    public Action onPrimaryTouchCancelled;

    public Action<Vector2> onSecondaryTouchStarted;
    public Action<Vector2> onSecondaryTouchPerformed;
    public Action onSecondaryTouchCancelled;

    private void Awake()
    {
        _primaryTouchHolded = false;
        _savedTimeForHold = _timeForHold;
        
        _mobileActions = new MobileActions();

        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        _mobileActions.Enable();
        _mobileActions.MobileMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        _mobileActions.Disable();
        _mobileActions.MobileMap.RemoveCallbacks(this);
    }

    // primary touch handling (started / performed / canceled)
    public void OnPrimaryTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            onPrimaryTouchStarted?.Invoke(context.ReadValue<Vector2>());
            _timePrimaryInput = Time.deltaTime;
            StartCoroutine(HoldTime());
        }

        if (context.performed && _primaryTouchHolded)
        {
           onPrimaryTouchPerformed?.Invoke(context.ReadValue<Vector2>());
        }

        if (context.canceled)
        {
            StopCoroutine(HoldTime());
            _primaryTouchHolded = false;
            _timeForHold = _savedTimeForHold;
            onPrimaryTouchCancelled?.Invoke();
        }
    }

    // secondary touch handling (started / performed) , if its needed
    public void OnSecondaryTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            onSecondaryTouchStarted?.Invoke(context.ReadValue<Vector2>());
            _timePrimaryInput = Time.deltaTime;
        }

        if (context.performed && Time.deltaTime > _timePrimaryInput + _timeForHold)
        {
            onSecondaryTouchPerformed?.Invoke(context.ReadValue<Vector2>());
        }
    }

    // coroutine to detect primary hold
    private IEnumerator HoldTime()
    {
        yield return new WaitForSeconds(_timeForHold);
        _primaryTouchHolded = true;
    }
}