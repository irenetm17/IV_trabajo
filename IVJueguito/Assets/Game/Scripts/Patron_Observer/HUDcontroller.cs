using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HUDcontroller : MonoBehaviour, IObserver
{
    [Header("VIDAS")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float currentHealth = 3f;

    [Header("LLAVES")]
    private int numLlaves = 0;
    [SerializeField] private Image[] keys;

    [Header("DIALOGOS")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text textoDialogo;
    private string[] arrayTextosDialogos;
    private float typingTime = 0.05f;
    private int lineIndex;
    private bool didDialogueStart;

    [Header("PAUSA")]
    private bool isPaused = false;
    [SerializeField] private GameObject BotonPausa;
    [SerializeField] private GameObject BotonDespausa;
    [SerializeField] private GameObject panelPausa;

    [SerializeField] private GameObject PanelMuerte;
    [SerializeField] private Slider volumen;

    public void OnEvent(IEvent evento)
    {
        if (evento.Tipo == eventType.PlayerStatsUpdated)
        {
            PlayerStatsEvent event2 = (PlayerStatsEvent)evento; //desempaqueta

            currentHealth += event2.health;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UpdateHearts();
        }

        if (evento.Tipo == eventType.CollectiblePicked)
        {
            CollectibleEvent event4 = (CollectibleEvent)evento; //desempaqueta
            if(event4.tipo == CollectibleType.Corazones)
            {
                currentHealth += event4.amount;
                currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
                UpdateHearts();
            }
            if (event4.tipo == CollectibleType.Llaves)
            {
                numLlaves += event4.amount;
                UpdateKeys();
            }
        }
        if (evento.Tipo == eventType.UseKey)
        {
            numLlaves--;
            UpdateKeys();
        }
        if (evento.Tipo == eventType.GamePaused)
        {
            //CollectibleEvent event5 = (CollectibleEvent)evento;
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
                BotonDespausa.SetActive(true);
                BotonPausa.SetActive(false);
                panelPausa.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                BotonPausa.SetActive(true);
                BotonDespausa.SetActive(false);
                panelPausa.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }


        }

        if (evento.Tipo == eventType.DialogueStarted)
        {
            DialogueStartedEvent event3 = (DialogueStartedEvent)evento;
            StartDialogue(event3.arrayTextos);
        }

        if (evento.Tipo == eventType.PlayerDied)
        {
            Time.timeScale = 0f;
            PlayerCanMoveEvent quieto = new PlayerCanMoveEvent(false);
            EventManager.instance.Publicar(quieto);
            PanelMuerte.SetActive(true);
            panelPausa.SetActive(false);
            BotonDespausa.SetActive(false);
            BotonPausa.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    #region VIDAS Y LLAVES
    private void UpdateHearts()
    {
        float remainingHealth = currentHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            float fill = Mathf.Clamp01(remainingHealth);//devuelve un valor entre 0 y 1, si es mas de 1 da 1
            hearts[i].fillAmount = fill;

            remainingHealth -= 1f;
        }
    }
    private void UpdateKeys()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (i < numLlaves)
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


    #region DIALOGOS
    private void StartDialogue(string[] array)
    {
        PlayerCanMoveEvent quieto = new PlayerCanMoveEvent(false);
        EventManager.instance.Publicar(quieto);

        arrayTextosDialogos = array;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }
    private void StopDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
    }
    private IEnumerator ShowLine()
    {
        textoDialogo.text = string.Empty;
        foreach (char ch in arrayTextosDialogos[lineIndex])
        {
            textoDialogo.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        yield return new WaitForSeconds(3.5f);
        if (lineIndex < arrayTextosDialogos.Length - 1)
        {
            ActivarCartel();
        }
    }
    private void ActivarCartel()
    {
        lineIndex++;
        if (lineIndex < arrayTextosDialogos.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);


            PlayerCanMoveEvent muevete = new PlayerCanMoveEvent(true);
            EventManager.instance.Publicar(muevete);
        }
    }
    void Update()
    {
        if (didDialogueStart && Mouse.current.leftButton.wasPressedThisFrame && (lineIndex < arrayTextosDialogos.Length))
        {
            dialoguePanel.SetActive(true);
            if (textoDialogo.text == arrayTextosDialogos[lineIndex])
            {
                ActivarCartel();
            }
            else
            {
                StopAllCoroutines();
                //StopCoroutine(ShowLine());
                textoDialogo.text = arrayTextosDialogos[lineIndex];
            }
        }
        if (isPaused && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseGame();
        }
    }
    #endregion

    public void PauseGame()
    {
        SimpleEvent pausita = new SimpleEvent(eventType.GamePaused);
        EventManager.instance.Publicar(pausita);
    }

    public void UpdateVolume(float volumen)
    {
        if(volumen <= 0f) volumen = 0.0001f;
        VolumeEvent eventoVolumen = new VolumeEvent(volumen);
        EventManager.instance.Publicar(eventoVolumen);
    }

    void Start()
    {
        //currentHealth = maxHealth;
        UpdateHearts();
        UpdateKeys();

        EventManager.instance.Subscribir(eventType.PlayerStatsUpdated, this);
        EventManager.instance.Subscribir(eventType.DialogueStarted, this);
        EventManager.instance.Subscribir(eventType.CollectiblePicked, this);
        EventManager.instance.Subscribir(eventType.GamePaused, this);
        EventManager.instance.Subscribir(eventType.UseKey, this);
        EventManager.instance.Subscribir(eventType.PlayerDied, this);

        BotonPausa.SetActive(true);
        BotonDespausa.SetActive(false);
        panelPausa.SetActive(false);
        PanelMuerte.SetActive(false);

        if (volumen != null)
        {
            volumen.value = 1f;
            volumen.onValueChanged.AddListener(UpdateVolume);
        }

    }

    void OnDestroy()
    {
        if (EventManager.instance!=null)
        {
            EventManager.instance.Desuscribir(eventType.PlayerStatsUpdated, this);
            EventManager.instance.Desuscribir(eventType.DialogueStarted, this);
            EventManager.instance.Desuscribir(eventType.CollectiblePicked, this);
            EventManager.instance.Desuscribir(eventType.GamePaused, this);
            EventManager.instance.Desuscribir(eventType.UseKey, this);
            EventManager.instance.Desuscribir(eventType.PlayerDied, this);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Scene");
    }
}
