using UnityEngine;

public class Utils: MonoBehaviour
{
    private Camera mainCamera = Camera.main;
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

    }
}