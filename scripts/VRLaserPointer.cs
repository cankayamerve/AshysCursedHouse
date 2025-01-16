using System.Collections;
using System.Collections.Generic; // List<> i�in gerekli
using UnityEngine;
using UnityEngine.EventSystems; // PointerEventData i�in gerekli

public class VRLaserPointer : MonoBehaviour
{
    public GameObject laserPrefab; // Lazer prefab'�
    public Transform vrHead; // VR oyuncusunun ba�� (Camera)
    public float maxDistance = 10.0f; // Lazerin maksimum mesafesi
    public Color laserColor = Color.red; // Lazerin rengi

    private GameObject laser; // Instantiate edilen lazer
    private LineRenderer lineRenderer; // LineRenderer bile�eni
    private RaycastHit hit; // Raycast sonucu

    void Start()
    {
        // Laser prefab'�n� instantiate et
        laser = Instantiate(laserPrefab, transform);
        lineRenderer = laser.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = laser.AddComponent<LineRenderer>();
        }

        // LineRenderer ayarlar�n� yap
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }

    void Update()
    {
        // Ray'�n ba�lang�� noktas� ve y�n�
        Vector3 origin = vrHead.position;
        Vector3 direction = vrHead.forward;

        // Lazerin ba�lang�� noktas�n� ayarla
        lineRenderer.SetPosition(0, origin);

        // Raycast yap
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            // Lazerin biti� noktas�n� ayarla
            lineRenderer.SetPosition(1, hit.point);

            // UI ile etkile�imi kontrol et
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            // Hit noktas� ekran koordinatlar�na d�n��t�r�l�r
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(hit.point);
            pointerData.position = new Vector2(screenPoint.x, screenPoint.y);

            // UI elemanlar�n� tespit et
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                // �lk UI eleman�na t�klama i�lemi
                ExecuteEvents.Execute(results[0].gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            // Lazerin biti� noktas�n� maksimum mesafeye ayarla
            lineRenderer.SetPosition(1, origin + direction * maxDistance);
        }
    }
}