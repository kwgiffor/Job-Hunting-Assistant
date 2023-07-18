using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;

namespace JobHuntingAssistant.AI{

    /// <summary>
    /// 
    /// </summary>
    public class GPT4Model : IAIModel
    {
        private readonly OpenAIAPI _api;
        private const string _apiKey = "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";
        private int _maxTokens = 1500;

        public GPT4Model()
        {
            _api = new OpenAIAPI(new APIAuthentication(_apiKey));
        }

        /// <summary>
        /// Generates a resume using the GPT-4 model
        /// </summary>
        public string Prompt(string prompt)
        {
            // Send the prompt to GPT-4
            var chatRequest = new ChatRequest()
            {
                Model = OpenAI_API.Models.Model.GPT4,
		        Temperature = 0.8,
		        MaxTokens = _maxTokens,
		        Messages = new ChatMessage[] {
			        new ChatMessage(ChatMessageRole.User, prompt)
		        }
            };
            
            // Get the generated text from GPT-4
            var chatResult = _api.Chat.CreateChatCompletionAsync(chatRequest).Result;
            string generatedText = chatResult.ToString();

            return generatedText;
        }
    }

}