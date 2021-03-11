using UnityEngine;

namespace ItemSystem.Scripts
{
    public class PickableSpawner : MonoBehaviour
    {
        [SerializeField] private string itemBaseName;
        [SerializeField] private GameObject pickableItemPrefab;

        private void Start()
        {
            var pickableItem =  Instantiate(pickableItemPrefab, transform.position, transform.rotation).GetComponent<IPickable>();
            pickableItem.Initialize(itemBaseName);
            Destroy(gameObject);
        }
        
    }
}