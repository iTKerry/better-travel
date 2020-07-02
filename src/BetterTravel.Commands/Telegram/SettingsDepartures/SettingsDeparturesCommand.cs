﻿using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsDepartures
{
    public class SettingsDeparturesCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}