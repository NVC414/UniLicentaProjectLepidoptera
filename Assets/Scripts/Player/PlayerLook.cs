using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float sensitivity = 0.1f;
    [SerializeField] float minPitch = -85f;
    [SerializeField] float maxPitch = 85f;

    Vector2 lookInput;
    float pitch;
    bool lookEnabled = true;

    void Start()
    {
        SetCursorForUI(false);
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        if (!lookEnabled) return;

        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraRoot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void SetLookEnabled(bool enabled)
    {
        lookEnabled = enabled;
        if (!enabled) lookInput = Vector2.zero;
    }

    public void SetCursorForUI(bool ui)
    {
        Cursor.lockState = ui ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = ui;
    }
}