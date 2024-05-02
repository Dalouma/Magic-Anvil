using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCustomerData : MonoBehaviour
{
    public List<string> preferredWeapons= new List<string>()
    {
        "Axe",
        "Shield",
        "Dagger",
        "Hammer",
        "Sword"
    };
    public List<string> hatedWeapons = new List<string>()
    {
        "Axe",
        "Shield",
        "Dagger",
        "Hammer",
        "Sword"
    };
    public List<string> introDialogue = new List<string>()
    {
        "Hello smithy, I'm looking to get something to help me with the trees on my farm",
        "Hey! I need something to help in my next battle!",
        "It's time for my kid to learn the blade, could you make something fit for him?"
    };
    public List<string> secondLine = new List<string>()
    {
        "Got anything like that?",
        "Something for the front lines",
        "Nothing too fancy, he's still small"
    };
    public List<string> payment = new List<string>()
    {
        "Neat, how much?",
        "Alright, name your price",
        "Hmm lets see, how far will this set me back?"
    };
    public List<string> preferred = new List<string>()
    {
        "I think this will do",
        "Alright, I bet I can make this work",
        "Cool, I'll take it"
    };
    public List<string> unpreferred = new List<string>()
    {
        "Ah, nah, I think I'll look elsewhere",
        "You kidding me? No thanks",
        "Hmm, um, you can keep it"
    };
    public List<string> baddeal = new List<string>()
    {
        "*Sigh* alright, I'll take it, but man that's steep",
        "Jeez, alright then.",
        "You're clearing me out but, alright."
    };
    public List<string> neutral = new List<string>()
    {
        "That's fine, sure.",
        "Fair deal, I'll take it. Here's the money",
        "Acceptable, you got yourself a deal"
    };
    public List<string> gooddeal = new List<string>()
    {
        "Wow, yeah I'll do that, thanks",
        "Great deal! I'll have to tell my boys about this place",
        "Yeah sure, that's a great price yeah."
    };
    public List<string> refusal = new List<string>()
    {
        "You're funny, what's your actual price?",
        "I don't have that much money boss",
        "Uh that's a little much dont you think?"

    };
}
