using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public float[] cooldowns = { 1f, 2f, 2f, 2f };
    private float[] lastUseTime = new float[4];

    [SerializeField] private GameObject diam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) TryUseAbility(0); // EN ESTOS SITIOS PONER LAS TECLAS QUE HAY QUE PULSAR PARA CADA HABILIDAD
        if (Input.GetKeyDown(KeyCode.Alpha2)) TryUseAbility(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) TryUseAbility(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) TryUseAbility(3);
    }

    void TryUseAbility(int index)
    {
        if (Time.time < lastUseTime[index] + cooldowns[index]) return;

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
        diam.SetActive(true);
        StartCoroutine(Wait(0.5f));
        diam.SetActive(false);
    }

    /* PONER ESTO DE AQUI EN UNA CLASE QUE TENGA EL COLLIDER EN EL OBJETO diam 
    void OnTriggerEnter(Collider other)
    {
        PlayerMovement enemy = other.GetComponent<PlayerMovement>();//poner clase enemigo y no clase playermovement
        if (enemy!=null)
        {
            // Bajar vida a enemigos
        }
    }*/

    void AbilityRuby()
    {
        //aqui habra que generar un proyectil que creo que estara en el object pool
    }

    void AbilitySapphire()
    {
        //para esto necesito un poco de imaginacion aun
    }

    void AbilityEmerald()
    {
        //esto necesito la clase proyectil para ello 
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
