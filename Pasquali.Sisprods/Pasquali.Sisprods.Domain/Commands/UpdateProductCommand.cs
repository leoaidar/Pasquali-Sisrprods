using System;
using Flunt.Notifications;
using Flunt.Validations;
using Pasquali.Sisprods.Domain.Commands.Contracts;

namespace Pasquali.Sisprods.Domain.Commands
{
    public class UpdateProductCommand : Notifiable, ICommand
    {
        public UpdateProductCommand() { }

        public UpdateProductCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsGreaterThan(Id,0,"Id", "Cliente sem identificador!")
                    .HasMinLen(Name, 6, "Name", "Por favor, digite o nome do produto!")
                    .HasMaxLen(Name, 150, "Name", "Limite ultrapasado do nome do produto!")
            );
        }
    }
}