namespace Extensions
{
    public static partial class BuilderExtension
    {
        public static int SymbolCount(this string self, char symbol)
        {
            var count = 0;

            for (int i = 0; i < self.Length; i++)
            {
                if (self[i] == symbol)
                {
                    count++;
                }
            }

            return count;
        }
    }
}