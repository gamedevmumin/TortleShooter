using UnityEngine;

namespace ItemSystem.Scripts
{
    public abstract class ActiveItem : Item, IActiveItem
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite itemIcon;
        
        public override string Name
        {
            get => itemName;
            protected set => itemName = value;
        }

        public override Sprite Icon
        {
            get => itemIcon;
            protected set => itemIcon = value;
        }
        
        public override PickableItem PickableItem { get; protected set; }

        public abstract override void OnPickUp();

        public abstract void Activate();
    }
}