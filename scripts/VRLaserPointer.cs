using System.Collections;
using System.Collections.Generic; // List<> için gerekli
using UnityEngine;
using UnityEngine.EventSystems; // PointerEventData için gerekli

public class VRLaserPointer : MonoBehaviour
{
    public GameObject laserPrefab; // Lazer prefab'ý
    public Transform vrHead; // VR oyuncusunun baþý (Camera)
    public float maxDistance = 10.0f; // Lazerin maksimum mesafesi
    public Color laserColor = Color.red; // Lazerin rengi

    private GameObject laser; // Instantiate edilen lazer
    private LineRenderer lineRenderer; // LineRenderer bileþeni
    private RaycastHit hit; // Raycast sonucu

    void Start()
    {
        // Laser prefab'ýný instantiate et
        laser = Instantiate(laserPrefab, transform);
        lineRenderer = laser.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = laser.AddComponent<LineRenderer>();
        }

        // LineRenderer ayarlarýný yap
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }

    void Update()
    {
        // Ray'ýn baþlangýç noktasý ve yönü
        Vector3 origin = vrHead.position;
        Vector3 direction = vrHead.forward;

        // Lazerin baþlangýç noktasýný ayarla
        lineRenderer.SetPosition(0, origin);

        // Raycast yap
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            // Lazerin bitiþ noktasýný ayarla
            lineRenderer.SetPosition(1, hit.point);

            // UI ile etkileþimi kontrol et
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            // Hit noktasý ekran koordinatlarýna dönüþtürülür
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(hit.point);
            pointerData.position = new Vector2(screenPoint.x, screenPoint.y);

            // UI elemanlarýný tespit et
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                // Ýlk UI elemanýna týklama iþlemi
                ExecuteEvents.Execute(results[0].gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            // Lazerin bitiþ noktasýný maksimum mesafeye ayarla
            lineRenderer.SetPosition(1, origin + direction * maxDistance);
        }
    }
}