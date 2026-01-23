using UnityEngine;
using UnityEngine.UI;

public class MagicCooldownBarUI : MonoBehaviour
{
    [SerializeField] private PlayerFireballLauncher launcher;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        if (launcher == null || fillImage == null) return;
        fillImage.fillAmount = launcher.Cooldown01;
    }
}