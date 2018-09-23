namespace RSharp.API.Players.Equipment
{
    public static class EquipmentData
    {
        /// <summary>
        /// Gets the amount of equipment pieces.
        /// </summary>
        public const byte Amount = 14;

        /// <summary>
        /// All the equipment fields.
        /// </summary>
        #region Equipment Fields
        public const byte Hat = 0;
        public const byte Cape = 1;
        public const byte Amulet = 2;
        public const byte Weapon = 3;
        public const byte Chest = 4;
        public const byte Shield = 5;
        public const byte Legs = 7;
        public const byte Hands = 9;
        public const byte Feet = 10;
        public const byte Ring = 12;
        public const byte Arrows = 13;
        #endregion
    }
}