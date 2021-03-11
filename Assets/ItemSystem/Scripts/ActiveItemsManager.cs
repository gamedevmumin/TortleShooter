using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem.Scripts
{
    [CreateAssetMenu(fileName = "ActiveItemsManager", menuName = "ActiveItemsManager")]
    public class ActiveItemsManager : ScriptableObject,  ISerializationCallbackReceiver
    {
        private List<ActiveItem> carriedItems;
        private ActiveItem chosenActiveItem;
        private int maximumCarriedItems;
        [SerializeField] private GameEvent activeItemPickedUp;
        [SerializeField] private GameObject pickableItemPrefab;
        
        public ActiveItem ChosenActiveItem => chosenActiveItem;

        public void Initialize()
        {
            carriedItems = new List<ActiveItem>();
            chosenActiveItem = null;
            maximumCarriedItems = 1;
        }

        public void RemoveChosenItem()
        {
            carriedItems.Remove(chosenActiveItem);
            chosenActiveItem = null;
            Debug.Log("hmm?");
            activeItemPickedUp.Raise();
        }
        
        public void ActivateChosenItem()
        {

            if (chosenActiveItem != null)
            {
                chosenActiveItem.Activate();
            }
        }
        
        public void OnPickUp(ActiveItem activeItem, Transform transform)
        {
            if (carriedItems.Count == maximumCarriedItems)
            {
                carriedItems.Remove(chosenActiveItem);
                var pickableItem =  Instantiate(pickableItemPrefab, transform.position, transform.rotation).GetComponent<IPickable>();
                pickableItem.Initialize(chosenActiveItem.ItemBaseKey);
                chosenActiveItem = activeItem;
                carriedItems.Add(activeItem);
                activeItem.OnPickUp();
                activeItemPickedUp.Raise();
                return;
            }
            carriedItems.Add(activeItem);
            chosenActiveItem = carriedItems[0];
            activeItem.OnPickUp();
            activeItemPickedUp.Raise();

        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            Initialize();
        }
    }
}