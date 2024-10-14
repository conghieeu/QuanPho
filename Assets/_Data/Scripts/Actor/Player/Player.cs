using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int score;

    public int Score { get => score; set => score = value; }
}
