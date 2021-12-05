﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexesSystem
{
    //[DebuggerDisplay]
    public class Position
    {
        public event EventHandler Activated;
        public event EventHandler Deactivated;
        
        public void Activate()
        {
            OnActivate(EventArgs.Empty);
        }

        private void OnActivate(EventArgs eventArgs)
        {
            var handler = Activated;
            handler?.Invoke(this, eventArgs);
        }

        public void Deactivate()
        {
            OnDeactivate(EventArgs.Empty);
        }

        private void OnDeactivate(EventArgs eventArgs)
        {
            var handler = Deactivated;
            handler?.Invoke(this, eventArgs);
        }
    }
}
