using UnityEngine;

public static class InputManager
{
    public static Vector3 GetMovementInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public static bool IsAiming()
    {
        return Input.GetMouseButton(0);
    }
}
