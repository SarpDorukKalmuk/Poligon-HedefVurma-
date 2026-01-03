using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Hedef Ayarlari")]
    public GameObject targetPrefab;   // Madde 1: Prefab
    public Transform[] spawnPoints;   // Madde 8: Arrays

    [Header("UI Ayarlari")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private int score = 0;
    private float timer = 30f;        // 30 saniye kuralý
    private float nextSpawnTime = 0f;

    void Start()
    {
        if (scoreText != null) scoreText.text = "Puan: 0";
    }

    // Madde 6: FixedUpdate (Fizik tabanlý güncellemeler için)
    void FixedUpdate() { }

    void Update()
    {
        // Madde 7: DeltaTime kullanýmý
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timerText != null) timerText.text = "Sure: " + Mathf.CeilToInt(timer).ToString();

            // Hedef oluþturma zamaný kontrolü
            if (Time.time >= nextSpawnTime)
            {
                SpawnTarget();
                nextSpawnTime = Time.time + 1.2f; // Hedeflerin çýkýþ hýzý
            }
        }
        else
        {
            if (timerText != null) timerText.text = "OYUN BITTI";
        }
    }

    void SpawnTarget()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        // Madde 8: Dizi içinden rastgele konum seç
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform sp = spawnPoints[randomIndex];

        if (targetPrefab != null)
        {
            // Madde 4 & 2: Instantiate, Vector3 (position) ve Quaternion (rotation)
            GameObject newTarget = Instantiate(targetPrefab, sp.position, sp.rotation);

            // 1 SANIYE KURALI: Vurulmazsa 1 saniye sonra yok et
            Destroy(newTarget, 1f);
        }
    }

    public void AddScore()
    {
        score++;
        if (scoreText != null) scoreText.text = "Puan: " + score.ToString();
    }
}