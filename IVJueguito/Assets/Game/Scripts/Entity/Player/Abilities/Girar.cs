using UnityEngine;

public class Girar : MonoBehaviour
{
    public float speed = 90f; // grados por segundo

    void Update()
    {
        // Calcular nueva rotación Y en espacio global y forzar X/Z a 0
        float newY = (transform.eulerAngles.y + speed * Time.deltaTime) % 360f;
        transform.rotation = Quaternion.Euler(0f, newY, 0f);
    }
}
