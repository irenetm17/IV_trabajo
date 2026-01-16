using UnityEngine;

public class PlayerStats : MonoBehaviour, IObserver
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float currentHealth = 1f;
    [SerializeField] private int numLlaves = 0;

    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this); 
        EventManager.instance.Subscribir(eventType.UseKey, this);
        if (currentHealth <= 0) currentHealth = maxHealth;
        ComprobarMuerte();
    }

    public void OnEvent(IEvent evento)
    {
        float varVida = 0f;
        bool actualizo = false;
        if (evento.Tipo == eventType.PlayerStatsUpdated)
        {
            PlayerStatsEvent event2 = (PlayerStatsEvent)evento; //desempaqueta
            varVida = event2.health;
            actualizo = true;
        }

        else if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent event2 = (CollectibleEvent)evento;
            if (event2.tipo == CollectibleType.Corazones)
            {
                varVida = event2.amount;
                actualizo = true;
            }
            else if (event2.tipo == CollectibleType.Llaves)
            {
                numLlaves += (int)event2.amount;
            }
        }
        else if (evento.Tipo == eventType.UseKey) 
        {
            numLlaves--;
            Debug.Log($"Llave usada. Restantes: {numLlaves}");
        }

        if (actualizo) {
            currentHealth += varVida;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            ComprobarMuerte();
        }
    }
    private void ComprobarMuerte()
    {
        if (currentHealth <= 0f)
        {
            if(_animator != null)
            {
                _animator.SetBool("alive", false);
            }
            SimpleEvent muerte = new SimpleEvent(eventType.PlayerDied);
            EventManager.instance.Publicar(muerte);
        }
    }
}