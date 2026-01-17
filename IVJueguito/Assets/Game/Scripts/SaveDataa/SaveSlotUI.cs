using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlotUI : MonoBehaviour
{
    public int slotIndex;

    [SerializeField] private Image[] hearts;
    [SerializeField] private GameObject[] gems;
    [SerializeField] private Image[] keys;

    void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        SaveManager.instance.SetSlot(slotIndex);

        if (SaveManager.instance.HasSave())
        {
            SaveData data = SaveManager.instance.LoadGame();
            UpdateHearts(data.playerLives);
            UpdateGems(data.playerGems);
            UpdateKeys(data.playerKeys);
        }
        else
        {
            UpdateHearts(3);
            UpdateGems(0);
            UpdateKeys(0);
        }
    }

    public void OnClickPlay() //al hacer clic
    {
        GameSession.selectedSlot = slotIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Scene");
    }
    public void OnClickDelete()
    {
        SaveManager.instance.SetSlot(slotIndex);
        SaveManager.instance.DeleteSave();
        Refresh();
    }

    #region VIDAS, GEMAS, LLAVES
    private void UpdateHearts(float l)
    {
        float remainingHealth = l;

        for (int i = 0; i < hearts.Length; i++)
        {
            float fill = Mathf.Clamp01(remainingHealth);//devuelve un valor entre 0 y 1, si es mas de 1 da 1
            hearts[i].fillAmount = fill;

            remainingHealth -= 1f;
        }
    }
    private void UpdateGems(int g)
    {
        for (int i = 0; i < gems.Length; i++)
        {
            if (i < g)
            {
                gems[i].SetActive(true);
            }
            else
            {
                gems[i].SetActive(false);
            }
        }
    }
    private void UpdateKeys(int k)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (i < k)
            {
                keys[i].enabled = true;
            }
            else
            {
                keys[i].enabled = false;
            }
        }
    }
    #endregion
}
