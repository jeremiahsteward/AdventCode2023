namespace ACCoreComponents
{
    public static class Extraction
    {
        public static string ExtractionPath = "C:\\Temp\\AC";

        public static List<string> ReadInFile(this string filename)
        {
            List<string> data = new List<string>();

            string rawData = string.Empty;
            string fullPath = Path.Combine(ExtractionPath, filename);

            if (File.Exists(fullPath))
            {
                using (var sr = new StreamReader(fullPath))
                {
                    rawData = sr.ReadToEnd();
                }

                data = rawData.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                throw new FileNotFoundException($"The File [{filename}] could not be found at [{ExtractionPath}]");
            }

            return data;
        }

        public static List<string> ReadInDataAsListAndString(this string filename, out string oneLine)
        {
            List<string> data = new List<string>();

            string rawData = string.Empty;
            string fullPath = Path.Combine(ExtractionPath, filename);

            if (File.Exists(fullPath))
            {
                using (var sr = new StreamReader(fullPath))
                {
                    rawData = sr.ReadToEnd();
                }

                data = rawData.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();

                oneLine = rawData;
            }
            else
            {
                throw new FileNotFoundException($"The File [{filename}] could not be found at [{ExtractionPath}]");
            }

            return data;
        }
    }

    public static class Helpers
    {
        public static int ToInt(this string value, int defaultValue = 0)
        {
            int result = defaultValue;

            int.TryParse(value, out result);

            return result;
        }

        public static bool IsDigit(this char character)
        {
            string digits = "0123456789";

            return digits.Contains(character);
        }
    }
}