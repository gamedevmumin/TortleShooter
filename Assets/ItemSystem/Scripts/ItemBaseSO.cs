using UnityEngine;

namespace ItemSystem.Scripts
{
    [CreateAssetMenu(fileName = "ItemBase", menuName = "ItemBase", order = 0)]
    public class ItemBaseSO : ScriptableObject
    {
        [SerializeField] private ActiveItemBase activeItemBase;

        public ActiveItemBase ActiveItemBase => activeItemBase;
    }
}