using System;

public interface IScheduledAction
{
    void DoAction();
}

public class JokeAction : IScheduledAction
{
    public void DoAction()
    {
        Console.WriteLine("Telling a joke: Why don’t scientists trust atoms? Because they make up everything!");
    }
}

public class FactAction : IScheduledAction
{
    public void DoAction()
    {
        Console.WriteLine("Sharing a fact: Honey never spoils. Archaeologists have found pots of honey in ancient Egyptian tombs that are over 3,000 years old and still perfectly edible!");
    }
}