using System.Collections.Generic;
using UnityEngine;

public class UIDialogPaginate : MonoBehaviour
{
    [SerializeField] List<GameObject> m_paginatedGOs = new List<GameObject>();

    public int Count { get => m_paginatedGOs.Count; }
    public int Current { get; private set; }

    private void OnEnable()
    {
        Current = 0;
        SetActiveGO(m_paginatedGOs[Current]);
    }

    public void Btn_Next()
    {
        Current++;
        if (Current >= Count) Current = 0;
        SetActiveGO(m_paginatedGOs[Current]);
    }

    public void Btn_Prev()
    {
        Current--;
        if (Current < 0) Current = Count -1;
        SetActiveGO(m_paginatedGOs[Current]);
    }

    void SetActiveGO(GameObject active) =>
       m_paginatedGOs.ForEach(val => val.SetActive(val.name == active.name));
    
}
