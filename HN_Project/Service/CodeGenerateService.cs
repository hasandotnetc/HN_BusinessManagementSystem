namespace HN_Backend.Service
{
    public class CodeGenerateService
    {
        public string GenerateCode(string prefix, int length, int number)
        {
            string numberStr = number.ToString().PadLeft(length, '0');
            return $"{prefix}{numberStr}";
        }
    }
}
