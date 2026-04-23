using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [Header("Telas")]
    public GameObject optionsPanel;

    [Header("Áudio")]
    public AudioSource musicaFundo; // O "toca-fitas" da sua música

    // --- BOTÕES DE ABRIR E FECHAR ---
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    // --- CONTROLES DE VOLUME ---

    // Controla ABSOLUTAMENTE TUDO (Efeitos + Música)
    public void MudarVolumeGeral(float volume)
    {
        AudioListener.volume = volume;
    }

    // Controla APENAS a Música de Fundo
    public void MudarVolumeMusica(float volume)
    {
        if (musicaFundo != null)
        {
            musicaFundo.volume = volume;
        }
    }
}