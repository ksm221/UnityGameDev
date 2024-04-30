using UnityEngine;

public class ActivateRoom5 : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private GameObject targetGameObject1;
    [SerializeField] private GameObject targetGameObject2;
    [SerializeField] private GameObject targetGameObject3;
    [SerializeField] private GameObject targetGameObject4;
    [SerializeField] private GameObject targetGameObject5;
    [SerializeField] private GameObject targetGameObject6;
    [SerializeField] private GameObject targetGameObject7;
    [SerializeField] private GameObject targetGameObject8;
    [SerializeField] private GameObject targetGameObject9;
    [SerializeField] private GameObject targetGameObject10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Assuming projectiles are tagged "Projectile"
        {
                targetGameObject.SetActive(true);
                targetGameObject1.SetActive(true);
                targetGameObject2.SetActive(true);
                targetGameObject3.SetActive(true);
                targetGameObject4.SetActive(true);
                targetGameObject5.SetActive(true);
                targetGameObject6.SetActive(true);
                targetGameObject7.SetActive(true);
                targetGameObject8.SetActive(true);
                targetGameObject9.SetActive(true);
                targetGameObject10.SetActive(true);
        }
    }
}
