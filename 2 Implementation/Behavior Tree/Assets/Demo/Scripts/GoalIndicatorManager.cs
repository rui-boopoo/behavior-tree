using UnityEngine;

public class GoalIndicatorManager : MonoBehaviour
{
    public void ShowAt(Table table)
    {
        gameObject.SetActive(true);

        Vector3 newPosition = table.gameObject.transform.position;
        newPosition.z -= 1.5f;
        transform.position = newPosition;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}