using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenAI;
using OpenAI.Models;
using OpenAI.Assistants;
// using OpenAI.Threads;
using System.Linq;
using DG.Tweening;
using OpenAI.Images;
using UnityEngine.Assertions;
using OpenAI.Chat;
 
public class ChatGPT_instance : MonoBehaviour
{  
    private Image _image;
    private TextMeshProUGUI _textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();  
    }

    public async void sendImageRequest(Texture2D texture2D, OpenAIClient api){
        string prompt = @"You are a helpful learning supporter for 5th grade students. Students are working on block-based coding on a 3D puzzle-like(or grid-path-finding) game. 
        This is 3D game, developed by using block-based coding. A unique feature of this game is its emphasis on integrating game narrative into overall learning activities: activating a portal with an avatar by programming the avatar's movements and actions with coding blocks. Students then can make terrain using bottom blocks, and strategically position the avatar and the portal within the game world. 
        Students can also make their games more challenging by adding two types of optional objects: items to collect and monsters to avoid.
        When you are requested to edit the image, you can use your creativity and should think of the purpose of this game and gameworld design. For instance, you can add more bottom blocks or change the position of the avatar and the portal. You can also add items and monsters to the game world.";

        var request = new ImageEditRequest(texture2D, prompt, 1, ImageSize.Small);
        var imageResults = await api.ImagesEndPoint.CreateImageEditAsync(request);

        foreach (var result in imageResults)
        {
            Debug.Log(result.ToString());
            Assert.IsNotNull(result.Texture);
            //save the image to the disk
            var bytes = result.Texture.EncodeToPNG();
            // System.IO.File.WriteAllBytes("/Users/nahunhee/image"+ Time.time+ ".png", bytes);
        }
    }

    public async void sendMessageRequest(string message, OpenAIClient api,  ScrollRect scrollRect)
    {
        Debug.Log("Sending message: " + message);
        string code = message;
        //start image color effect dotweening to light gray
        GetComponent<Image>().DOColor( new Color(0.8f, 0.8f, 0.8f, 1f), 0.5f).SetLoops(-1, LoopType.Yoyo);


        string basicPrompt = @"You are a helpful teacher to teach cyber security to high school students.";
        
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        
        var messages = new List<Message>
        {
            new Message(Role.System, basicPrompt),
            new Message(Role.User, "I want to know about cyber security regarding my question: " + message)
        };
        
        var chatRequest = new ChatRequest(messages, "gpt-4o", temperature: 0);
        var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
        var choice = response.FirstChoice;
        Debug.Log($"[{choice.Index}] {choice.Message.Role}: {choice.Message} | Finish Reason: {choice.FinishReason}"); 

        _textMeshPro.text = choice.Message.ToString() + "\n.";;
        _textMeshPro.GetComponent<AutoResizeTextUI>().forceUpdate();
        scrollRect.verticalNormalizedPosition = 0f;
        // Utils.setTypeWriterEffect(_textMeshPro, m_.PrintContent(), scrollRect);

        //stop image color effect dotweening
        GetComponent<Image>().DOKill();
    }
}
