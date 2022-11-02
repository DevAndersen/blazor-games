﻿namespace DevAndersen.BlazorGames.Core.GameHandlers;

public class RockPaperScissorsHandler : GameHandler
{
    public RockPaperScissorsHandler(IEnumerable<Guid> players) : base(GameIdentity.RockPaperScissors, players)
    {
    }
}
