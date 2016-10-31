using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BattleShipModel
{
    public class ShipFactory
    {
        private readonly IEnumerable<Type> _shipTypes;

        public ShipFactory()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            _shipTypes = currentAssembly.GetTypes()
                .Where(t => typeof (Ship).IsAssignableFrom(t) && !t.IsAbstract);
        }

        public Ship Create(ShipType shipType, string start, string end)
        {
            try
            {
                return _shipTypes
                    .Select(type => CreateSpecific(type, shipType, new BoardCoordinate(start), new BoardCoordinate(end)))
                    .First(mammal => mammal != null);
            }
            catch (Exception)
            {
                throw new ApplicationException();
            }
        }

        public Ship CreateSpecific(Type type, ShipType shipEnumType, BoardCoordinate startCoordinate,
            BoardCoordinate endCoordinate)
        {
            var mammalInstance = (Ship) Activator.CreateInstance(type, startCoordinate, endCoordinate);

            return mammalInstance.Is(shipEnumType) ? mammalInstance : null;
        }
    }
}