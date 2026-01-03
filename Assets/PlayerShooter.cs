using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Ayarlar")]
    public Camera cam;
    public Animator crosshairAnimator;
    public GameManager gameManager;
    public GameObject hitEffectPrefab;

    [Range(0.1f, 1f)]
    public float mermiKalinligi = 0.2f; // Mermiyi bir iðne deðil, bir top gibi kalýnlaþtýrýr

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void LateUpdate() { }

    void Shoot()
    {
        if (crosshairAnimator != null)
            crosshairAnimator.SetTrigger("ShootTrigger");

        // Ekran ortasýndan ýþýn
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // IÞIN YERÝNE KÜRE (SphereCast) KULLANIMI: 
        // Bu, mermiyi kalýnlaþtýrýr ve "hedefin yanýndan geçti ama vurmadý" sorununu çözer.
        if (Physics.SphereCast(ray, mermiKalinligi, out hit, 500f))
        {
            Debug.Log("Vurulan Nesne: " + hit.transform.name);

            if (hit.transform.CompareTag("Target"))
            {
                if (gameManager != null) gameManager.AddScore();

                if (hitEffectPrefab != null)
                {
                    GameObject fx = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(fx, 1f);
                }

                Destroy(hit.transform.gameObject);
                Debug.Log("<color=green>HEDEF TAM ISABET!</color>");
            }
        }
        else
        {
            Debug.Log("<color=red>Mermi havayý vurdu, hiçbir þeye çarpmadý.</color>");
        }
    }
}