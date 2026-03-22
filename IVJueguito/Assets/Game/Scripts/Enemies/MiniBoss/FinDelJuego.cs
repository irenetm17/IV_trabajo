using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinDelJuego : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject textoFin;
    [SerializeField] private GameObject botonFinal;

    [Header("Tiempos")]
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float delayTexto = 0.5f;
    [SerializeField] private float delayObjetoFinal = 1.0f;

    private void Awake()
    {
        textoFin.SetActive(false);
        botonFinal.SetActive(false);

        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
    }

    public void EndGame()
    {
        StartCoroutine(FinDelJuegoCoroutine());
    }

    private IEnumerator FinDelJuegoCoroutine()
    {
        Debug.Log("Corrutina de fin iniciada. Duración: " + fadeDuration);

        if (fadeImage == null)
        {
            Debug.LogError("¡No hay fadeImage asignada en el Inspector!");
            yield break;
        }

        float t = 0f;
        Color c = fadeImage.color;

        // Usamos unscaledDeltaTime por si el juego está en pausa o con lag
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;
        Debug.Log("Fade completado. Mostrando texto...");

        yield return new WaitForSecondsRealtime(delayTexto);
        if (textoFin != null) textoFin.SetActive(true);

        yield return new WaitForSecondsRealtime(delayObjetoFinal);
        if (botonFinal != null) botonFinal.SetActive(true);

        // Habilitamos el ratón por si estaba bloqueado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        AudioService.instance.PlaySFX("Boton");
        SceneManager.LoadScene("MenuInicial");
    }
}