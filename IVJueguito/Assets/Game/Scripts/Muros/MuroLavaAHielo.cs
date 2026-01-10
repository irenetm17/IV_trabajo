using UnityEngine;

public class MuroLavaAHielo : MonoBehaviour
{
    [SerializeField]
    private bool _isHurtful = true;

    [SerializeField]
    private Material _lavaShader;
    [SerializeField]
    private Material _hieloShader;

    [SerializeField]
    private Collider _muroCollider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (_isHurtful)
            {
                // Hacerle daño
                PlayerStatsEvent vidasRestar = new PlayerStatsEvent(-0.5f, 0);
                EventManager.instance.Publicar(vidasRestar);
            }

        }
        Sapphire tempano = other.GetComponent<Sapphire>();
        if (tempano != null)
        {
            // Desactivar hacer daño al jugador
            _isHurtful = false;

            // Quitar muro
            _muroCollider.enabled = false;

            // Cambiar de shader de lava a hielo
            gameObject.GetComponent<MeshRenderer>().material = _hieloShader;

        }
    }

}
