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
        
        public void OnPickUp(ActiveItem activeItem)
        {
            if (carriedItems.Count == maximumCarriedItems)
            {
                // drop out chosenActive item and pick up proper one
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