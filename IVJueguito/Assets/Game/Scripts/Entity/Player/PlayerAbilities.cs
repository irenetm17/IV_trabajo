using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    public float[] cooldowns = { 1f, 2f, 2f, 2f };
    private float[] lastUseTime = new float[4];

    // HAY QUE USAR ESTA MIERDA DE BRUJERIA RARA QUE FUNCIONE ME CAGO EN LA HOSTIA
    [SerializeField] private InputActionReference ability0; 
    [SerializeField] private InputActionReference ability1;
    [SerializeField] private InputActionReference ability2;
    [SerializeField] private InputActionReference ability3;

    [SerializeField] private GameObject diam;

    void OnEnable()
    {
        ability0.action.Enable();
        ability1.action.Enable();
        ability2.action.Enable();
        ability3.action.Enable();
    }

    void OnDisable()
    {
        ability0.action.Disable();
        ability1.action.Disable();
        ability2.action.Disable();
        ability3.action.Disable();
    }

    void Update()
    {
        if (ability0.action.WasPressedThisFrame())
        {
            Debug.Log("Ability0 input detectado");
            TryUseAbility(0);
        }
        if (ability1.action.WasPressedThisFrame())
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

    void AbilityRuby() { }
    void AbilitySapphire() { }
    void AbilityEmerald() { }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        diam.SetActive(false);
    }
}
