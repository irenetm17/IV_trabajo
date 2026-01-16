using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;


public class PlayerAbilities : MonoBehaviour, IObserver
{
    public float[] cooldowns = { 1f, 2f, 2f, 2f };
    [SerializeField] private Image[] cooldownImages;
    [SerializeField] private Image[] bloqueadosImages;

    private bool canMove = true;
    private float[] lastUseTime = new float[4];

    // HAY QUE USAR ESTA MIERDA DE BRUJERIA RARA QUE FUNCIONE ME CAGO EN LA HOSTIA
    public InputActionReference ability2;
    public InputActionReference ability3;

    [SerializeField] private int gemas = 0;//serializable para pruebas

    private Animator animator;

    [Header("DIAMANTE")]
    [SerializeField] private GameObject diam;

    [Header("RUBI")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballSpawnPoint;
    [SerializeField] private LayerMask ground;

    [Header("ZAFIRO")]
    [SerializeField] private GameObject sapphireZone;
    [SerializeField] private float sapphireGrowTime = 0.3f;
    [SerializeField] private float sapphireActiveTime = 1.5f;

    [Header("ESMERALDA")]
    [SerializeField] private GameObject emeraldColider;
    [SerializeField] private SpriteRenderer emeraldSprite;
    [SerializeField] private float emeraldFadeTime = 0.4f;
    [SerializeField] private float emeraldActiveTime = 1.5f;

    void Start()
    {
        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
        EventManager.instance.Subscribir(eventType.PlayerCanMove, this);

        animator = GetComponentInChildren<Animator>();
    }
    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.PlayerStatsUpdated)
        {
            PlayerStatsEvent event2 = (PlayerStatsEvent)evento; //desempaqueta

            if(event2.gems != 0)
            {
                gemas += event2.gems;

                if (gemas > 0 && gemas <=4)
                {
                    bloqueadosImages[gemas - 1].gameObject.SetActive(false); //desbloquea la habilidad correspondiente
                }
            }
        }

        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent event4 = (CollectibleEvent)evento; //desempaqueta
            if(event4.tipo == CollectibleType.Gema)
            {
                gemas += event4.amount;
                if (gemas > 0)
                {
                    bloqueadosImages[gemas - 1].gameObject.SetActive(false); //desbloquea la habilidad correspondiente
                }
            }
        }

        if (evento.Tipo == eventType.PlayerCanMove)
        {
            canMove = !canMove;
        }
    }
    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.Desuscribir(eventType.PlayerStatsUpdated, this);
            EventManager.instance.Desuscribir(eventType.CollectiblePicked, this);
            EventManager.instance.Desuscribir(eventType.PlayerCanMove, this);
        }
    }

    void OnEnable()
    {
        ability2.action.Enable();
        ability3.action.Enable();
    }

    void OnDisable()
    {
        ability2.action.Disable();
        ability3.action.Disable();
    }

    void Update()
    {
        UpdateCooldownUI();
        if (!canMove) return;
        if (Mouse.current.leftButton.IsPressed()) // Lo del raton de las narices
        {
            TryUseAbility(0);
        }
        if (Mouse.current.rightButton.IsPressed())
        {
            TryUseAbility(1);
        }
        if (ability2.action.WasPressedThisFrame())
        {
            TryUseAbility(2);
        }
        if (ability3.action.WasPressedThisFrame())
        {
            TryUseAbility(3);
        }
    }
    void UpdateCooldownUI()
    {
        for (int i = 0; i < cooldownImages.Length; i++)
        {
            float cooldown = cooldowns[i];
            float timePassed = Time.time - lastUseTime[i];
            float remaining = Mathf.Clamp01(1 - (timePassed / cooldown));
            cooldownImages[i].fillAmount = remaining;
        }
    }


    void TryUseAbility(int index)
    {
        if ((Time.time < lastUseTime[index] + cooldowns[index]) || gemas < index+1)
            return;

        lastUseTime[index] = Time.time;

        switch (index)
        {
            case 0: AbilityDiamond(); break;
            case 1: AbilityRuby(); break;
            case 2: AbilitySapphire(); break;
            case 3: AbilityEmerald(); break;
        }
    }

    void AbilityDiamond()
    {
        Debug.Log("AbilityDiamond ejecutada");
        diam.SetActive(true);
        animator.SetBool("attacking", true);
        AudioService.instance.PlaySFX("UsarDiamante");
        StartCoroutine(Wait(0.5f));
    }

    void AbilityRuby()
    {
        Ray ray = Camera.main.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );
        bool izq = (Mouse.current.position.ReadValue().x < (Screen.width * 0.5f));

        AudioService.instance.PlaySFX("UsarRubi");
        if (Physics.Raycast(ray, out RaycastHit hit, 500f, ground))
        {
            Vector3 targetPoint = hit.point;

            // Evita disparar hacia abajo
            targetPoint.y = fireballSpawnPoint.position.y;

            Vector3 direction = (targetPoint - fireballSpawnPoint.position).normalized;

            GameObject fireball = Instantiate(
                fireballPrefab,
                fireballSpawnPoint.position,
                Quaternion.identity
            );
            fireball.transform.localScale = new Vector3(
                izq ? 3f : -3f,
                3f,
                (izq ? 3f : -3f)
            );
            Ruby ruby = fireball.GetComponent<Ruby>();
            ruby.Init(direction);
        }
    }

    void AbilitySapphire()
    {

        AudioService.instance.PlaySFX("UsarHielo");
        StartCoroutine(SapphireRoutine());
    }
    IEnumerator SapphireRoutine()
    {
        sapphireZone.SetActive(true);
        
        foreach (Transform child in sapphireZone.transform) // Activar todos los hijos
        {
            child.gameObject.SetActive(true);
            Sapphire s = child.GetComponent<Sapphire>();
            s.Init();
        }

        sapphireZone.transform.localScale = Vector3.zero;
        float t = 0f;
        while (t < sapphireGrowTime)
        {
            t += Time.deltaTime;
            float progress = t / sapphireGrowTime;
            sapphireZone.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress); // escalado de 0 a 1
            yield return null;
        }
        sapphireZone.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(sapphireActiveTime);

        sapphireZone.SetActive(false);
    }

    void AbilityEmerald()
    {

        AudioService.instance.PlaySFX("UsarEsmeralda");
        StartCoroutine(EmeraldRoutine());
    }
    IEnumerator EmeraldRoutine()
    {
        emeraldColider.SetActive(true);
        emeraldSprite.gameObject.SetActive(true);

        
        Color c = emeraldSprite.color;// Empezar invisible
        c.a = 0f;
        emeraldSprite.color = c;
        float t = 0f;
        while (t < emeraldFadeTime)// Fade in
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / emeraldFadeTime);
            c.a = alpha;
            emeraldSprite.color = c;
            yield return null;
        }
        c.a = 1f;
        emeraldSprite.color = c;

        yield return new WaitForSeconds(emeraldActiveTime);

        emeraldColider.SetActive(false);
        emeraldSprite.gameObject.SetActive(false);
    }


    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        diam.SetActive(false);
        animator.SetBool("attacking", false);
    }
}
