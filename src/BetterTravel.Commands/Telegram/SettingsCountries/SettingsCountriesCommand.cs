﻿using BetterTravel.Commands.Abstractions;

namespace BetterTravel.Commands.Telegram.SettingsCountries
{
    public class SettingsCountriesCommand : ICommand
    {
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}