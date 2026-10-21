using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UISelectHelper : MonoBehaviour
{
    [SerializeField] InputActionReference navigateAction;

    EventSystem _eventSystem;

    void Awake()
    {
        _eventSystem = EventSystem.current;
    }

    void Update()
    {
        if (navigateAction.action.WasPressedThisFrame() && !_eventSystem.currentSelectedGameObject && _eventSystem.firstSelectedGameObject)
            _eventSystem.SetSelectedGameObject(_eventSystem.firstSelectedGameObject);
    }
}
