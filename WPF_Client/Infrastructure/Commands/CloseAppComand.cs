using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Client.Infrastructure.Commands.Base;

namespace WPF_Client.Infrastructure.Commands
{
    internal class CloseAppComand:Command
    {
        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) => Application.Current.Shutdown();
    }
}
