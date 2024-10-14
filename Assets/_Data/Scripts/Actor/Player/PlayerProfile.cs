using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [SerializeField] int score;

    public int Score { get => score; set => score = value; }
}
