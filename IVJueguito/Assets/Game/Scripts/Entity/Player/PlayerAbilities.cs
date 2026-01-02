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

    [SerializeField] private GameObject diam;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballSpawnPoint;
    [SerializeField] private LayerMask ground;


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

    void AbilitySapphire() { }
    void AbilityEmerald() { }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        diam.SetActive(false);
    }
}
