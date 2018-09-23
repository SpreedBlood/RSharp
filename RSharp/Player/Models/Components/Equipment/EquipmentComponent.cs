using RSharp.API.Players.Equipment;
using System.Collections.Generic;

namespace RSharp.Player.Models.Components.Equipment
{
    internal class EquipmentComponent
    {
        internal IList<IEquipment> Equipment { get; }

        internal EquipmentComponent()
        {
            Equipment = new List<IEquipment>();

            //Initializes the equipment.
            for (byte i = 0; i < EquipmentData.Amount; i++)
            {
                Equipment.Add(new Equipment());
            }
        }
    }
}
