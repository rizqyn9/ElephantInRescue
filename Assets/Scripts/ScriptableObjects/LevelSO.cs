using UnityEngine;

[CreateAssetMenu(menuName = "EIR/Create Level")]
public class LevelSO : DescriptionBaseSO
{
    public int Level;
    public int Stage;
    public int CountDown;
    public GameObject GO_Tutorial;
    public GameObject GO_MainComponent;

    public bool ShouldTutorialUI { get => GO_Tutorial != null; }

    private void OnValidate()
    {
        name = $"Stg{Stage}-Lvl{Level}";
    }
}
