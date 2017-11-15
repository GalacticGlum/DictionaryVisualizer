namespace DictionaryVisualizer
{
    public static class DefinitionManager
    {
        public static string[] GetDefinition(string word)
        {
            switch (word)
            {
                case "A":
                    return new[] {"B"};
                case "B":
                    return new[] {"C", "A"};
                case "C":
                    return new[] {"E","A","B","A"};
                case "E":
                    return new[] { "C", "B", "A", "A"};
                default:
                    return null;
            }
        }
    }
}
