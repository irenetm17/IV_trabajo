using MoreMountains.Feedbacks;
using UnityEngine;

public class MiniBoss : Enemy
{
    [HideInInspector] public bool triggerSpecial = false;
    [HideInInspector] public bool special1Done = false;
    [HideInInspector] public bool special2Done = false;
    [HideInInspector] public bool activateParticles = true;
    [SerializeField] ParticleSystem windArea;
    [SerializeField] ParticleSystem stunned;
    //[SerializeField] PuertaAutomatica puertaVinculada;
    public SlimeSpawner sp = null;

    // FEEDBACKS
    [SerializeField] private MMFeedbacks MMF_Explosion;
    [SerializeField] private MMFeedbacks MMF_PushBack;
    [SerializeField] private MMFeedbacks MMF_Wind;

    public MiniBossFlyweight MiniBossData
        {
            get { return (MiniBossFlyweight)flyweightData; }
        }

    public override void KillEnemy()
    {
        //DoorOpenedEvent eventoAbrir = new DoorOpenedEvent(puertaVinculada, true);
        //EventManager.instance.Publicar(eventoAbrir);
    }

    public void pushPlayer(float force, float radius)
        {
            if(activateParticles)
            {
                windArea.Play();
                MMF_Wind.PlayFeedbacks();
                activateParticles = false;
                Debug.Log("Efecto de viento encendido");
            }

            GameObject player = GameObject.FindWithTag("Player");
            if (DistanceWithPlayer() < radius)
            {

            Debug.Log("Estoy entrando al empuje");
                    Vector3 dir = (player.transform.position - transform.position).normalized;
                    dir.y = 0;
                AudioService.instance.PlaySFX("GolpeGolem");

            Rigidbody rb = player.GetComponent<Rigidbody>();


            //player.transform.Translate(dir * force * Time.deltaTime, Space.World);
            rb.linearVelocity += dir * force * Time.deltaTime;
            rb.AddForce(dir * force, ForceMode.Force);
        }
    }

    public void SpawnSlimes()
    {
        Vector3 offset = new Vector3(20, 0, 0);

        Vector3 p1 = playerTransform.position + offset; 
        Vector3 p2 = playerTransform.position - offset;
        if (sp != null)
        {
            sp.SpawnSlimes(p1);
            sp.SpawnSlimes(p2);
        }
    }

    public void Impulse(float force)
    {
        AudioService.instance.PlaySFX("GolpeGolem");

        GameObject player = GameObject.FindWithTag("Player");

        Rigidbody rb = player.GetComponent<Rigidbody>();

        Vector3 dir = (player.transform.position - transform.position).normalized;

        rb.linearVelocity += dir * force * Time.deltaTime;
        rb.AddForce(dir * force, ForceMode.Impulse);
    }
    public void StopWind()
    {
        windArea.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        activateParticles = true;
        Debug.Log("Efecto apagado");
    }
    public void StunEffect()
    {

        stunned.Play();
    
        Debug.Log("Efecto de stun encendido");
    }

    public void SpecialAttack()
    {

        MMF_Explosion.PlayFeedbacks();


        Debug.Log("Efecto de explosión encendido");

    }

    public void pushBack()
    {

        MMF_PushBack.PlayFeedbacks();


        Debug.Log("Efecto de explosión encendido");

    }

    protected override void Update()
    {
        base.Update();

        if (currentHp <= 20 && special1Done == false)
        {
            this.ChangeState(this.MiniBossData.specialGimmickBossState);
            special1Done = true;
        }

        if (currentHp <= 10 && special2Done == false)
        {
            this.ChangeState(this.MiniBossData.specialGimmickBossState);
            special2Done = true;
        }

    }

}
