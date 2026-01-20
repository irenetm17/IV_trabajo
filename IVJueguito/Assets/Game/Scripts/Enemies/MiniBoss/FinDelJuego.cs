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
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;

        yield return new WaitForSeconds(delayTexto);
        textoFin.SetActive(true);

        yield return new WaitForSeconds(delayObjetoFinal);
        botonFinal.SetActive(true);
    }

    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        AudioService.instance.PlaySFX("Boton");
        SceneManager.LoadScene("MenuInicial");
    }
}