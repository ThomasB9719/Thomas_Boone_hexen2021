using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.ReplaySystem
{
    public class DelegateReplayCommand : IReplayableCommand
    {
        private Action _forward;
        private Action _backward;

        public DelegateReplayCommand(Action forward, Action backward)
        {
            _forward = forward;
            _backward = backward;
        }

        public void Forward()
            => _forward();

        public void Backward()
            => _backward();
    }
}
