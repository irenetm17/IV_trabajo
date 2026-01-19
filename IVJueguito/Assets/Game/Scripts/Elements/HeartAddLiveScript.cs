using MoreMountains.Feedbacks;
using UnityEngine;

public class HeartAddLiveScript : Entity
{
    [SerializeField] private MMFeedbacks MMF_Player;

    private void Awake()
    {
        base.Awake();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatsEvent vidasSumar = new PlayerStatsEvent(1f, 0);
            EventManager.instance.Publicar(vidasSumar);
            MMF_Player.PlayFeedbacks();
            Destroy(gameObject,0.5f);
        }
    }

}
