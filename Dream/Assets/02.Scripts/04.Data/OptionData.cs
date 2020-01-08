
[System.Serializable]
public class OptionData
{
    public int language = 0;
    public float sound = 0.5f;
    public bool isrighthand = true;

    public void ChangeOption (int newLang, float newSound, bool newHand)
    {
        language = (int)newLang;
        sound = newSound;
        isrighthand = newHand;
    }
}
