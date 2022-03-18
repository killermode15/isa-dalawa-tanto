using UnityEngine;

public static class AudioHelper
{
    public static AudioSource PlayClip(AudioClip clip, bool destroyAfter = true)
    {
        if(!clip)
        {
            Debug.LogWarning("No clip found");
            return null;
        }

        GameObject sourceObj = new GameObject($"[Clip] {clip.name}");
        AudioSource source = sourceObj.AddComponent<AudioSource>();

        source.clip = clip;
        source.Play();

        if (destroyAfter)
            GameObject.Destroy(sourceObj, clip.length);

        return source;
    }
}
