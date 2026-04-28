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
    // Função para o botão de SAIR do Menu Principal
    public void FecharJogo()
    {
        // Fecha o jogo compilado
        Application.Quit();

        // Para o Play dentro do Unity pra você testar
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}