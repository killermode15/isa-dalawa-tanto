public static class ExtensionClass
{
    public static string AsString(this CharacterName charName)
    {
        string name = charName.ToString();
        string finalName = string.Empty;

        if (name.Contains("Mother"))
        {
            name = name.Remove(0, 6);
            finalName = "Mother " + name;
        }
        else if (name.Contains("Mrs"))
        {
            name = name.Remove(0, 3);
            finalName = "Mrs. " + name;
        }
        else if(name.Contains("Ms"))
        {
            name = name.Remove(0, 2);
            finalName = "Ms. " + name;
        }
        else if(name == "Null")
        {
            finalName = string.Empty;
        }
        else
        {
            finalName = name;
        }

        return finalName;
    }
}