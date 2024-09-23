using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Player> players;
    public List<Customer> customers;
    public float customerSpawnRate = 5f; // Tỷ lệ spawn khách hàng
    private float spawnTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        players = new List<Player>();
        customers = new List<Customer>();
        spawnTimer = customerSpawnRate;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnCustomer();
            spawnTimer = customerSpawnRate;
        }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);
    }

    public void SpawnCustomer()
    {
        // Tạo khách hàng mới và thêm vào danh sách
        Customer newCustomer = new Customer();
        customers.Add(newCustomer);
        // Logic để đặt khách hàng vào vị trí ngẫu nhiên
    }

    public void RemoveCustomer(Customer customer)
    {
        customers.Remove(customer);
    }

    public void UpdateScore(Player player, int score)
    {
        player.Score += score;
        // Cập nhật UI hoặc các thành phần khác nếu cần
    }

    public void GameOver()
    {
        // Logic khi trò chơi kết thúc
    }
}