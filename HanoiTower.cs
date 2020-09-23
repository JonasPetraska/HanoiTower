using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower
{
    public class HanoiTower
    {
        private int _numberOfDisks;
        private char _nameOfStartTower;
        private char _nameOfEndTower;
        private char _nameOfIntermediaryTower;
        private int _counter;

        private HanoiTowerState _state;

        public HanoiTower(int numberOfDisks, char nameOfStartTower, char nameOfEndTower, char nameOfIntermediaryTower)
        {
            _numberOfDisks = numberOfDisks;
            _nameOfStartTower = nameOfStartTower;
            _nameOfEndTower = nameOfEndTower;
            _nameOfIntermediaryTower = nameOfIntermediaryTower;

            var initialFirstTowerState = new List<int>();
            for (int i = 1; i <= numberOfDisks; i++)
                initialFirstTowerState.Add(i);

            initialFirstTowerState = initialFirstTowerState.OrderByDescending(x => x).ToList();

            _state = new HanoiTowerState(_nameOfStartTower, _nameOfEndTower, _nameOfIntermediaryTower, initialFirstTowerState, null, null);
        }

        public void Execute()
        {
            Console.WriteLine($"Initial state: {_state.GetState()}");
            ExecuteInternal(_numberOfDisks, _nameOfStartTower, _nameOfEndTower, _nameOfIntermediaryTower);
        }

        private void ExecuteInternal(int numberOfDisk, char startTower, char endTower, char intermediaryTower)
        {
            if (numberOfDisk <= 0)
                return;

            ExecuteInternal(numberOfDisk - 1, startTower, intermediaryTower, endTower);
            _state.Move(startTower, endTower, numberOfDisk);
            _counter++;
            Console.WriteLine($"{_counter}. Move disk {numberOfDisk} from {startTower} to {endTower}. {_state.GetState()}.");
            ExecuteInternal(numberOfDisk - 1, intermediaryTower, endTower, startTower);
        }
    }

    public class HanoiTowerState
    {
        private Dictionary<char, List<int>> _internalState;
        private char _nameOfStartTower;
        private char _nameOfEndTower;
        private char _nameOfIntermediaryTower;

        public HanoiTowerState(char nameOfStartTower, char nameOfEndTower, char nameOfIntermediaryTower, List<int> initialFirstTowerState, List<int> initialSecondTowerState, List<int> initialThirdTowerState)
        {
            _nameOfStartTower = nameOfStartTower;
            _nameOfEndTower = nameOfEndTower;
            _nameOfIntermediaryTower = nameOfIntermediaryTower;
            _internalState = new Dictionary<char, List<int>>();
            _internalState.Add(_nameOfStartTower, initialFirstTowerState ?? new List<int>());
            _internalState.Add(_nameOfEndTower, initialSecondTowerState ?? new List<int>());
            _internalState.Add(_nameOfIntermediaryTower, initialThirdTowerState ?? new List<int>());
        }

        public string GetState()
        {
            return $"{_nameOfStartTower}=({string.Join(",", _internalState[_nameOfStartTower])}), {_nameOfIntermediaryTower}=({string.Join(",", _internalState[_nameOfIntermediaryTower])}), {_nameOfEndTower}=({string.Join(",", _internalState[_nameOfEndTower])})";
        }

        public void Move(char fromTower, char toTower, int diskNumber)
        {
            _internalState[fromTower].Remove(diskNumber);
            _internalState[toTower].Add(diskNumber);
        }
    }
}
