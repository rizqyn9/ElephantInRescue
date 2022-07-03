using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handle all inventory state
/// Eg: Root Stone
/// </summary>
[CreateAssetMenu(menuName = "Events/Inventory Controller")]
[System.Serializable]
public class InventorySO : DescriptionBaseSO
{
    public UnityAction<int, int> OnEventRaised;

    [SerializeField] int m_count;
    public int Count
    {
        get => m_count;
        internal set
        {
            OnEventRaised.Invoke(m_count, value);
            m_count = value;
        }
    }

    public void Set(int value)
    {
        Count = value;
    }

    public void Remove()
    {
        Count--;
    }

    public void Add()
    {
        Count++;
    }

    public void Reset()
    {
        Count = 0; 
    }
}
