using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float hassasiyet = 2f; // Fare hýzý
    private float rotasyonX = 0f;
    private float rotasyonY = 0f;

    void Start()
    {
        // Fareyi oyunun içine hapseder (Ekranda görünmez, rahat niþan alýrsýn)
        // ESC tuþuna basarak fareden kurtulabilirsin
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Fare hareketlerini al
        float fareX = Input.GetAxis("Mouse X") * hassasiyet;
        float fareY = Input.GetAxis("Mouse Y") * hassasiyet;

        // Yatay ve dikey rotasyonu hesapla
        rotasyonY += fareX;
        rotasyonX -= fareY;

        // Yukarý-aþaðý bakýþ açýsýný sýnýrla (Takla atmamasý için)
        rotasyonX = Mathf.Clamp(rotasyonX, -90f, 90f);

        // Kamerayý döndür
        transform.eulerAngles = new Vector3(rotasyonX, rotasyonY, 0);
    }
}