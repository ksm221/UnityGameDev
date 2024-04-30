using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject; // The other GameObject to deactivate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectiles") // Assuming projectiles are tagged "Projectile"
        {
            gameObject.SetActive(false); // Deactivate the switch
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(false); // Deactivate the target GameObject
            }
        }
    }
}
