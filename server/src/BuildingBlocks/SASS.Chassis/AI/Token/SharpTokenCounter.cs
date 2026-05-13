using SharpToken;

namespace SASS.Chassis.AI.Token
{
    public sealed class SharpTokenCounter : ITokenCounter
    {
        private readonly GptEncoding _encoding = GptEncoding.GetEncoding("cl100k_base");

        // Use cl100k_base encoding which is compatible with GPT-4, GPT-3.5, and many modern models

        public int CountTokens(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }
        
            // Count tokens using SharpToken
            var tokens = _encoding.Encode(text);
            return tokens.Count;
        }
    }
}

