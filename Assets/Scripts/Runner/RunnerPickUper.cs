using UnityEngine;

public class RunnerPickUper : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IPickUpable>()?.PickUp();
    }
}
