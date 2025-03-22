using Hæveautomaten.Interfaces.Views;

namespace HæveautomatenTests.Views
{
    public class FakeBaseView : IBaseView
    {
        private Queue<string> _inputs;
        private List<string> _outputs;

        public FakeBaseView()
        {
            _inputs = new Queue<string>();
            _outputs = new List<string>();
        }

        public void SetUserInputs(IEnumerable<string> inputs)
        {
            _inputs = new Queue<string>(inputs);
        }

        public string GetUserInput()
        {
            return _inputs.Dequeue();
        }

        public void DisplayMessage(string message)
        {
            _outputs.Add(message);
        }

        public List<string> GetOutputs()
        {
            return _outputs;
        }

        public string GetUserInputWithTitle(string message)
        {
            return _inputs.Dequeue();
        }

        public void CustomOutput(string output)
        {
            _outputs.Add(output);
        }

        public void CustomMenu(string[] customMenuOptions, string customBackTitle = "Back")
        {
            foreach (var option in customMenuOptions)
            {
                _outputs.Add(option);
            }
            _outputs.Add(customBackTitle);
        }
    }
}