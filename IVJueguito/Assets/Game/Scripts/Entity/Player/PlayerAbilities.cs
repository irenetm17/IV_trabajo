using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    public float[] cooldowns = { 1f, 2f, 2f, 2f };
    private float[] lastUseTime = new float[4];

    // HAY QUE USAR ESTA MIERDA DE BRUJERIA RARA QUE FUNCIONE ME CAGO EN LA HOSTIA
    public InputActionReference ability2;
    public InputActionReference ability3;

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

        if (Mouse.current.leftButton.IsPressed()) // Lo del raton de las narices
        {
            Debug.Log("Ability0 input detectado");
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

    void TryUseAbility(int index)
    {
        if (Time.time < lastUseTime[index] + cooldowns[index])
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
        StartCoroutine(Wait(0.5f));
    }

    void AbilityRuby()
    {
        Ray ray = Camera.main.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

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

            Ruby ruby = fireball.GetComponent<Ruby>();
            ruby.Init(direction);
        }
    }

    void AbilitySapphire()
    {
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
    }
}
