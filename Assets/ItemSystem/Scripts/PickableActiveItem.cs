using UnityEngine;

namespace ItemSystem.Scripts
{
    public class PickableActiveItem : MonoBehaviour, IInteractable, IPickable
    {
        [SerializeField] private ItemBaseSO activeItemsBaseSO;
        private ActiveItem activeItem;
        [SerializeField]
        private ActiveItemsManager activeItemsManager;

        private SpriteRenderer sR;
        void Awake() 
        {
            sR = GetComponentInChildren<SpriteRenderer>();
        }
        
        public void PickUp()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(string itemBaseKey)
        {
            activeItem = activeItemsBaseSO.ActiveItemBase[itemBaseKey];
            GetComponent<NameAndMessageHighlight>().Name = activeItem.Name;
            sR.sprite = activeItem.Icon;
        }

        public void Interact()
        {
            activeItemsManager.OnPickUp(activeItem);
            AudioManager.instance.PlaySound("PickUp");
            Destroy(gameObject);
        }
    }
}
