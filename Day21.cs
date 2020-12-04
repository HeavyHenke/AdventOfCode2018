namespace AoC2018
{
    class Day21
    {
        public string CalcA()
        {
            var machine = new Machine("Day21.txt");

            machine.Calc(0);   // Min/Max found thru debugging
            return machine.NumInstructionsExecuted.ToString();
        }
    }
}