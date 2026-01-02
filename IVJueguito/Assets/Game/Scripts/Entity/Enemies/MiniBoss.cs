using UnityEngine;

public class MiniBoss : Enemy
{
        // 1. Propiedad para no tener que castear en los estados
        // "Oye, dame mis datos, pero dámelos ya con la etiqueta correcta"
        public MiniBossFlyweight MiniBossData
        {
            get { return (MiniBossFlyweight)flyweightData; }
        }

    // 2. Habilidad Física encapsulada (limpia los estados)

        public void pushPlayer(float force, float radius)
        {
            // Lógica de búsqueda y empuje (la sacamos del Estado para ponerla aquí)
            if (DistanceWithPlayer() < radius)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Vector3 dir = (player.transform.position - transform.position).normalized;
                    dir.y = 0;

                    Rigidbody rb = player.GetComponent<Rigidbody>();
                    // Multiplicamos por deltaTime aquí o en el FixedUpdate
                    if (rb) rb.AddForce(dir * force * Time.deltaTime * 50f, ForceMode.Force);
                }
            }
        }

        // Aquí puedes poner más cosas: LanzarRayo(), Gritar(), etc.
    }
}
