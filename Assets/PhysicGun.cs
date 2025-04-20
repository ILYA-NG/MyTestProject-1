using System.Collections;
using UnityEngine;

public class PhysicGun : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;
    [SerializeField, Range(0.2f, 2f)]
    private float _delay = 2f;
    [SerializeField, Range(0.5f, 100f)]
    private float _velocity = 15f;
    [SerializeField, Min(.1f)]
    private float _mass = 5f;
    
    // Добавляем префаб снаряда
    public GameObject bulletPrefab;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            
                    // Вывод координат и направления в консоль
            Debug.Log($"_firePoint.position: {_firePoint.position}, _firePoint.forward: {_firePoint.forward}");

            // Создаем экземпляр префаба
            GameObject bullet = Instantiate(bulletPrefab, _firePoint.position, _firePoint.rotation);
            
            // Добавляем rigidbody и настраиваем его
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb == null)
                rb = bullet.AddComponent<Rigidbody>();
            rb.mass = _mass;
            rb.velocity = _firePoint.forward * _velocity;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            Destroy(bullet, 10f); // Уничтожаем снаряд через 10 секунд

            // Визуализация траектории полета снаряда
            Debug.DrawLine(_firePoint.position, _firePoint.position + _firePoint.forward * 5f, Color.red, 30f);
        }
    }
}