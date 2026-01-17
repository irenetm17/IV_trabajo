using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlotUI : MonoBehaviour
{
    public int slotIndex;

    public TMP_Text livesText;
    public TMP_Text gemsText;
    public TMP_Text keysText;

    public void Refresh()
    {
        SaveManager.instance.SetSlot(slotIndex);

        if (SaveManager.instance.HasSave())
        {
            SaveData data = SaveManager.instance.LoadGame();
            livesText.text = data.playerLives.ToString();
            gemsText.text = data.playerGems.ToString();
            keysText.text = data.playerKeys.ToString();
        }
        else
        {
            livesText.text = "3";
            gemsText.text = "0";
            keysText.text = "0";
        }
    }

    public void OnClickPlay() //al hacer clic
    {
        GameSession.selectedSlot = slotIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void OnClickDelete()
    {
        SaveManager.instance.SetSlot(slotIndex);
        SaveManager.instance.DeleteSave();
        Refresh();
    }
}
